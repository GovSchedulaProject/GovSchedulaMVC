using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models.Data.ViewModels
{
    // ViewModel for NEW Driver's License Application
    public class DvlaNewLicenseViewModel
    {
        [Required]
        [Display(Name = "Ghana Card Number")]
        public string? GhanaCardNumber { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string? DateOfBirth { get; set; } // Consider DateTime

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        // Add other necessary fields (e.g., Address, Vision Test Results ID)
    }

    // ViewModel for Driver's License RENEWAL
    public class DvlaLicenseRenewalViewModel
    {
        [Required]
        [Display(Name = "Driver's License Number")]
        public string? LicenseNumber { get; set; }

        [Required]
        [Display(Name = "Ghana Card Number")]
        public string? GhanaCardNumber { get; set; }
    }

    // ViewModel for NEW Vehicle Registration
    public class DvlaVehicleRegistrationViewModel
    {
        [Required]
        [Display(Name = "Owner's Ghana Card Number")]
        public string? OwnerGhanaCardNumber { get; set; }

        [Required]
        [Display(Name = "Vehicle Identification Number (VIN)")]
        public string? Vin { get; set; }

        [Required]
        [Display(Name = "Vehicle Make")]
        public string? Make { get; set; }

        [Required]
        [Display(Name = "Vehicle Model")]
        public string? Model { get; set; }

        [Required]
        [Display(Name = "Year of Manufacture")]
        public string? Year { get; set; }

        // Add other fields (Engine Number, Chassis Number, Color, etc.)
    }
}