using System.ComponentModel.DataAnnotations;

namespace MvcLibrary.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public List<Book>? Books { get; set; }
        public Category()
        {
            IsDeleted = false;
        }
    }
}
