using System.Collections.Generic; // Make sure List is included

namespace GovSchedulaWeb.Models.ViewModels
{
    public class ServiceViewModel
    {
        // Add '?' to make string properties nullable
        public string? Name { get; set; }
        public string? Link { get; set; }
    }
    
    // Add this class to the file
public class BookingConfirmationViewModel
{
    public string? BookingId { get; set; }
    public string? QrCodeUrl { get; set; } // We'll use a placeholder URL/image for now
    public string? ServiceName { get; set; }
    public string? AppointmentDate { get; set; } // e.g., "Tuesday, October 21, 2025"
    public string? AppointmentTime { get; set; } // e.g., "10:15 AM"
    public string? OfficeName { get; set; } // e.g., "Accra Central Passport Office"
    public string? OfficeAddress { get; set; }
}

    public class DepartmentViewModel
    {
        // Add '?' to make string properties nullable
        public string? Id { get; set; } 
        public string? Name { get; set; } 
        public string? Hours { get; set; } 
        public string? LogoUrl { get; set; } 

        // Initialize List properties with '= new()'
        public List<ServiceViewModel> Services { get; set; } = new List<ServiceViewModel>(); 
    }

    public class DepartmentSelectViewModel
    {
        // Initialize List properties with '= new()'
        public List<DepartmentViewModel> Departments { get; set; } = new List<DepartmentViewModel>();
    }

    // Add this class
    public class SelectSlotViewModel
    {
        public string? ServiceName { get; set; } // e.g., "New Passport Application"
        // TODO: Add properties to hold the application data being booked (or an ID)

        // --- Mock Data for Available Slots ---
        public List<string> AvailableDates { get; set; } = new List<string> { "Monday, Oct 27", "Tuesday, Oct 28", "Wednesday, Oct 29" };
        public List<string> AvailableTimes { get; set; } = new List<string> { "09:00 AM", "09:30 AM", "10:00 AM", "10:30 AM", "..." };
        public List<string> AvailableOffices { get; set; } = new List<string> { "Accra Central Office", "Tema Branch Office", "Kumasi Regional Office" };
        // --- End Mock Data ---

        // Properties to capture user's selection
        public string? SelectedDate { get; set; }
        public string? SelectedTime { get; set; }
        public string? SelectedOffice { get; set; }
    }
}