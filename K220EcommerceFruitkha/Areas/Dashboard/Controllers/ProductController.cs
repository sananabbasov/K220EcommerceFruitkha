using K220EcommerceFruitkha.Data;
using K220EcommerceFruitkha.Helper;
using K220EcommerceFruitkha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(x => x.Category).ToList();
            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Categories"] = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, int categoryId, IFormFile NewPhoto)
        {
            product.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
            product.CategoryId = categoryId;
            product.CreatedDate = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = _context.Categories.ToList();
            var product = _context.Products.SingleOrDefault(x => x.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile NewPhoto, string OldPhoto)
        {
            ViewData["Categories"] = _context.Categories.ToList();
            try
            {
                if (NewPhoto != null)
                {
                    product.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
                }
                else
                {
                    product.PhotoUrl = OldPhoto;
                }
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            try
            {
                var pro = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
                pro.IsDeleted = true;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
