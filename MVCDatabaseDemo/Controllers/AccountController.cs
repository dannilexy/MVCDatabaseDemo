using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCDatabaseDemo.Models;
using MVCDatabaseDemo.Models.ViewModels;

namespace MVCDatabaseDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check if user exists
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                //Log user in 
                var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (loginResult.Succeeded)
                {
                    //redirect to dashboard
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please correct the errors and try again.";
                return View(model);
            }


            //Create a new user
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                DOB = model.DOB,
                Gender = model.Gender,
                Occupation = model.Occupation,
                NormalizedUserName = model.Email.ToUpper(),
                Address = model.Address,
                EmailConfirmed = true, // Set to true if you want to skip email confirmation
                PhoneNumber = model.PhoneNumber,
                NormalizedEmail = model.Email.ToUpper(),
            };

            //change password

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //Log user in after registration
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(Login)); // Redirect to home or dashboard
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewBag.ErrorMessage = "Registration failed. Please correct the errors and try again.";
                return View(model);
            }
        }
    }
}
