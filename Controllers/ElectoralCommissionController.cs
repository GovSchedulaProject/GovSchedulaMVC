using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services;
using GovSchedulaWeb.Models.ViewModels;
using GovSchedulaWeb.Services; // 👈 Add this for IEmailService
using Microsoft.AspNetCore.Mvc;

namespace GovSchedulaWeb.Controllers
{
    public class ElectoralCommissionController : Controller
    {
        private readonly VoterRegService _voterRegService;
        private readonly IEmailService _emailService; // 👈 Add this

        public ElectoralCommissionController(VoterRegService voterRegService, IEmailService emailService)
        {
            _voterRegService = voterRegService;
            _emailService = emailService; // 👈 Injected email service
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
        public async Task<IActionResult> Register(VoterRegistrationViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            try
            {
                // Save booking (registration)
                await _voterRegService.AddVoterAsync(model, userId.Value);

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
                        var deptName = "Electoral Commission";
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
                Console.WriteLine($"Error in ElectoralCommission Register: {ex.Message}");
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
                return RedirectToAction("Register");
            }

            return View();
        }
    }
}
