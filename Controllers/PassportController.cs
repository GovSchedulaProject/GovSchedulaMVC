using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering; // Needed for SelectListItem

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
            TempData.Keep("PassportReviewData");
             
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

       [HttpGet]
        public IActionResult ReviewDetails()
        {
            if (!TempData.ContainsKey("PassportReviewData") || TempData["PassportReviewData"] == null)
            {
                return RedirectToAction("Create"); // Go back to Create if no data
            }
 
            var reviewDataJson = TempData["PassportReviewData"] as string;
            
            // This is the full model with all the form data
            var viewModelFromJson = JsonSerializer.Deserialize<PassportApplicationViewModel>(reviewDataJson ?? "{}");

            if (viewModelFromJson == null)
            {
                 return RedirectToAction("Create");
            }

            // --- THIS IS THE CHANGE ---
            
            // We no longer map to a separate, incomplete view model.
            // We just pass the *full* model directly to the view.
            
            // DELETE ALL THIS:
            // var reviewViewModel = new ReviewDetailsViewModel { ... };

            // KEEP THIS:
            // Keep the original data in TempData for the "Edit" button.
            TempData.Keep("PassportReviewData"); 
                                 
            // SEND THE FULL MODEL:
            return View(viewModelFromJson); // Pass the FULL PassportApplicationViewModel
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitApplication()
        {
            if (!TempData.ContainsKey("PassportReviewData") || TempData["PassportReviewData"] == null)
            {
                return RedirectToAction("Create"); // Go back if no data
            }

            // --- START: NEW LOGIC ---

            // 1. Get the final application data from TempData
            var reviewDataJson = TempData["PassportReviewData"] as string;
            var applicationData = JsonSerializer.Deserialize<PassportApplicationViewModel>(reviewDataJson ?? "{}");

            // 2. TODO: Save 'applicationData' to your database
            // This is where you will add your code to save the
            // applicationData to your database with a
            // status like "Pending Approval".
            //
            // Example:
            // var newApplication = new PassportApplication();
            // newApplication.FirstName = applicationData.FirstName;
            // ... (map all other fields) ...
            // newApplication.Status = "Pending Approval";
            // _context.PassportApplications.Add(newApplication);
            // await _context.SaveChangesAsync();

            // 3. CRITICAL: Clear TempData now that the application is fully submitted.
            // This prevents the user from going back to the "Review" page.
            TempData.Remove("PassportReviewData");

            // 4. Redirect to our new "ApplicationSubmitted" page
            return RedirectToAction("ApplicationSubmitted", "Passport");
            
            // --- END: NEW LOGIC ---

            // The old redirect is no longer used:
            // return RedirectToAction("Confirmation", "Booking");
        }
        
        [HttpGet]
        public IActionResult ApplicationSubmitted()
        {
            // This just displays the static view we created in Step 1.
            return View();
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