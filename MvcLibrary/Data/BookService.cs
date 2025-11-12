using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BookService : IService<Book>
    {
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Book item)
        {
            _dbContext.Books.Add(item);
            _dbContext.SaveChanges();
        }
        public void Remove(Book item)
        {
            _dbContext.Books.Remove(item);
            _dbContext.SaveChanges();
        }
        public void Delete(Book item)
        {
            item.IsDeleted = true;
            _dbContext.SaveChanges();
        }
        public void Update(Book item)
        {
            _dbContext.Books.Update(item);
            _dbContext.SaveChanges();
        }
        public IEnumerable<Book> GetAll() => _dbContext.Books;
    }
}
