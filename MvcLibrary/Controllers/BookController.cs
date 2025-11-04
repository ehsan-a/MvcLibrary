using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class BookController : Controller
    {
        public BookRepository? Repository { get; set; }
        public IRepository<Category>? CategoryRepo { get; set; }
        public BookController(IRepository<Book> repository, IRepository<Category> categoryRepo)
        {
            Repository = repository as BookRepository;
            CategoryRepo = categoryRepo;
        }
        public IActionResult Index(string searchString, string bookCategory)
        {
            IEnumerable<Category> categoryQuery = CategoryRepo.GetAll();
            var books = Repository.GetAll().Where(x => x.IsDeleted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
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
            ViewData["CategoryId"] = new SelectList(CategoryRepo.GetAll(), "Id", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Title", "Author", "CategoryId", "Year")] Book book)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            if (ModelState.IsValid) Repository.Add(book);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            ViewData["CategoryId"] = new SelectList(CategoryRepo.GetAll(), "Id", "Title", id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "Title", "Author", "CategoryId", "Year")] Book book)
        {
            if (id != book.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var preBook = Repository.GetAll().FirstOrDefault(x => x.Id == id);
                preBook.Title = book.Title;
                preBook.Author = book.Author;
                preBook.CategoryId = book.CategoryId;
                preBook.Year = book.Year;
            }
            ViewData["CategoryId"] = new SelectList(CategoryRepo.GetAll(), "Id", "Title", id);
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book != null) Repository.Delete(book);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var book = Repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
