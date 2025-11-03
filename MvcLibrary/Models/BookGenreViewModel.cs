using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcLibrary.Models
{
    public class BookGenreViewModel
    {
        public List<Book>? Books { get; set; }
        public SelectList? Genres { get; set; }
    }
}
