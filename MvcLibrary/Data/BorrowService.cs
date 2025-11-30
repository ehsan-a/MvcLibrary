using Microsoft.EntityFrameworkCore;
using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BorrowService : IService<Borrow>
    {
        private readonly AppDbContext _dbContext;

        public BorrowService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Borrow item)
        {
            _dbContext.Borrows.Add(item);
            _dbContext.SaveChanges();
        }
        public void Remove(Borrow item)
        {
            _dbContext.Borrows.Remove(item);
            _dbContext.SaveChanges();
        }
        public void Update(Borrow item)
        {
            _dbContext.Borrows.Update(item);
            _dbContext.SaveChanges();
        }
        public IEnumerable<Borrow> GetAll() => _dbContext.Borrows.Include(x => x.User).Include(b => b.Book);
    }
}
