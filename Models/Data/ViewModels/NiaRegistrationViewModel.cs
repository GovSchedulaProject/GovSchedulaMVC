using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models.Data.ViewModels
{
    public class NiaRegistrationViewModel
    {
        // Similar fields to Passport Application might be needed
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Other Names")]
        public string? OtherNames { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string? DateOfBirth { get; set; } // Consider DateTime

        [Required]
        [Display(Name = "Place Of Birth")]
        public string? PlaceOfBirth { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Residential Address")]
        public string? ResidentialAddress { get; set; }

        // Add other potentially required fields:
        // Nationality, Digital Address, Parent Info, etc.
    }
}