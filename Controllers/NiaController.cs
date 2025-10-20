using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models; // Include ViewModels

namespace GovSchedulaWeb.Controllers
{
    public class NiaController : Controller
    {
        // GET: /Nia/Register
        // Displays the Ghana Card registration form
        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new NiaRegistrationViewModel();
            return View(viewModel);
        }

        // POST: /Nia/Register
        // Handles submission of the registration form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(NiaRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Show form with validation errors
            }
            // TODO: Process NIA registration data
            return RedirectToAction("Index", "Home"); // Placeholder redirect
        }
    }
}