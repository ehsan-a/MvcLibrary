using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class UserRepository : IRepository<User>
    {
        private readonly IBaseDataModel baseDataModel;

        public UserRepository(IBaseDataModel baseDataModel)
        {
            this.baseDataModel = baseDataModel;
        }
        public void Add(User item) => baseDataModel.Users.Add(item);
        public void Remove(User item) => baseDataModel.Users.Remove(item);
        public void Delete(User item) => item.IsDeleted = true;
        public void Update(User item) { }
        public IEnumerable<User> GetAll() => baseDataModel.Users;
    }
}
