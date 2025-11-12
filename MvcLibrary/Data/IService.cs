namespace MvcLibrary.Data
{
    public interface IService<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        IEnumerable<T> GetAll();
    }
}
