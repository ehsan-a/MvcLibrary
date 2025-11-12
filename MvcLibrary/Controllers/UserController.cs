using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;
using System.Collections.Generic;

namespace MvcLibrary.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly IService<Borrow> _borrowService;
        public UserController(IService<User> userService, IService<Borrow> borrowService)
        {
            _userService = userService as UserService;
            _borrowService = borrowService;
        }
        public IActionResult Index(string searchString, string userType)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();

            var userQuery = new[] { new { Id = 0, Title = "Normal" }, new { Id = 1, Title = "Admin" } }.ToList();
            var users = _userService.GetAll().Where(x => x.IsDeleted == false);
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
            if (ModelState.IsValid) _userService.Add(user);
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

            var user = _userService.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
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
            var records = _borrowService.GetAll().Where(x => x.UserId == HttpContext.Session.GetInt32("_userId"));
            return View(records);
        }
        public IActionResult Register()
        {
            return RedirectToAction("Create");
        }

        public IActionResult Edit(int id)
        {
            var book = _userService.GetAll().FirstOrDefault(x => x.Id == id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "FullName", "Email", "Username", "Password", "IsAdmin")] User user)
        {
            if (id != user.Id) return NotFound();
            if (ModelState.IsValid) _userService.Update(user);
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var book = _userService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _userService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book != null) _userService.Delete(book);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var book = _userService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}