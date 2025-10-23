using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services;
using GovSchedulaWeb.Models.Data.ViewModels;
using GovSchedulaWeb.Services; // For List
using Microsoft.AspNetCore.Mvc;
using System; // For DateTime
using System.Collections.Generic;

namespace GovSchedulaWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        private readonly IEmailService _emailService;

        public AdminController(AdminService adminService, IEmailService emailService)
        {
            _adminService = adminService;
            _emailService = emailService;
        }

        //Loging in as Admin

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var admin = await _adminService.AuthenticateAsync(model.Ssn, model.Password);

            if (admin != null)
            {
                // You can store data in session
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                HttpContext.Session.SetInt32("DepartmentId", admin.DepartmentId);
                HttpContext.Session.SetString("Ssn", admin.Ssn.ToString());

                // Redirect to dashboard or review page
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.ErrorMessage = "Invalid SSN or Password";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        //Admin Dashboard

        public async Task<IActionResult> Dashboard()
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Login", "Admin");

            var admin = await _adminService.GetAdminByIdAsync(adminId.Value);
            if (admin == null)
                return RedirectToAction("Login", "Admin");

            var model = await _adminService.GetDashboardDataAsync(admin.DepartmentId);
            return View(model);
        }

        public async Task<IActionResult> ApplicationReview(int departmentId, int? statusId)
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Login", "Admin");

            var admin = await _adminService.GetAdminByIdAsync(adminId.Value);
            if (admin == null)
                return RedirectToAction("Login", "Admin");

            departmentId = admin.DepartmentId;


            if (departmentId == 2)
            {
                var applications = await _adminService.GetVoterApplicationsByDepartmentAsync(departmentId, statusId);
                var statuses = await _adminService.GetAllStatusesAsync();

                var model = new AdminApplicationsViewModel
                {
                    DepartmentId = departmentId,
                    Applications = applications,
                    Statuses = statuses
                };

                return View(model);
            }
            else if (departmentId == 1)
            {
                var applications = await _adminService.GetPassportApplicationsByDepartmentAsync(departmentId, statusId);
                var statuses = await _adminService.GetAllStatusesAsync();

                var model = new AdminApplicationsViewModel
                {
                    DepartmentId = departmentId,
                    Applications = applications,
                    Statuses = statuses
                };

                return View(model);
            }

            return RedirectToAction("Login", "Admin");

        }

        public async Task<IActionResult> ReviewDetails(int departmentId, int Id)
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Login", "Admin");

            var admin = await _adminService.GetAdminByIdAsync(adminId.Value);
            if (admin == null)
                return RedirectToAction("Login", "Admin");

            departmentId = admin.DepartmentId;


            if (departmentId == 2)
            {
                var application = await _adminService.GetVoterApplicationDetailsAsync(Id);
                if (application == null) return NotFound();

                var statuses = await _adminService.GetAllStatusesAsync();

                var model = new AdminReviewPageViewModel
                {
                    Application = application,
                    Statuses = statuses
                };

                return View(model);
            }
            else if (departmentId == 1)
            {
                var application = await _adminService.GetPassportApplicationDetailsAsync(Id);
                if (application == null) return NotFound();

                var statuses = await _adminService.GetAllStatusesAsync();

                var model = new AdminReviewPageViewModel
                {
                    Application = application,
                    Statuses = statuses
                };

                return View(model);
            }
            return RedirectToAction("Login", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int applicationId, int statusId, int departmentId, string? reason, DateTime? appointmentDate, string? appointmentTime)
        {
            bool result = false;
            string departmentName = departmentId == 1 ? "Passport" : "Voter ID";

            // Fetch applicant details for email
            var application = departmentId == 1
                ? await _adminService.GetPassportApplicationDetailsAsync(applicationId)
                : await _adminService.GetVoterApplicationDetailsAsync(applicationId);

            if (application == null)
            {
                TempData["Error"] = "Application not found.";
                return RedirectToAction("ApplicationReview", new { departmentId });
            }

            // Update DB
            if (departmentId == 1)
                result = await _adminService.UpdatePassportApplicationStatusAsync(applicationId, statusId);
            else if (departmentId == 2)
                result = await _adminService.UpdateVoterApplicationStatusAsync(applicationId, statusId);

            if (!result)
            {
                TempData["Error"] = "Unable to update status. Please try again.";
                return RedirectToAction("ApplicationReview", new { departmentId });
            }

            // Send email based on status
            var applicantName = $"{application.GeneralDetails.FirstName} {application.GeneralDetails.LastName}";
            var applicantEmail = application.GeneralDetails.Email;

            if (statusId == 2) // Approved
            {
                await _emailService.SendApprovalEmailAsync(applicantEmail, applicantName, departmentName, appointmentDate, appointmentTime);
            }
            else if (statusId == 3 && !string.IsNullOrWhiteSpace(reason)) // Rejected
            {
                await _emailService.SendRejectionEmailAsync(applicantEmail, applicantName, departmentName, reason);
            }

            TempData["Success"] = "Status updated and email notification sent!";
            return RedirectToAction("ApplicationReview", new { departmentId });
        }

    }
}