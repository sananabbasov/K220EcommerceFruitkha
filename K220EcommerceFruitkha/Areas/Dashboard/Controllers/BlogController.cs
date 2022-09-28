using K220EcommerceFruitkha.Areas.Dashboard.ViewModels;
using K220EcommerceFruitkha.Data;
using K220EcommerceFruitkha.Helper;
using K220EcommerceFruitkha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;

        public BlogController(AppDbContext context, IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _env = env;
            _contextAccessor = contextAccessor;
        }


        public IActionResult Index()
        {
            var blogs = _context.Blogs.Include(x=>x.User).ToList();
            var blogTags = _context.BlogTags.Include(x=>x.Tag).ToList();
            
            BlogVM vm = new()
            {
                Blogs = blogs,
                BlogTags = blogTags,
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
        public async Task<IActionResult> Create(Blog blog, int[] tagIds, IFormFile NewPhoto)
        {
            try
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                blog.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _env);
                blog.CreatedDate = DateTime.Now;
                blog.UpdatedDate = DateTime.Now;
                blog.UserId = userId;
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
                for (int i = 0; i < tagIds.Length; i++) 
                {
                    BlogTag blogTag = new()
                    {
                        BlogId = blog.Id,
                        TagId = tagIds[i]
                    };
                    await _context.BlogTags.AddAsync(blogTag); 
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
         
        }
    }
}
