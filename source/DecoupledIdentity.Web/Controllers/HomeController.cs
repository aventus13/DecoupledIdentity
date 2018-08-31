using DecoupledIdentity.Core;
using DecoupledIdentity.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DecoupledIdentity.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Unprotected()
        {
            return View();
        }

        [Authorize]
        public IActionResult Protected()
        {
            return View();
        }

        public IActionResult Unauthorised()
        {
            return View();
        }

        public IActionResult Register()
        {
            HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Username = model.Username,
                Password = model.Password
            };

            await _userManager.CreateAsync(user, model.Password);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> LogIn()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Provided login details are invalid.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return View();
        }
    }
}
