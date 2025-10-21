using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Include ViewModels

namespace GovSchedulaWeb.Controllers
{
    public class DvlaController : Controller
    {
        // --- New Driver's License ---
        [HttpGet]
        public IActionResult NewLicense()
        {
            var viewModel = new DvlaNewLicenseViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewLicense(DvlaNewLicenseViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            // TODO: Process New License data
            return RedirectToAction("Index", "Home"); // Placeholder
        }

        // --- License Renewal ---
        [HttpGet]
        public IActionResult RenewLicense()
        {
            var viewModel = new DvlaLicenseRenewalViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RenewLicense(DvlaLicenseRenewalViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            // TODO: Process License Renewal data
            return RedirectToAction("Index", "Home"); // Placeholder
        }

        // --- Vehicle Registration ---
        [HttpGet]
        public IActionResult RegisterVehicle()
        {
            var viewModel = new DvlaVehicleRegistrationViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterVehicle(DvlaVehicleRegistrationViewModel vehicleModel)
        {
            if (!ModelState.IsValid) return View(vehicleModel);
            // TODO: Process Vehicle Registration data
            return RedirectToAction("Index", "Home"); // Placeholder
        }
    }
}