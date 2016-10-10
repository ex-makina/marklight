using System;
using UnityEngine;

namespace MarkLight
{
    public class CalculatedViewField<T> : ViewField<T>, IAutoSubscriber
    {
        bool CachedValueIsValid = false;
        System.Collections.Generic.HashSet<ViewFieldBase> FieldsSubscribedTo = new System.Collections.Generic.HashSet<ViewFieldBase>();
        public System.Func<T> GetCalculatedValue;
        public System.Action<T> UpdateSourceValuesFromValue;

        public override T InternalValue
        {
            get
            {
                return GetValue();
            }

            set
            {
                SetValue(value);
                base.InternalValue = value;
            }
        }

        public override T DirectValue
        {
            set
            {
                SetValue(value);
                base.DirectValue = value;
            }
        }

        public override object DirectObjectValue
        {
            set
            {
                SetValue((T)value);
                base.DirectObjectValue = value;
            }
        }

        public override object ObjectValue
        {
            get
            {
                return GetValue();
            }

            set
            {
                SetValue((T)value);
                base.ObjectValue = value;
            }
        }

        public override bool IsSet
        {
            get
            {
                return true;
            }
        }

        public override T Value
        {
            get
            {
                return GetValue();
            }

            set
            {
                SetValue(value);
                base.Value = value;
            }
        }

        T GetValue()
        {
            Debug.Log("Calculating value");
            if (GetCalculatedValue == null)
                throw new System.InvalidOperationException("GetCalculatedValue has not been specifed");

            T calculatedValue;
            if (CachedValueIsValid)
                return InternalValue;
            try
            {
                ClearChangeSubscriptions();
                AutoSubscription.StartSubscription(this);
                calculatedValue = GetCalculatedValue();
            }
            finally
            {
                AutoSubscription.EndSubscription(this);
            }
            CachedValueIsValid = true;
            base.Value = calculatedValue;
            return calculatedValue;
        }

        void SetValue(T value)
        {
            if (UpdateSourceValuesFromValue == null)
                return;
            UpdateSourceValuesFromValue(value);
            CachedValueIsValid = false;
        }

        void IAutoSubscriber.ViewFieldWasAccessed(ViewFieldBase viewField)
        {
            if (!FieldsSubscribedTo.Contains(viewField))
                viewField.ValueSet += ViewField_ValueSet;
        }

        void ViewField_ValueSet(object sender, EventArgs e)
        {
            Debug.Log("A subscribed-to member's value changed");
            CachedValueIsValid = false;
            base.InternalValue = default(T);
        }

        void ClearChangeSubscriptions()
        {
            Debug.Log("Unsubscribing from " + FieldsSubscribedTo.Count + " fields");
            foreach (ViewFieldBase notifier in FieldsSubscribedTo)
                notifier.ValueSet -= ViewField_ValueSet;
            FieldsSubscribedTo.Clear();
        }
    }
}