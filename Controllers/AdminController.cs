using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Use the correct namespace
using System; // For DateTime
using System.Collections.Generic; // For List

namespace GovSchedulaWeb.Controllers
{
    // TODO: Add [Authorize(Roles = "Admin, Staff")] attribute later for security
    public class AdminController : Controller
    {
        // GET: /Admin/Dashboard or /Admin
        public IActionResult Dashboard()
        {
            // --- MOCK DATA ---
            var mockQueue = new List<QueueItemViewModel> {
                new QueueItemViewModel { LiveQueueToken = "P001", ServiceName = "New Passport", Status = "Waiting", CheckedInTime = "09:15 AM"},
                new QueueItemViewModel { LiveQueueToken = "P002", ServiceName = "Passport Renewal", Status = "Waiting", CheckedInTime = "09:21 AM"},
                new QueueItemViewModel { LiveQueueToken = "D001", ServiceName = "New Driver's License", Status = "Waiting", CheckedInTime = "09:25 AM"},
            };
            // --- END MOCK DATA ---

            var viewModel = new AdminDashboardViewModel
            {
                CurrentDate = DateTime.Now.ToString("dddd, MMMM dd, yyyy"),
                LiveQueue = mockQueue,
                WaitingCount = mockQueue.Count(q => q.Status == "Waiting")
            };

            return View(viewModel);
        }

        // GET: /Admin/Scanner
        [HttpGet] // Explicitly state it handles GET requests
        public IActionResult Scanner()
        {
            var viewModel = new ScannerViewModel
            {
                StatusMessage = "Position QR code within the frame."
            };
            return View(viewModel);
        }

        // POST action can be added later to handle the scanned data submission
        /*
        [HttpPost]
        public IActionResult ProcessScan(string scannedData)
        {
        // TODO: Validate scannedData (Booking ID)
        // TODO: Call backend logic to check-in user
        // TODO: Return JSON result (success/failure) or redirect
        }
        */
    }
}