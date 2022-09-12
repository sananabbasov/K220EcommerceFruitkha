using K220EcommerceFruitkha.Data;
using K220EcommerceFruitkha.Models;
using Microsoft.AspNetCore.Mvc;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x=>x.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
