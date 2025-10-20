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
}