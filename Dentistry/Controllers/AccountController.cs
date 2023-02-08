using Dentistry.Models;
using Dentistry.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dentistry.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

        [HttpGet]
		public IActionResult Login(string? returnUrl = null)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result =
					await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин и (или) пароль");
				}
			}
			return View(model);
		}
        
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}