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
        private readonly IRepository<Borrow> _repository;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Book> _bookRepo;
        public BorrowController(IRepository<Borrow> repository, IRepository<User> userRepo, IRepository<Book> bookRepo)
        {
            _repository = repository;
            _userRepo = userRepo;
            _bookRepo = bookRepo;
        }
        public IActionResult Index(string searchString, string borrowBook)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();

            IEnumerable<Book> bookQuery = _bookRepo.GetAll();
            var borrows = _repository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                borrows = borrows.Where(s => s.User.FullName.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!string.IsNullOrEmpty(borrowBook))
            {
                borrows = borrows.Where(x => x.BookId == int.Parse(borrowBook));
            }
            var borrowFilterVM = new FilterViewModel<Borrow>
            {
                SelectListItems = new SelectList(bookQuery, "Id", "Title", borrowBook),
                Items = borrows.ToList(),
                SearchString = searchString,
            };

            return View(borrowFilterVM);
        }

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Session.GetInt32("_userId"))))
                return RedirectToAction("Login", "User");
            ViewData["UserId"] = new SelectList(_userRepo.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")), "Id", "FullName");
            ViewData["BookId"] = new SelectList(_bookRepo.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("BookId", "UserId")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.GetAll().FirstOrDefault(x => x.Id == borrow.BookId).IsAvailable = false;
                _repository.Add(borrow);
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Return(int id)
        {
            var borrow = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (borrow != null)
            {
                borrow.IsReturned = true;
                borrow.ReturnDate = DateTime.Now;
                _bookRepo.GetAll().First(x => x.Id == borrow.BookId).IsAvailable = true;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            ViewData["UserId"] = new SelectList(HttpContext.Session.GetInt32("_userType") != 1 ? _userRepo.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")) : _userRepo.GetAll(), "Id", "FullName");
            ViewData["BookId"] = new SelectList(_bookRepo.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "UserId", "BookId")] Borrow borrow)
        {
            if (id != borrow.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var preBorrow = _repository.GetAll().FirstOrDefault(x => x.Id == id);
                preBorrow.Book.IsAvailable = true;
                preBorrow.UserId = borrow.UserId;
                preBorrow.BookId = borrow.BookId;
                _bookRepo.GetAll().First(x => x.Id == borrow.BookId).IsAvailable = false;
            }
            ViewData["UserId"] = new SelectList(HttpContext.Session.GetInt32("_userType") != 1 ? _userRepo.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")) : _userRepo.GetAll(), "Id", "FullName");
            ViewData["BookId"] = new SelectList(_bookRepo.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View(borrow);
        }

        public IActionResult Details(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
