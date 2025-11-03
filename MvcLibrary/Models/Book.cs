namespace MvcLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
        public string? Year { get; set; }
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
