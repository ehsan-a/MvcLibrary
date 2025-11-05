using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        [Required]
        public string Title { get; set; }
        [DisplayName("Category"), Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Year { get; set; }
        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public Book()
        {
            Id = _nextId++;
            IsAvailable = true;
            IsDeleted = false;
        }
    }
}
