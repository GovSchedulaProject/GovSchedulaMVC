using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels; // Include our ViewModels

namespace GovSchedulaWeb.Controllers
{
    public class BookingController : Controller
    {
        // This method handles GET requests to /Booking or /Booking/Index
        public IActionResult Index()
        {
            // Create mock data (later this will come from a database service)
            var departments = new List<DepartmentViewModel>
            {
                new DepartmentViewModel {
                    Id = "passport", Name = "Department of Passport", Hours = "9:00AM - 4:00PM",
                    LogoUrl = "~/images/passport-logo.png", // Use ~ for root path
                    // Inside BookingController.cs, Index action
                Services = new List<ServiceViewModel> {
                    new ServiceViewModel { Name = "New Passport Application", Link = Url.Action("Create", "Passport")! }, // Correct
                    new ServiceViewModel { Name = "Passport Renewal", Link = Url.Action("Renew", "Passport")! },       // Correct
                    new ServiceViewModel { Name = "Lost/Damaged Replacement", Link = Url.Action("Replace", "Passport")! } // Correct
                    }
                },
                new DepartmentViewModel {
                    Id = "dvla", Name = "Driver and Vehicle License Authority", Hours = "9:00AM - 4:00PM",
                    LogoUrl = "~/images/dvla-logo.png",
                    Services = new List<ServiceViewModel> {
                        new ServiceViewModel { Name = "New Driver's License", Link = Url.Action("NewLicense", "Dvla")! },
                        new ServiceViewModel { Name = "License Renewal", Link = Url.Action("RenewLicense", "Dvla")! },
                        new ServiceViewModel { Name = "Vehicle Registration", Link = Url.Action("RegisterVehicle", "Dvla")! }

                    }
                },
                 new DepartmentViewModel {
                    Id = "nhis", Name = "National Health Insurance Scheme", Hours = "9:00AM - 4:00PM",
                    LogoUrl = "~/images/nhis-logo.png",
                    Services = new List<ServiceViewModel> {
                        new ServiceViewModel { Name = "New Registration", Link = Url.Action("Register", "Nhis")! }, // Should generate /Nhis/Register
                        new ServiceViewModel { Name = "Membership Renewal", Link = Url.Action("Renew", "Nhis")! }    // Should generate /Nhis/Renew
                    }
                },
                new DepartmentViewModel {
                    Id = "ec", Name = "Electoral Commission", Hours = "9:00AM - 4:00PM",
                    LogoUrl = "~/images/electoral-commission-logo.png",
                    Services = new List<ServiceViewModel> {
                        new ServiceViewModel { Name = "Voter Registration", Link = Url.Action("Register", "ElectoralCommission")! }
                    }
                },
                new DepartmentViewModel {
                    Id = "nia", Name = "National Identification Authority", Hours = "9:00AM - 4:00PM",
                    LogoUrl = "~/images/nia-logo.png",
                    Services = new List<ServiceViewModel> {
                        new ServiceViewModel { Name = "Ghana Card Registration", Link = Url.Action("Register", "Nia")! }
                    }
                },
                new DepartmentViewModel {
                    Id = "gra", Name = "Ghana Revenue Authority", Hours = "9:00AM - 4:00PM",
                    LogoUrl = "~/images/gra-logo.png",
                    Services = new List<ServiceViewModel> {
                        new ServiceViewModel { Name = "File Tax Returns", Link = Url.Action("FileReturns", "Gra")! }
                    }
                }
                // Add other departments...
            };

            var viewModel = new DepartmentSelectViewModel
            {
                Departments = departments
            };

            // return the view with the departments list
            return View(viewModel);
        }

        // GET: /Booking/Confirmation/{bookingId} (Example route)
        // Or simply /Booking/Confirmation and pass data via TempData after booking POST
        public IActionResult Confirmation(/* We might pass a bookingId here later */)
        {
            // --- MOCK DATA ---
            // In a real scenario, you'd fetch booking details using an ID
            var viewModel = new BookingConfirmationViewModel
            {
                BookingId = TempData["BookingId"] as string ?? "N/A",
                QrCodeUrl = TempData["QrCodeUrl"] as string ?? "~/images/placeholder-qr.png",
                ServiceName = TempData["ServiceName"] as string ?? "N/A",
                AppointmentDate = TempData["AppointmentDate"] as string ?? "N/A",
                AppointmentTime = TempData["AppointmentTime"] as string ?? "N/A",
                OfficeName = TempData["OfficeName"] as string ?? "N/A",
                OfficeAddress = TempData["OfficeAddress"] as string ?? "N/A" // Add address to TempData if needed
            };

            // Check if BookingId is missing - means user navigated directly?
            if(viewModel.BookingId == "N/A")
            {
                // Maybe redirect home or show an error
                // return RedirectToAction("Index", "Home");
            }

            // Pass the data to the View and render it
            return View(viewModel);
        }

        

        // GET: /Booking/SelectSlot
        // Displays the page for choosing date/time/location
        [HttpGet]
        public IActionResult SelectSlot(/* Pass application data/ID via TempData or route */)
        {
            // TODO: Retrieve application details (e.g., service name) from TempData/Session
            var viewModel = new SelectSlotViewModel
            {
                ServiceName = TempData["ServiceName"] as string ?? "Selected Service" // Example retrieve
                // TODO: Populate AvailableDates/Times/Offices based on actual availability logic
            };
            // Keep data in TempData if needed for the POST
            // TempData.Keep(); 
            return View(viewModel);
        }   

        // POST: /Booking/SelectSlot
        // Handles the submission of the chosen date/time/location
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectSlot(SelectSlotViewModel model)
        {
            if (!ModelState.IsValid)
            {
                 // TODO: Repopulate available slots if needed before returning view
                return View(model); 
            }

            // --- TODO: Final Booking Logic ---
            // 1. Retrieve the full application data (from TempData/Session using an ID).
            // 2. Combine with model.SelectedDate, model.SelectedTime, model.SelectedOffice.
            // 3. Save the *complete* appointment record to the database.
            // 4. Generate the *real* Booking ID and QR Code.
            // 5. Store the final details in TempData for the confirmation page.
            // --- End TODO ---

            // Example storing final details in TempData:
            TempData["BookingId"] = "FINAL-BOOKING-ID-123";
            TempData["QrCodeUrl"] = "~/images/placeholder-qr.png"; // Use real QR later
            TempData["ServiceName"] = model.ServiceName ?? "Booked Service";
            TempData["AppointmentDate"] = model.SelectedDate;
            TempData["AppointmentTime"] = model.SelectedTime;
            TempData["OfficeName"] = model.SelectedOffice;
            // ... store other details ...

            // Redirect to the final confirmation page
            return RedirectToAction("Confirmation", "Booking");
        }
    }
}