using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarkLight
{
    [Serializable]
    public class CalculatedViewField<T> : ViewField<T>, IAutoSubscriber
    {
        bool IsCalculating;
        bool CachedValueIsValid;
        HashSet<ViewFieldBase> FieldsSubscribedTo = new HashSet<ViewFieldBase>();
        HashSet<IObservableList> ObservableListsSubscribedTo = new HashSet<IObservableList>();
        public System.Func<T> GetCalculatedValue;
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

        void IAutoSubscriber.ObservableListWasAccessed(IObservableList list)
        {
            if (!ObservableListsSubscribedTo.Contains(list))
            {
                list.ListChanged += List_ListChanged;
                ObservableListsSubscribedTo.Add(list);
            }
        }

        private void List_ListChanged(object sender, ListChangedEventArgs e)
        {
            CachedValueIsValid = false;
            if (!IsCalculating)
                UpdateCachedValue();
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

            foreach (IObservableList notifier in ObservableListsSubscribedTo)
                notifier.ListChanged -= List_ListChanged;
            ObservableListsSubscribedTo.Clear();
        }
    }

    [Serializable]
    public class _CalculatedString : CalculatedViewField<string> { }
}