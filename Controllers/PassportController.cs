using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services;
using GovSchedulaWeb.Models.ViewModels;
using GovSchedulaWeb.Services; 
using Microsoft.AspNetCore.Mvc;

namespace GovSchedulaWeb.Controllers
{
    public class PassportController : Controller
    {
        private readonly PassportService _passportService;
        private readonly IEmailService _emailService;

        public PassportController(PassportService passportService, IEmailService emailService)
        {
            _passportService = passportService;
            _emailService = emailService; 
        }

        // GET: /Passport/Create
        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please log in to continue.";
                return RedirectToAction("Login", "Account");
            }

            var model = new PassportApplicationViewModel
            {
                PassportRegistration = new PassportRegistration(),
                GeneralDetail = new GeneralDetail(),
                Family = new Family()
            };
            return View(model);
        }

        // POST: /Passport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassportApplicationViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            // Remove validation errors for unused identity proof types
            RemoveUnusedIdentityProofErrors(model.SelectedIdentityProofType);

            // Remove navigation property errors
            RemoveNavigationPropertyErrors();

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Field: {error.Key} - Error: {subError.ErrorMessage}");
                    }
                }
                return View(model);
            }

            try
            {
                await _passportService.AddPassportAsync(model, userId.Value);

                var applicantEmail = model.GeneralDetail.Email;
                var applicantName = $"{model.GeneralDetail.FirstName} {model.GeneralDetail.LastName}";
                var bookingDate = DateTime.Now; // You can replace this with your booking date field if available

                await _emailService.SendBookingConfirmationEmailAsync(
                    applicantEmail,
                    applicantName,
                    "Passport",
                    bookingDate
                );

                TempData["SuccessMessage"] = "Passport application submitted successfully!";
                return RedirectToAction("Success");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Passport Create: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                ModelState.AddModelError("", "An error occurred while saving your data. Please try again.");
                return View(model);
            }
        }

        // GET: /Passport/Success
        public IActionResult Success()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (TempData["SuccessMessage"] == null)
            {
                return RedirectToAction("Create");
            }

            return View();
        }

        #region Helper Methods

        private void RemoveUnusedIdentityProofErrors(string selectedType)
        {
            var allTypes = new Dictionary<string, string>
            {
                { "GhanaCard", "GhanaCard" },
                { "BirthCertificate", "BirthCertificate" },
                { "VoterId", "VoterId" },
                { "NHIS", "Nhis" },
                { "Guarantor", "Guarantor" }
            };

            foreach (var type in allTypes)
            {
                if (type.Key != selectedType)
                {
                    RemoveModelStateErrorsForPrefix(type.Value);
                }
            }
        }

        private void RemoveNavigationPropertyErrors()
        {
            var navigationProperties = new[]
            {
                "GeneralDetail.User",
                "GeneralDetail.Department",
                "GeneralDetail.IdentityProofNavigation",
                "PassportRegistration.GeneralDetails",
                "PassportRegistration.Family"
            };

            foreach (var prop in navigationProperties)
            {
                ModelState.Remove(prop);
            }
        }

        private void RemoveModelStateErrorsForPrefix(string prefix)
        {
            var keysToRemove = ModelState.Keys
                .Where(k => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }
        }

        #endregion
    }
}
