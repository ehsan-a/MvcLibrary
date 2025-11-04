using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class BaseDataModel : IBaseDataModel
    {
        public BaseDataModel()
        {
            Books = new List<Book>();
            Users = new List<User>();
            Borrows = new List<Borrow>();
            Categories = new List<Category>();
        }
        public List <Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<Borrow> Borrows { get; set; }
        public List<Category> Categories { get; set; }
    }
}
