using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.Data.ViewModels; // Include ViewModels

namespace GovSchedulaWeb.Controllers
{
    public class ElectoralCommissionController : Controller
    {
        // GET: /ElectoralCommission/Register
        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new VoterRegistrationViewModel();
            return View(viewModel);
        }

        // POST: /ElectoralCommission/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(VoterRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Show form with validation errors
            }
            // TODO: Process Voter Registration data
            return RedirectToAction("Index", "Home"); // Placeholder redirect
        }
    }
}