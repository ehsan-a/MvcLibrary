using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcLibrary.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book? Book { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        [DisplayName("Borrow Date")]
        public DateTime BorrowDate { get; set; }
        [DisplayName("Return Date")]
        public DateTime? ReturnDate { get; set; }
        [DisplayName("Is Returned")]
        public bool IsReturned { get; set; }
        public DateTime GetReturnDate() => BorrowDate.AddDays(10);
        public Borrow()
        {
            IsReturned = false;
            BorrowDate = DateTime.Now;
        }
    }
}
