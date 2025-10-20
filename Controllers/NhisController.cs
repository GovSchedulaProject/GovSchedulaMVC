using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Include ViewModels

namespace GovSchedulaWeb.Controllers
{
    public class NhisController : Controller
    {
        // GET: /Nhis/Register
        // Displays the form for new registration
        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new NhisRegistrationViewModel();
            return View(viewModel);
        }

        // POST: /Nhis/Register
        // Handles submission of the new registration form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(NhisRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Show form with validation errors
            }
            // TODO: Process registration data (save, call external API, etc.)
            return RedirectToAction("Index", "Home"); // Placeholder redirect
        }

        // GET: /Nhis/Renew
        // Displays the form for membership renewal
        [HttpGet]
        public IActionResult Renew()
        {
            var viewModel = new NhisRenewalViewModel();
            return View(viewModel);
        }

        // POST: /Nhis/Renew
        // Handles submission of the renewal form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Renew(NhisRenewalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Show form with validation errors
            }
            // TODO: Process renewal data (verify, update status, etc.)
            return RedirectToAction("Index", "Home"); // Placeholder redirect
        }
    }
}