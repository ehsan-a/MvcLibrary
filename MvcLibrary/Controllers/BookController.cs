using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class BookController : Controller
    {
        public IRepository<Book>? Repository { get; set; }
        public BookController(IRepository<Book> repository)
        {
            Repository = repository;
        }
        public IActionResult Index(string searchString, string bookGenre)
        {
            IEnumerable<string> genreQuery = Repository.GetAll().Select(m => m.Genre!);
            var books = Repository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre == bookGenre);
            }
            var bookGenreVM = new BookGenreViewModel
            {
                Genres = new SelectList(genreQuery.Distinct().ToList()),
                Books = books.ToList()
            };

            return View(bookGenreVM);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Title", "Author", "Genre", "Year")] Book book)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            if (ModelState.IsValid) Repository.Add(book);
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

    }
}
