using UnityEngine;
using System.Collections.Generic;

namespace MarkLight
{
    public static class AutoSubscription
    {
        static Queue<IAutoSubscriber> AutoSubscriberQueue = new Queue<IAutoSubscriber>();
        static HashSet<IAutoSubscriber> AutoSubscribers = new HashSet<IAutoSubscriber>();

        public static int ActiveAutoSubscriberCount { get { return AutoSubscriberQueue.Count; } }

        public static void StartSubscription(IAutoSubscriber subscriber)
        {
            if (subscriber == null)
                throw new System.ArgumentNullException("subscriber");
            if (AutoSubscribers.Contains(subscriber))
                throw new System.InvalidOperationException("Recursive calculation detected");

            AutoSubscriberQueue.Enqueue(subscriber);
            AutoSubscribers.Add(subscriber);
        }

        public static void EndSubscription(IAutoSubscriber subscriber)
        {
            if (subscriber == null)
                throw new System.ArgumentNullException("subscriber");
            if (AutoSubscriberQueue.Count == 0)
                throw new System.InvalidOperationException("There are no active IAutoSubscribers");
            if (!AutoSubscribers.Contains(subscriber))
                throw new System.InvalidOperationException("EndSubscription called on a subscriber without StartSubscription");
            if (AutoSubscriberQueue.Peek() != subscriber)
                throw new System.InvalidOperationException("EndSubscription is being called for a subscriber that is not at the top of the queue");

            AutoSubscribers.Remove(subscriber);
            AutoSubscriberQueue.Dequeue();
        }

        public static void NotifyViewFieldWasAccessed(ViewFieldBase field)
        {
            if (AutoSubscriberQueue.Count == 0)
                return;

            Debug.Log("Notifying subscriber a view field was accessed");
            IAutoSubscriber topmostSubscriber = AutoSubscriberQueue.Peek();
            topmostSubscriber.ViewFieldWasAccessed(field);
        }


    }
}
