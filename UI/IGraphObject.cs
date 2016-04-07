namespace UI
{
    public interface IGraphObject
    {
        NodeStatus Status { get; }

        void ChangeViewToDefault();
        void ChangeView();
    }
}