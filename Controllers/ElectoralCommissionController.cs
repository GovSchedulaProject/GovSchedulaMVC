using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services; // Include ViewModels
using GovSchedulaWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GovSchedulaWeb.Controllers
{
    public class ElectoralCommissionController : Controller
    {
        private VoterRegService _voterRegService;
        public ElectoralCommissionController(VoterRegService voterRegService)
        {
            _voterRegService = voterRegService;
        }
        // GET: /ElectoralCommission/Register
        [HttpGet]
        public IActionResult Register()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please log in to continue.";
                return RedirectToAction("Login", "Account");
            }

            var model = new VoterRegistrationViewModel
            {
                VoterIdRegistration = new VoterIdregistration(),
                GeneralDetail = new GeneralDetail()
            };
            return View(model);
        }

        // POST: /ElectoralCommission/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(VoterRegistrationViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            // Remove validation errors for unused identity proof types
            //RemoveUnusedIdentityProofErrors(model.SelectedIdentityProofType);

            // Remove navigation property errors
            //RemoveNavigationPropertyErrors();
            //if (!ModelState.IsValid)
            //{
            //    foreach (var error in ModelState)
            //    {
            //        foreach (var subError in error.Value.Errors)
            //        {
            //            Console.WriteLine($"Field: {error.Key} - Error: {subError.ErrorMessage}");
            //        }
            //    }
            //    return View(model);
            //}
            try
            {
                await _voterRegService.AddVoterAsync(model, userId.Value);

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
    }
}