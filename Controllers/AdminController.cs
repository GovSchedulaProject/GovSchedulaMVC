using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels;
using GovSchedulaWeb.Services; // <-- 1. ADD THIS USING
using System;
using System.Collections.Generic;
using System.Linq;

namespace GovSchedulaWeb.Controllers
{
    public class AdminController : Controller
    {
        // --- 2. ADD THESE TWO LINES ---
        private readonly IEmailService _emailService;
        // private readonly ApplicationDbContext _context; // (Your DB context will go here later)

        // --- 3. UPDATE THE CONSTRUCTOR ---
        public AdminController(IEmailService emailService) // (Add , ApplicationDbContext context later)
        {
            _emailService = emailService;
            // _context = context;
        }

        // ... (Your Dashboard, ApplicationReview, and ReviewDetails GET actions stay the same) ...

        // GET: /Admin/Dashboard
        public IActionResult Dashboard()
        {
            // ... (code unchanged) ...
            var viewModel = new AdminDashboardViewModel
            {
                CurrentDate = DateTime.Now.ToString("dddd, MMMM dd, yyyy"),
                PendingApplicationsCount = 15,
                AppointmentsTodayCount = 82,
                CitizensWaitingCount = 3,
                AverageWaitTime = "12 min"
            };
            return View(viewModel);
        }

        // GET: /Admin/ApplicationReview
        [HttpGet]
        public IActionResult ApplicationReview()
        {
            // ... (code unchanged) ...
            var pending = new List<ApplicationSummaryItem> {
                new ApplicationSummaryItem { ApplicationId = 101, ApplicantName = "Philip Agyapong", ServiceName = "New Passport", SubmittedOn = DateTime.Now.AddDays(-1), Status = "Pending" },
                new ApplicationSummaryItem { ApplicationId = 102, ApplicantName = "Jane Doe", ServiceName = "Passport Renewal", SubmittedOn = DateTime.Now.AddHours(-5), Status = "Pending" }
            };
            var viewModel = new ApplicationReviewViewModel { PendingApplications = pending };
            return View(viewModel);
        }

        // GET: /Admin/ReviewDetails/101
        [HttpGet]
        public IActionResult ReviewDetails(int id)
        {
             // ... (code unchanged) ...
            var application = new PassportApplicationViewModel {
                FirstName = "Philip", LastName = "Agyapong", DateOfBirth = "11/01/1996",
                FatherName = "Das", MotherName = "Mas",
                GuarantorFullName = "Test Guarantor",
            };
            ViewData["ApplicationId"] = id;
            return View(application);
        }


        // --- 4. UPDATE APPROVE ACTION ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveApplication(int applicationId, DateTime appointmentDate, string appointmentTime)
        {
            // TODO: Get real application from DB
            // var application = _context.PassportApplications.Find(applicationId);
            // if (application == null) return NotFound();

            // --- MOCK DATA (DELETE LATER) ---
            var mockUserEmail = "philipagyapong18@gmail.com"; // <-- Use your real email
            var mockUserName = "Philip"; // Get this from the 'application' object
            // --- END MOCK DATA ---

            // ... (Your logic to save to DB) ...
            
            // 5. Send the approval email
            var subject = "Your Application has been Approved!";
            var message = $@"
                <h1>Booking Confirmed!</h1>
                <p>Dear {mockUserName},</p>
                <p>Your application (ID: {applicationId}) has been approved.</p>
                <p><strong>Your appointment is scheduled for:</strong></p>
                <ul>
                    <li><strong>Date:</strong> {appointmentDate.ToShortDateString()}</li>
                    <li><strong>Time:</strong> {appointmentTime}</li>
                </ul>
                <p>Please keep this email safe. You will need to present your Booking ID (or QR Code) at the service center.</p>
                <p>Thank you,<br>GovSchedula Team</p>";
            
            await _emailService.SendEmailAsync(mockUserEmail, subject, message);
            
            return RedirectToAction("ApplicationReview");
        }

        // --- 5. UPDATE REJECT ACTION ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectApplication(int applicationId, string reason)
        {
            // TODO: Get real application from DB
            // var application = _context.PassportApplications.Find(applicationId);
            // if (application == null) return NotFound();

            // --- MOCK DATA (DELETE LATER) ---
            var mockUserEmail = "philipagyapong18@gmail.com"; // <-- Use your real email
            var mockUserName = "Philip";
            // --- END MOCK DATA ---

            // ... (Your logic to update status in DB) ...
            
            // 4. Send rejection email to the user
            var subject = "Your Application Status";
            var message = $@"
                <h1>Application Update</h1>
                <p>Dear {mockUserName},</p>
                <p>We regret to inform you that your application (ID: {applicationId}) has been rejected.</p>
                <p><strong>Reason:</strong> {reason}</p>
                <p>Please review the reason, make the necessary corrections, and feel free to re-apply.</p>
                <p>Thank you,<br>GovSchedula Team</p>";

            await _emailService.SendEmailAsync(mockUserEmail, subject, message);

            TempData["Message"] = "Application has been rejected.";
            return RedirectToAction("ApplicationReview");
        }

        // --- 6. UPDATE REQUEST INFO ACTION ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestInfo(int applicationId, string message)
        {
            // TODO: Get real application from DB
            // var application = _context.PassportApplications.Find(applicationId);
            // if (application == null) return NotFound();

            // --- MOCK DATA (DELETE LATER) ---
            var mockUserEmail = "philipagyapong18@gmail.com"; // <-- Use your real email
            var mockUserName = "Philip";
            // --- END MOCK DATA ---

            // ... (Your logic to update status in DB) ...

            // 4. Send info request email to the user
            var subject = "Action Required: More Information Needed for Your Application";
            var htmlMessage = $@"
                <h1>Application Update</h1>
                <p>Dear {mockUserName},</p>
                <p>Your application (ID: {applicationId}) requires more information before it can be processed.</p>
                <p><strong>Information Required:</strong> {message}</p>
                <p>Please log in to your portal to provide the requested details.</p>
                <p>Thank you,<br>GovSchedula Team</p>";

            await _emailService.SendEmailAsync(mockUserEmail, subject, message);
            
            TempData["Message"] = "Information request has been sent.";
            return RedirectToAction("ApplicationReview");
        }
        
        // ... (Your LiveQueue and Scanner actions stay the same) ...
        [HttpGet]
        public IActionResult LiveQueue()
        {
            // ... (code unchanged) ...
            var mockQueue = new List<QueueItemViewModel> {
                new QueueItemViewModel { LiveQueueToken = "P001", ServiceName = "New Passport", Status = "Waiting", CheckedInTime = "09:15 AM"},
                new QueueItemViewModel { LiveQueueToken = "P002", ServiceName = "Passport Renewal", Status = "Waiting", CheckedInTime = "09:21 AM"},
                new QueueItemViewModel { LiveQueueToken = "D001", ServiceName = "New Driver's License", Status = "Waiting", CheckedInTime = "09:25 AM"},
            };
            var viewModel = new LiveQueueViewModel
            {
                CurrentDate = DateTime.Now.ToString("dddd, MMMM dd, yyyy"),
                LiveQueue = mockQueue,
                WaitingCount = mockQueue.Count(q => q.Status == "Waiting"),
                AverageWaitTime = "8 min",
                CurrentlyServing = 1
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Scanner()
        {
             // ... (code unchanged) ...
            var viewModel = new ScannerViewModel
            {
                StatusMessage = "Position QR code within the frame."
            };
            return View(viewModel);
        }
    }
}