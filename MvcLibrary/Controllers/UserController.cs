using Microsoft.AspNetCore.Mvc;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class UserController : Controller
    {
        public IRepository<User>? Repository { get; set; }
        public IRepository<Borrow>? BorrowRepo { get; set; }
        public UserController(IRepository<User> repository, IRepository<Borrow> borrowRepo)
        {
            Repository = repository;
            BorrowRepo = borrowRepo;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            return View(Repository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("FullName", "Email", "Username", "Password", "IsAdmin")] User user)
        {
            if (ModelState.IsValid) Repository.Add(user);
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return RedirectToAction("Login");
            else
                return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Edit(bool notUsed)
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetInt32("_userId"))))
                return RedirectToAction("Profile");
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetInt32("_userId"))))
                return RedirectToAction("Profile");
            var user = Repository.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("_userId", user.Id);
                HttpContext.Session.SetInt32("_userType", (user.IsAdmin ? 1 : 0));
                HttpContext.Session.SetString("_userFullName", user.FullName);
            }
            return RedirectToAction("Profile");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        public IActionResult Profile()
        {
            var records = BorrowRepo.GetAll().Where(x => x.UserId == HttpContext.Session.GetInt32("_userId"));
            return View(records);
        }
        public IActionResult Register()
        {
            return RedirectToAction("Create");
        }
    }
}
