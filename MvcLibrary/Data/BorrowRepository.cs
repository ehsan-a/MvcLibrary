using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BorrowRepository : IRepository<Borrow>
    {
        public List<Borrow> Items { get; set; } = new();
        public void Add(Borrow item) => Items.Add(item);
        public void Remove(Borrow item) => Items.Remove(item);
        public void Update(Borrow item) { }
        public IEnumerable<Borrow> GetAll() => Items;
    }
}
