using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly IBaseDataModel baseDataModel;

        public CategoryRepository(IBaseDataModel baseDataModel)
        {
            this.baseDataModel = baseDataModel;
        }
        public void Add(Category item) => baseDataModel.Categories.Add(item);
        public void Remove(Category item) => baseDataModel.Categories.Remove(item);
        public void Update(Category item) { }
        public IEnumerable<Category> GetAll() => baseDataModel.Categories;
    }
}
