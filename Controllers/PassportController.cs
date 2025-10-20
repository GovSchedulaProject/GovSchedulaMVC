using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Include the ViewModel

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
        
        // POST: /Passport/SubmitApplication
        // This action is called AFTER the user confirms details on the review page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitApplication(/* Pass necessary data via hidden fields or TempData */)
        {
        // --- TODO: Actual Backend Logic ---
        // 1. Retrieve the complete application data (from TempData, Session, or hidden form fields).
        // 2. Validate the data one last time.
        // 3. Save the application data to the database.
        // 4. Generate a unique Booking ID.
        // 5. Generate a QR Code for the Booking ID.
        // 6. Save the Booking ID and maybe QR code reference with the application record.
        // --- End TODO ---

        // --- For Now: Redirect to Confirmation Page ---
        // We'll pass mock data for now. Later, we'll pass the *actual* Booking ID
        // and details, maybe using TempData.

        // Example using TempData (requires setup in Program.cs if not default)
        // TempData["BookingId"] = generatedBookingId;
        // TempData["QrCodeUrl"] = generatedQrCodeUrl;
        // TempData["ServiceName"] = "New Passport Application";
        // ... set other details for confirmation page ...

        // Redirect to the Confirmation action in the BookingController
        return RedirectToAction("Confirmation", "Booking"); 
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