using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Include the ViewModel

namespace GovSchedulaWeb.Controllers
{
    public class PassportController : Controller
    {
        // GET: /Passport/Create
        // Displays the single-page application form
        [HttpGet]
        public IActionResult Create()
        {
            // If returning from Review edit, load data
            var model = TempData.ContainsKey("PassportReviewData")
                        && TempData["PassportReviewData"] is string reviewDataJson
                        && !string.IsNullOrEmpty(reviewDataJson)
                ? JsonSerializer.Deserialize<PassportApplicationViewModel>(reviewDataJson)
                : new PassportApplicationViewModel();

             if (model == null) model = new PassportApplicationViewModel();
            TempData.Remove("PassportReviewData"); // Clear stale data
             
             model.IdTypeOptions = new List<SelectListItem>
    {
                new SelectListItem { Value = "", Text = "-- Select ID Type --" },
                 new SelectListItem { Value = "Ghana Card", Text = "Ghana Card" },
                new SelectListItem { Value = "Voter ID", Text = "Voter ID" },
                new SelectListItem { Value = "Driver's License", Text = "Driver's License" }
                // Add other relevant ID types
    };

            return View(model); // Renders Create.cshtml

            // --- ADD CODE TO POPULATE DROPDOWN ---
    
        }

        // POST: /Passport/Create
        // Handles submission of the single-page form, goes to Review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PassportApplicationViewModel model)
        {
            // Optional: Server-side validation
            // if (!ModelState.IsValid)
            // {
            //     return View(model);
            // }

            // Store data for the Review page
            TempData["PassportReviewData"] = JsonSerializer.Serialize(model);

            // Go directly to Review Details
            return RedirectToAction("ReviewDetails");
        }

        // --- ReviewDetails, SubmitApplication, Renew, Replace, VerifyIdentity actions remain the same ---
        // Make sure ReviewDetails GET action still uses TempData["PassportReviewData"]
        [HttpGet]
        public IActionResult ReviewDetails()
        {
            if (!TempData.ContainsKey("PassportReviewData") || TempData["PassportReviewData"] == null)
            {
                return RedirectToAction("Create"); // Go back to Create if no data
            }
            // ... rest of ReviewDetails GET action ...
             var reviewDataJson = TempData["PassportReviewData"] as string;
             var viewModelFromJson = JsonSerializer.Deserialize<PassportApplicationViewModel>(reviewDataJson ?? "{}");
             TempData.Keep("PassportReviewData");

             // Map to ReviewDetailsViewModel if needed
             var reviewViewModel = new ReviewDetailsViewModel { /* copy properties */ };

             return View(reviewViewModel); // Pass ReviewDetailsViewModel
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitApplication()
        {
             if (!TempData.ContainsKey("PassportReviewData") || TempData["PassportReviewData"] == null)
            {
                return RedirectToAction("Create"); // Go back if no data
            }
            // ... rest of SubmitApplication POST action ...
            return RedirectToAction("Confirmation", "Booking");
        }

        [HttpGet]
        public IActionResult Renew() { /* ... */ return View(); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Renew(PassportRenewalViewModel model) { /* ... */ return RedirectToAction("Index","Home"); }

        [HttpGet]
        public IActionResult Replace() { /* ... */ return View(); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Replace(PassportReplacementViewModel model) { /* ... */ return RedirectToAction("VerifyIdentity"); }

        [HttpGet]
        public IActionResult VerifyIdentity() { /* ... */ return View(); }

    } // End Controller
} // End Namespace