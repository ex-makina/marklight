using System;
using UnityEngine;

namespace MarkLight
{
    [Serializable]
    public class CalculatedViewField<T> : ViewField<T>, IAutoSubscriber
    {
        bool IsCalculating;
        bool CachedValueIsValid;
        [System.NonSerialized]
        System.Collections.Generic.HashSet<ViewFieldBase> FieldsSubscribedTo = new System.Collections.Generic.HashSet<ViewFieldBase>();
        [System.NonSerialized]
        public System.Func<T> GetCalculatedValue;
        [System.NonSerialized]
        public System.Action<T> UpdateSourceValuesFromValue;

        public override T InternalValue
        {
            get
            {
                EnsureCachedValueIsUpToDate();
                return base.InternalValue;
            }

            set
            {
                base.InternalValue = value;
            }
        }

        public override T Value
        {
            get
            {
                EnsureCachedValueIsUpToDate();
                return base.Value;
            }

            set
            {
                base.Value = value;
            }
        }

        void EnsureCachedValueIsUpToDate()
        {
            if (!CachedValueIsValid)
                UpdateCachedValue();
        }

        void UpdateCachedValue()
        {
            CachedValueIsValid = false;
            if (GetCalculatedValue == null)
                throw new System.InvalidOperationException("GetCalculatedValue has not been specifed");
            T calculatedValue;
            try
            {
                IsCalculating = true;
                ClearChangeSubscriptions();
                AutoSubscription.StartSubscription(this);
                calculatedValue = GetCalculatedValue();
            }
            finally
            {
                IsCalculating = false;
                AutoSubscription.EndSubscription(this);
            }
            CachedValueIsValid = true;
            Value = calculatedValue;
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
            if (viewField != this && !FieldsSubscribedTo.Contains(viewField))
            {
                viewField.ValueSet += ViewField_ValueSet;
                FieldsSubscribedTo.Add(viewField);
            }
        }

        void ViewField_ValueSet(object sender, EventArgs e)
        {
            CachedValueIsValid = false;
            if (!IsCalculating)
                UpdateCachedValue();
        }

        void ClearChangeSubscriptions()
        {
            foreach (ViewFieldBase notifier in FieldsSubscribedTo)
                notifier.ValueSet -= ViewField_ValueSet;
            FieldsSubscribedTo.Clear();
        }
    }

    [Serializable]
    public class _CalculatedString : CalculatedViewField<string> { }
}