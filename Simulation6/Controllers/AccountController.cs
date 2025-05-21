using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simulation6.Models.LoginRegister;
using Simulation6.ViewModels.AccountVM;

namespace Simulation6.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager,SignInManager<AppUser> signInManager, RoleManager<AppUser> roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> Login()
        //{

        //}
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {

            if (!ModelState.IsValid) return View(vm);

            return View(nameof(Login));
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
    }
}
