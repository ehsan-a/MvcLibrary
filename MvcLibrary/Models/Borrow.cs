using System.ComponentModel;

namespace MvcLibrary.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        [DisplayName("Borrow Date")]
        public DateTime BorrowDate { get; set; }
        [DisplayName("Return Date")]
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public DateTime GetReturnDate() => BorrowDate.AddDays(10);
        public Borrow()
        {
            Id = _nextId++;
            IsReturned = false;
            BorrowDate = DateTime.Now;
        }
    }
}
