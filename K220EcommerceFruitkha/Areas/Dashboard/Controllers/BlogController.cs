using K220EcommerceFruitkha.Areas.Dashboard.ViewModels;
using K220EcommerceFruitkha.Data;
using K220EcommerceFruitkha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var blogs = _context.Blogs.ToList();
            var tags = _context.Tags.ToList();
           
            BlogVM vm = new()
            {
                Blogs = blogs,
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Tags"] = await _context.Tags.Where(x=>x.IsDeleted == false).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            return View();
        }
    }
}
