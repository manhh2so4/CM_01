namespace HStrong.Core.UI.Dragging{
    public interface IDragContainer<T> : IDragDestination<T>,IDragSource<T> where T : class
    {

    }
}