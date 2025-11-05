using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;
using System.Collections.Generic;

namespace MvcLibrary.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository;
        private readonly IRepository<Borrow> _borrowRepo;
        public UserController(IRepository<User> repository, IRepository<Borrow> borrowRepo)
        {
            _repository = repository as UserRepository;
            _borrowRepo = borrowRepo;
        }
        public IActionResult Index(string searchString, string userType)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();

            var userQuery = new[] { new { Id = 0, Title = "Normal" }, new { Id = 1, Title = "Admin" } }.ToList();
            var users = _repository.GetAll().Where(x => x.IsDeleted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Username.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!string.IsNullOrEmpty(userType))
            {
                users = users.Where(x => x.IsAdmin == (userType == "1" ? true : false));
            }
            var bookFilterVM = new FilterViewModel<User>
            {
                SelectListItems = new SelectList(userQuery, "Id", "Title", userType),
                Items = users.ToList(),
                SearchString = searchString,
            };

            return View(bookFilterVM);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("FullName", "Email", "Username", "Password", "IsAdmin")] User user)
        {
            if (ModelState.IsValid) _repository.Add(user);
            if (HttpContext.Session.GetInt32("_userType") != 1)
            {
                TempData["notificationRegister"] = "Register Successful! Please Login.";
                return RedirectToAction("Login");
            }
            else
                return RedirectToAction("Index");
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

            var user = _repository.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("_userId", user.Id);
                HttpContext.Session.SetInt32("_userType", (user.IsAdmin ? 1 : 0));
                HttpContext.Session.SetString("_userFullName", user.FullName);
                return RedirectToAction("Profile");
            }
            else
            {
                TempData["notificationLogin"] = "Username or Password is Wrong!";
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        public IActionResult Profile()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetInt32("_userId"))))
                return NotFound();
            var records = _borrowRepo.GetAll().Where(x => x.UserId == HttpContext.Session.GetInt32("_userId"));
            return View(records);
        }
        public IActionResult Register()
        {
            return RedirectToAction("Create");
        }

        public IActionResult Edit(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "FullName", "Email", "Username", "Password", "IsAdmin")] User user)
        {
            if (id != user.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var preUser = _repository.GetAll().FirstOrDefault(x => x.Id == id);
                preUser.FullName = user.FullName;
                preUser.Email = user.Email;
                preUser.Username = user.Username;
                preUser.Password = user.Password;
                preUser.IsAdmin = user.IsAdmin;
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book != null) _repository.Delete(book);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}