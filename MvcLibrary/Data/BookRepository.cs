using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BookRepository : IRepository<Book>
    {
        private readonly IBaseDataModel baseDataModel;

        public BookRepository(IBaseDataModel baseDataModel)
        {
            this.baseDataModel = baseDataModel;
        }
        public void Add(Book item) => baseDataModel.Books.Add(item);
        public void Remove(Book item) => baseDataModel.Books.Remove(item);
        public void Delete(Book item) => item.IsDeleted = true;
        public void Update(Book item) { }
        public IEnumerable<Book> GetAll() => baseDataModel.Books;
    }
}
