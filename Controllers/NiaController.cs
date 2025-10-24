using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services;
using GovSchedulaWeb.Models.ViewModels;
using GovSchedulaWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace GovSchedulaWeb.Controllers
{
    public class NiaController : Controller
    {
        private readonly NiaService _niaService;
        private readonly IEmailService _emailService;

        public NiaController(NiaService niaService, IEmailService emailService)
        {
            _niaService = niaService;
            _emailService = emailService;
        }

        // GET: /Nia/Register
        [HttpGet]
        public IActionResult Register()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please log in to continue.";
                return RedirectToAction("Login", "Account");
            }

            var model = new NiaApplicationViewModel
            {
                GhanaCardRegistration = new GhanaCardRegistration(),
                GeneralDetail = new GeneralDetail(),
                Family = new Family()
            };

            return View(model);
        }

        // POST: /Nia/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(NiaApplicationViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

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
                // Save registration details via NiaService
                await _niaService.AddNiaAsync(model, userId.Value);

                var applicantEmail = model.GeneralDetail?.Email;
                var applicantName = (model.GeneralDetail != null)
                    ? $"{model.GeneralDetail.FirstName} {model.GeneralDetail.LastName}".Trim()
                    : null;

                // Fallback to session values if the model didn't provide them
                if (string.IsNullOrWhiteSpace(applicantEmail))
                    applicantEmail = HttpContext.Session.GetString("UserEmail");

                if (string.IsNullOrWhiteSpace(applicantName))
                    applicantName = HttpContext.Session.GetString("Username");

                // Only attempt send if we have an email
                if (!string.IsNullOrWhiteSpace(applicantEmail))
                {
                    try
                    {
                        // small defensive defaults
                        var deptName = "National Identification Authority";
                        var bookingDate = DateTime.Now;

                        await _emailService.SendBookingConfirmationEmailAsync(
                            applicantEmail,
                            string.IsNullOrWhiteSpace(applicantName) ? "Applicant" : applicantName,
                            deptName,
                            bookingDate
                        );
                        Console.WriteLine($"Booking confirmation sent to {applicantEmail}");
                    }
                    catch (Exception ex)
                    {
                        // Log the error but don't break registration flow
                        Console.WriteLine($"Failed to send booking confirmation email to {applicantEmail}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("No applicant email available in model or session — skipping confirmation email.");
                }
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
                Console.WriteLine($"Error in NIA Register: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                ModelState.AddModelError("", "An error occurred while saving your data. Please try again.");
                return View(model);
            }
        }

        // ✅ Success Page
        public IActionResult Success()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (TempData["SuccessMessage"] == null)
                return RedirectToAction("Register");

            return View();
        }
    }
}
