using K220EcommerceFruitkha.Data;
using K220EcommerceFruitkha.Helper;
using K220EcommerceFruitkha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _context.Sliders.ToListAsync();
            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider, IFormFile NewPhoto)
        {
            try
            {
                slider.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
                slider.CreatedDate = DateTime.Now;
                _context.Sliders.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(x=>x.Id == id);
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider, IFormFile NewPhoto, string OldPhoto)
        {
            try
            {
                if (NewPhoto != null)
                {
                    slider.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
                }
                else
                {
                    slider.PhotoUrl = OldPhoto;
                }
                _context.Sliders.Update(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(x=>x.Id == id);
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Slider slider)
        {
            var deletedSlider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == slider.Id);
            deletedSlider.IsDeleted = true;
            _context.Sliders.Update(deletedSlider);    
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
