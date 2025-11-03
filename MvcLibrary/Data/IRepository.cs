namespace MvcLibrary.Data
{
    public interface IRepository<T>
    {
        List<T> Items { get; set; }
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        IEnumerable<T> GetAll();
    }
}
