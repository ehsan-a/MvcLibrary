using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BorrowRepository : IRepository<Borrow>
    {
        private readonly IBaseDataModel baseDataModel;

        public BorrowRepository(IBaseDataModel baseDataModel)
        {
            this.baseDataModel = baseDataModel;
        }
        public void Add(Borrow item) => baseDataModel.Borrows.Add(item);
        public void Remove(Borrow item) => baseDataModel.Borrows.Remove(item);
        public void Update(Borrow item) { }
        public IEnumerable<Borrow> GetAll() => baseDataModel.Borrows;
    }
}
