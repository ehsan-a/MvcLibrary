using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View(Repository.GetAll());
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
