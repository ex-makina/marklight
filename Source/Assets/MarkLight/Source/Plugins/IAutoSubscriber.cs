namespace MarkLight
{
    public interface IAutoSubscriber
    {
        void ViewFieldWasAccessed(ViewFieldBase viewField);
        void ObservableListWasAccessed(IObservableList list);
    }
}
