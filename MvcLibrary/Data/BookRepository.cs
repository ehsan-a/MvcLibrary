using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BookRepository : IRepository<Book>
    {
        public List<Book> Items { get; set; } = new();
        public void Add(Book item) => Items.Add(item);
        public void Remove(Book item) => Items.Remove(item);
        public void Delete(Book item) => item.IsDeleted = true;
        public void Update(Book item) { }
        public IEnumerable<Book> GetAll() => Items;
    }
}
