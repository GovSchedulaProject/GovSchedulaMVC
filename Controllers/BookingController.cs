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
                BookingId = "GSPP-ABC123XYZ",
                QrCodeUrl = "~/images/placeholder-qr.png", // Use a placeholder image
                ServiceName = "New Passport Application",
                AppointmentDate = "Tuesday, October 21, 2025",
                AppointmentTime = "10:15 AM",
                OfficeName = "Accra Central Passport Office",
                OfficeAddress = "Kinbu Road, Accra"
            };

            // Pass the data to the View and render it
            return View(viewModel);
        }
    }
}