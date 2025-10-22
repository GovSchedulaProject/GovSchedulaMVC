using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.Data.ViewModels;
using GovScedulaTrial.Models.Data.Services; // Include the ViewModel

namespace GovSchedulaWeb.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: /Account/Login
        // Displays the login form
        [HttpGet] // Explicitly state it handles GET requests
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        // POST: /Account/Login
        // Handles the login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _accountService.LogIn(model.Email, model.Password);

            if (user != null)
            {
                // Optionally set a session or cookie here
                //HttpContext.Session.SetString("UserName", user.Email);
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("Email", user.Email);

                // Redirect to homepage or profile page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }
        }

        // --- ADD SignUp Actions ---

        // GET: /Account/SignUp
        // Displays the sign up form
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        // POST: /Account/SignUp
        // Handles the sign up form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _accountService.SignUp(model);
                    if (result)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                }
               
                throw new Exception();
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }
    }
}