using UnityEngine;
using System.Collections.Generic;

namespace MarkLight
{
    public static class AutoSubscription
    {
        static Stack<IAutoSubscriber> AutoSubscriberQueue = new Stack<IAutoSubscriber>();
        static HashSet<IAutoSubscriber> AutoSubscribers = new HashSet<IAutoSubscriber>();

        public static int ActiveAutoSubscriberCount { get { return AutoSubscriberQueue.Count; } }

        public static void StartSubscription(IAutoSubscriber subscriber)
        {
            if (subscriber == null)
                throw new System.ArgumentNullException("subscriber");
            if (AutoSubscribers.Contains(subscriber))
                throw new System.InvalidOperationException("Recursive calculation detected");

            AutoSubscriberQueue.Push(subscriber);
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
                throw new System.InvalidOperationException("EndSubscription is being called for a subscriber that is not at the top of the stack");

            AutoSubscribers.Remove(subscriber);
            AutoSubscriberQueue.Pop();
        }

        public static void NotifyViewFieldWasAccessed(ViewFieldBase field)
        {
            if (AutoSubscriberQueue.Count == 0)
                return;

            IAutoSubscriber topmostSubscriber = AutoSubscriberQueue.Peek();
            topmostSubscriber.ViewFieldWasAccessed(field);
        }


    }
}
