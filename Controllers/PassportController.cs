using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models; // Include the ViewModel

namespace GovSchedulaWeb.Controllers
{
    public class PassportController : Controller
    {
        // GET: /Passport/Create
        // This action displays the blank form for a new application


        // GET: /Passport/VerifyIdentity
        // Displays the identity verification placeholder page
        [HttpGet]
        public IActionResult VerifyIdentity(/* We might pass an ID here later */)
        {
            var viewModel = new VerifyIdentityViewModel
            {
            Message = "Placeholder for identity verification step (e.g., upload photo, answer questions)."
            // Pass any needed data from the previous step if required
            };
            return View(viewModel);
        }
        public IActionResult Create()
        {
            var viewModel = new PassportApplicationViewModel();
            // We could pre-populate dropdown options here if needed
            return View(viewModel);
        }

        // POST: /Passport/Create
        // This action will handle the form submission later
        [HttpPost]
        [ValidateAntiForgeryToken] // Security measure
        public IActionResult Create(PassportApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, show the form again with errors
                return View(model);
            }

            // TODO: Process the valid data (save to database, etc.)
            // For now, just redirect somewhere (e.g., a success page or back home)
            return RedirectToAction("ReviewDetails");
        }

        // --- Add Actions for Renewal and Replacement later ---
        public IActionResult Renew()
        {
            var viewModel = new PassportRenewalViewModel();
            return View(viewModel); // Pass the blank model to the view
        }
        
        // POST: /Passport/Renew
        // Handles submission of the verification form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Renew(PassportRenewalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        return RedirectToAction("Index", "Home");
        }

        public IActionResult Replace()
        {
            var viewModel = new PassportReplacementViewModel();
            return View(viewModel); // Pass the blank model to the view
        }
        // POST: /Passport/Replace
        // Handles submission of the replacement form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Replace(PassportReplacementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Add logic here to process the replacement request
            // This might involve verifying the old passport number
            // and storing the reason/police report number.

            // For now, redirect home after "submission"
            return RedirectToAction("VerifyIdentity" /*, pass any needed route data here */);
        }

            // Inside PassportController.cs

            // GET: /Passport/ReviewDetails
            // Displays the review page placeholder
        [HttpGet]
        public IActionResult ReviewDetails(/* Pass data via TempData or session */)
        {
            // --- MOCK DATA FOR NOW ---
            // TODO: Retrieve the actual submitted data (e.g., from TempData or Session)
            var viewModel = new ReviewDetailsViewModel
    {
            FirstName = "Mock",
            LastName = "User",
            EmailAddress = "mock@example.com",
            PhoneNumber = "+233 12 345 6789",
            // Populate other fields as needed for display
    };
    // --- END MOCK DATA ---

            return View(viewModel);
        }
    }
}