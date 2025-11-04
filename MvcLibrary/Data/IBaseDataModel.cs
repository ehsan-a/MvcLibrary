using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public interface IBaseDataModel
    {
        List<Book> Books { get; set; }
        List<User> Users { get; set; }
        List<Borrow> Borrows { get; set; }
        List<Category> Categories { get; set; }
    }
}
