using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models; // Include the ViewModel

namespace GovSchedulaWeb.Controllers
{
    public class AccountController : Controller
    {
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
            if (!ModelState.IsValid)
            {
                // If required fields are missing, show the form again with errors
                return View(model);
            }

            // --- TODO: Add Actual Login Logic ---
            // Here you would typically:
            // 1. Check the model.LoginIdentifier and model.Password against a database
            //    (using ASP.NET Core Identity or your own user store).
            // 2. If valid, sign the user in (create a session cookie).
            // 3. If invalid, add an error to ModelState and return View(model).
            // Example of adding an error:
            // ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            // return View(model);
            // --- End TODO ---

            // For now, if model is valid, just redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        // --- Add Actions for SignUp later ---
        public IActionResult SignUp()
        {
            // TODO: Create SignUp ViewModel and View
             return RedirectToAction("Index", "Home"); // Placeholder
        }
    }
}