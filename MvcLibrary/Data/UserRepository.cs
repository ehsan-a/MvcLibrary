using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class UserRepository : IRepository<User>
    {
        public List<User> Items { get; set; } = new();
        public void Add(User item) => Items.Add(item);
        public void Remove(User item) => Items.Remove(item);
        public void Delete(User item) => item.IsDeleted = true;
        public void Update(User item) { }
        public IEnumerable<User> GetAll() => Items;
    }
}
