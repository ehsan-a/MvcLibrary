namespace MvcLibrary.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
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
