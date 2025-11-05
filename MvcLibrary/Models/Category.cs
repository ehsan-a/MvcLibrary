using System.ComponentModel.DataAnnotations;

namespace MvcLibrary.Models
{
    public class Category
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        [Required]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public Category()
        {
            Id = _nextId++;
            IsDeleted = false;
        }
    }
}
