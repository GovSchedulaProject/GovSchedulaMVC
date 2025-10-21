using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Include ViewModels

namespace GovSchedulaWeb.Controllers
{
    public class GraController : Controller
    {
        // GET: /Gra/FileReturns
        [HttpGet]
        public IActionResult FileReturns()
        {
            var viewModel = new TaxFilingViewModel();
            // We could pre-populate the TaxYear dropdown here
            return View(viewModel);
        }

        // POST: /Gra/FileReturns
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FileReturns(TaxFilingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Show form with validation errors
            }
            // TODO: Process Tax Filing data (calculate tax, save, submit to GRA API)
            return RedirectToAction("Index", "Home"); // Placeholder redirect
        }
    }
}