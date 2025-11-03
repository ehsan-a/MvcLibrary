namespace MvcLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        private static int _nextId { get; set; } = 1;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public User()
        {
            Id = _nextId++;
        }
    }
}
