using MvcLibrary.Models;

namespace MvcLibrary.Data
{
    public class SeedData
    {
        public static void InitializeUser(IRepository<User> repository)
        {
            if (repository.Items.Any())
            {
                return;
            }
            repository.Items.Add(
                new User
                {
                    FullName = "Ehsan Arefzadeh",
                    Email = "arefzaadeh@gmail.com",
                    JoinDate = DateTime.Now,
                    Username = "ehsan",
                    Password = "1234",
                    IsAdmin = true

                }
            );
            repository.Items.Add(
                new User
                {
                    FullName = "Alireza Arefzadeh",
                    Email = "alireza@gmail.com",
                    JoinDate = DateTime.Now,
                    Username = "alireza",
                    Password = "1234",
                    IsAdmin = false
                }
            );
            repository.Items.Add(
                new User
                {
                    FullName = "Pouyan Arefzadeh",
                    Email = "pouyan@gmail.com",
                    JoinDate = DateTime.Now,
                    Username = "pouyan",
                    Password = "1234",
                    IsAdmin = false
                }
            );
        }
        public static void InitializeBook(IRepository<Book> repository)
        {
            if (repository.Items.Any())
            {
                return;
            }
            repository.Items.Add(
                new Book
                {
                    Title = "CSharp in a nutshell",
                    Author = "Oriely",
                    Genre = "Learning",
                    Year = "2023"
                }
            );
            repository.Items.Add(
                new Book
                {
                    Title = "Clean Code",
                    Author = "C Martin",
                    Genre = "Learning",
                    Year = "2000",
                    IsAvailable = false
                }
            );
            repository.Items.Add(
                new Book
                {
                    Title = "ASP.NET",
                    Author = "Microsoft",
                    Genre = "Learning",
                    Year = "2024"
                }
            );
        }
        public static void InitializeBorrow(IRepository<Borrow> repository)
        {
            if (repository.Items.Any())
            {
                return;
            }
            repository.Items.Add(
                new Borrow
                {
                    BookId = 2,
                    UserId = 2,
                }
            );
        }
    }
}

