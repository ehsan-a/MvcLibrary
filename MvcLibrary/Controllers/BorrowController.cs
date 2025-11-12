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
        private readonly IService<Borrow> _borrowService;
        private readonly IService<User> _userService;
        private readonly IService<Book> _bookService;
        public BorrowController(IService<Borrow> borrowService, IService<User> userService, IService<Book> bookService)
        {
            _borrowService = borrowService;
            _userService = userService;
            _bookService = bookService;
        }
        public IActionResult Index(string searchString, string borrowBook)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();

            IEnumerable<Book> bookQuery = _bookService.GetAll();
            var borrows = _borrowService.GetAll();
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
            ViewData["UserId"] = new SelectList(_userService.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")), "Id", "FullName");
            ViewData["BookId"] = new SelectList(_bookService.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("BookId", "UserId")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                _bookService.GetAll().FirstOrDefault(x => x.Id == borrow.BookId).IsAvailable = false;
                _borrowService.Add(borrow);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult Return(int id)
        {
            var borrow = _borrowService.GetAll().FirstOrDefault(x => x.Id == id);
            if (borrow != null)
            {
                borrow.IsReturned = true;
                borrow.ReturnDate = DateTime.Now;
                _bookService.GetAll().First(x => x.Id == borrow.BookId).IsAvailable = true;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _borrowService.GetAll().FirstOrDefault(x => x.Id == id);
            ViewData["UserId"] = new SelectList(HttpContext.Session.GetInt32("_userType") != 1 ? _userService.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")) : _userService.GetAll(), "Id", "FullName");
            ViewData["BookId"] = new SelectList(_bookService.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "UserId", "BookId")] Borrow borrow)
        {
            if (id != borrow.Id) return NotFound();
            if (ModelState.IsValid) _borrowService.Update(borrow);
            ViewData["UserId"] = new SelectList(HttpContext.Session.GetInt32("_userType") != 1 ? _userService.GetAll().Where(x => x.Id == HttpContext.Session.GetInt32("_userId")) : _userService.GetAll(), "Id", "FullName");
            ViewData["BookId"] = new SelectList(_bookService.GetAll().Where(x => x.IsAvailable == true), "Id", "Title");
            return View(borrow);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _borrowService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
