using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Emit;

namespace MvcLibrary.Controllers
{
    public class BorrowController : Controller
    {
        public IRepository<Borrow>? Repository { get; set; }
        public IRepository<User>? UserRepo { get; set; }
        public IRepository<Book>? BookRepo { get; set; }
        public BorrowController(IRepository<Borrow> repository, IRepository<User> userRepo, IRepository<Book> bookRepo)
        {
            Repository = repository;
            UserRepo = userRepo;
            BookRepo = bookRepo;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            return View(Repository.GetAll());
        }

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetInt32("_userId"))))
                return RedirectToAction("Login", "User");
            ViewData["UserId"] = new SelectList(UserRepo.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")), "Id", "FullName");
            ViewData["BookId"] = new SelectList(BookRepo.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("BookId", "UserId")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                BookRepo.GetAll().FirstOrDefault(x => x.Id == borrow.BookId).IsAvailable = false;
                Repository.Add(borrow);
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Return(int id)
        {
            var borrow = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (borrow != null)
            {
                borrow.IsReturned = true;
                borrow.ReturnDate = DateTime.Now;
                BookRepo.GetAll().First(x => x.Id == borrow.BookId).IsAvailable = true;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            ViewData["UserId"] = new SelectList(HttpContext.Session.GetInt32("_userType") != 1 ? UserRepo.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")) : UserRepo.GetAll(), "Id", "FullName");
            ViewData["BookId"] = new SelectList(BookRepo.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "UserId", "BookId")] Borrow borrow)
        {
            if (id != borrow.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var preBorrow = Repository.GetAll().FirstOrDefault(x => x.Id == id);
                preBorrow.Book.IsAvailable = true;
                preBorrow.UserId = borrow.UserId;
                preBorrow.BookId = borrow.BookId;
                BookRepo.GetAll().First(x => x.Id == borrow.BookId).IsAvailable = false;
            }
            ViewData["UserId"] = new SelectList(HttpContext.Session.GetInt32("_userType") != 1 ? UserRepo.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")) : UserRepo.GetAll(), "Id", "FullName");
            ViewData["BookId"] = new SelectList(BookRepo.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View(borrow);
        }

        public IActionResult Details(int id)
        {
            var book = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
