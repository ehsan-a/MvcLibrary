using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class CategoryController : Controller
    {
        public IRepository<Category>? Repository { get; set; }
        public CategoryController(IRepository<Category> repository)
        {
            Repository = repository;
        }
        public IActionResult Index(string searchString)
        {
            var categories = Repository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }
            var categoryFilterVM = new FilterViewModel<Category>
            {
                Items = categories.ToList(),
                SearchString = searchString,
            };
            return View(categoryFilterVM);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Title")] Category category)
        {
            if (HttpContext.Session.GetInt32("_userType") != 1)
                return NotFound();
            if (ModelState.IsValid) Repository.Add(category);
            return RedirectToAction("Index");
        }
    }
}
