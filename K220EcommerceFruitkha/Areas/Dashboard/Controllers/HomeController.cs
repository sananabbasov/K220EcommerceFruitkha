using Microsoft.AspNetCore.Mvc;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
