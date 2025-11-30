using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly IService<Category> _categoryService;
        public BookController(IService<Book> bookService, IService<Category> categoryService)
        {
            _bookService = bookService as BookService;
            _categoryService = categoryService;
        }
        public IActionResult Index(string searchString, string bookCategory)
        {
            IEnumerable<Category> categoryQuery = _categoryService.GetAll();
            var books = _bookService.GetAll().Where(x => x.IsDeleted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!string.IsNullOrEmpty(bookCategory))
            {
                books = books.Where(x => x.CategoryId == int.Parse(bookCategory));
            }
            var bookFilterVM = new FilterViewModel<Book>
            {
                SelectListItems = new SelectList(categoryQuery, "Id", "Title", bookCategory),
                Items = books.ToList(),
                SearchString = searchString,
            };
            return View(bookFilterVM);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Title", "Author", "CategoryId", "Year")] Book book)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            if (ModelState.IsValid) _bookService.Add(book);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = _bookService.GetAll().FirstOrDefault(x => x.Id == id);
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Title", id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "Title", "Author", "CategoryId", "Year")] Book book)
        {
            if (id != book.Id) return NotFound();
            if (ModelState.IsValid) _bookService.Update(book);
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Title", id);
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _bookService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _bookService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book != null) _bookService.Delete(book);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var book = _bookService.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}