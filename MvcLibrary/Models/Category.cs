namespace MvcLibrary.Models
{
    public class Category
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        public string Title { get; set; }
        public Category()
        {
            Id = _nextId++;
        }
    }
}
