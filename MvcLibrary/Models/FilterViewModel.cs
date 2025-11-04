using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcLibrary.Models
{
    public class FilterViewModel<T>
    {
        public List<T>? Items { get; set; }
        public SelectList? SelectListItems { get; set; }
        public string? SearchString { get; set; }
    }
}
