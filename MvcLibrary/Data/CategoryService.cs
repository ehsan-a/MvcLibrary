using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class CategoryService : IService<Category>
    {
        private readonly AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Category item)
        {
            _dbContext.Categories.Add(item);
            _dbContext.SaveChanges();
        }
        public void Remove(Category item)
        {
            _dbContext.Categories.Remove(item);
            _dbContext.SaveChanges();
        }
        public void Delete(Category item)
        {
            item.IsDeleted = true;
            _dbContext.SaveChanges();
        }
        public void Update(Category item)
        {
            _dbContext.Categories.Update(item);
            _dbContext.SaveChanges();
        }
        public IEnumerable<Category> GetAll() => _dbContext.Categories;
    }
}

