using K220EcommerceFruitkha.Areas.Dashboard.DTOs;
using K220EcommerceFruitkha.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace K220EcommerceFruitkha.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AuthController : Controller
    {
        private readonly UserManager<K220User> _userManager;
        private readonly SignInManager<K220User> _signInManager;
        public AuthController(UserManager<K220User> userManager, SignInManager<K220User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                ViewBag.UserNotFound = "Istifadeci tapilmadi";
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password,false,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            K220User newUser = new()
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerDTO.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View();
        }
    }
}
