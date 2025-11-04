using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BorrowRepository : IRepository<Borrow>
    {
        private readonly IBaseDataModel BaseDataModel;

        public BorrowRepository(IBaseDataModel baseDataModel)
        {
            BaseDataModel = baseDataModel;
            foreach (var item in baseDataModel.Borrows)
            {
                item.Book = baseDataModel.Books.Find(x => x.Id == item.BookId);
                item.User = baseDataModel.Users.Find(x => x.Id == item.UserId);
            }
        }
        public void Add(Borrow item) => BaseDataModel.Borrows.Add(item);
        public void Remove(Borrow item) => BaseDataModel.Borrows.Remove(item);
        public void Update(Borrow item) { }
        public IEnumerable<Borrow> GetAll() => BaseDataModel.Borrows;
    }
}
