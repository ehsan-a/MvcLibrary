using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class UserService : IService<User>
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(User item)
        {
            _dbContext.Users.Add(item);
            _dbContext.SaveChanges();
        }
        public void Remove(User item)
        {
            _dbContext.Users.Remove(item);
            _dbContext.SaveChanges();
        }
        public void Delete(User item)
        {
            item.IsDeleted = true;
            _dbContext.SaveChanges();
        }
        public void Update(User item)
        {
            _dbContext.Users.Update(item);
            _dbContext.SaveChanges();
        }
        public IEnumerable<User> GetAll() => _dbContext.Users;
    }
}

