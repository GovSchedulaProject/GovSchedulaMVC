using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.Services;
using GovSchedulaWeb.Models.Data.ViewModels;
using GovSchedulaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        // ====================== LOGIN ======================

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var admin = await _adminService.AuthenticateAsync(model.Ssn, model.Password);

            if (admin != null)
            {
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                HttpContext.Session.SetInt32("DepartmentId", admin.DepartmentId);
                HttpContext.Session.SetString("Ssn", admin.Ssn.ToString());

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

        // ====================== DASHBOARD ======================

        public async Task<IActionResult> Dashboard()
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Login");

            var admin = await _adminService.GetAdminByIdAsync(adminId.Value);
            if (admin == null)
                return RedirectToAction("Login");

            var model = await _adminService.GetDashboardDataAsync(admin.DepartmentId);
            return View(model);
        }

        // ====================== APPLICATION REVIEW ======================

        public async Task<IActionResult> ApplicationReview(int departmentId, int? statusId)
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Login");

            var admin = await _adminService.GetAdminByIdAsync(adminId.Value);
            if (admin == null)
                return RedirectToAction("Login");

            departmentId = admin.DepartmentId;

            var statuses = await _adminService.GetAllStatusesAsync();
            List<AdminViewModel> applications = new();

            if (departmentId == 1)
                applications = await _adminService.GetPassportApplicationsByDepartmentAsync(departmentId, statusId);
            else if (departmentId == 2)
                applications = await _adminService.GetVoterApplicationsByDepartmentAsync(departmentId, statusId);
            else if (departmentId == 3)
                applications = await _adminService.GetNiaApplicationsByDepartmentAsync(departmentId, statusId);

            var model = new AdminApplicationsViewModel
            {
                DepartmentId = departmentId,
                Applications = applications,
                Statuses = statuses
            };

            return View(model);
        }

        // ====================== REVIEW DETAILS ======================

        public async Task<IActionResult> ReviewDetails(int departmentId, int id)
        {
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Login");

            var admin = await _adminService.GetAdminByIdAsync(adminId.Value);
            if (admin == null)
                return RedirectToAction("Login");

            departmentId = admin.DepartmentId;

            var statuses = await _adminService.GetAllStatusesAsync();
            AdminViewModel? application = null;

            if (departmentId == 1)
                application = await _adminService.GetPassportApplicationDetailsAsync(id);
            else if (departmentId == 2)
                application = await _adminService.GetVoterApplicationDetailsAsync(id);
            else if (departmentId == 3)
                application = await _adminService.GetNiaApplicationDetailsAsync(id);

            if (application == null)
                return NotFound();

            var model = new AdminReviewPageViewModel
            {
                Application = application,
                Statuses = statuses
            };

            return View(model);
        }

        // ====================== UPDATE STATUS ======================

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(
            int applicationId,
            int statusId,
            int departmentId,
            string? reason,
            DateTime? appointmentDate,
            string? appointmentTime)
        {
            bool result = false;
            string departmentName = departmentId switch
            {
                1 => "Passport",
                2 => "Voter ID",
                3 => "Ghana Card",
                _ => "Unknown"
            };

            AdminViewModel? application = departmentId switch
            {
                1 => await _adminService.GetPassportApplicationDetailsAsync(applicationId),
                2 => await _adminService.GetVoterApplicationDetailsAsync(applicationId),
                3 => await _adminService.GetNiaApplicationDetailsAsync(applicationId),
                _ => null
            };

            if (application == null)
            {
                TempData["Error"] = "Application not found.";
                return RedirectToAction("ApplicationReview", new { departmentId });
            }

            result = departmentId switch
            {
                1 => await _adminService.UpdatePassportApplicationStatusAsync(applicationId, statusId),
                2 => await _adminService.UpdateVoterApplicationStatusAsync(applicationId, statusId),
                3 => await _adminService.UpdateNiaApplicationStatusAsync(applicationId, statusId),
                _ => false
            };

            if (!result)
            {
                TempData["Error"] = "Unable to update status. Please try again.";
                return RedirectToAction("ApplicationReview", new { departmentId });
            }

            var applicantName = $"{application.GeneralDetails.FirstName} {application.GeneralDetails.LastName}";
            var applicantEmail = application.GeneralDetails.Email;

            if (statusId == 2) // Approved
                await _emailService.SendApprovalEmailAsync(applicantEmail, applicantName, departmentName, appointmentDate, appointmentTime);
            else if (statusId == 3 && !string.IsNullOrWhiteSpace(reason)) // Rejected
                await _emailService.SendRejectionEmailAsync(applicantEmail, applicantName, departmentName, reason);

            TempData["Success"] = "Status updated and email notification sent!";
            return RedirectToAction("ApplicationReview", new { departmentId });
        }
    }
}
