using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        public CategoryController(IRepository<Category> repository)
        {
            _repository = repository as CategoryRepository;
        }
        public IActionResult Index(string searchString)
        {
            var categories = _repository.GetAll().Where(x => x.IsDeleted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
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
            if (ModelState.IsValid) _repository.Add(category);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "Title")] Category category)
        {
            if (id != category.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var preCategory = _repository.GetAll().FirstOrDefault(x => x.Id == id);
                preCategory.Title = category.Title;
            }
            return View(category);
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
            var category = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (category != null) _repository.Delete(category);
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
