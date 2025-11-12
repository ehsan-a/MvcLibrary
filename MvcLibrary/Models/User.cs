using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MvcLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, DisplayName("Full Name")]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime JoinDate { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public List<Borrow> Borrows { get; set; }
        public User()
        {
            JoinDate = DateTime.Now;
            IsDeleted = false;
            IsAdmin = false;
        }
    }
}
