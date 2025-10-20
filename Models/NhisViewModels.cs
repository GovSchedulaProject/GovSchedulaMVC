using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models
{
    // ViewModel for NEW NHIS Registration
    public class NhisRegistrationViewModel
    {
        [Required]
        [Display(Name = "Ghana Card Number")]
        public string? GhanaCardNumber { get; set; }

        [Required]
        [Display(Name = "Full Name (as on Ghana Card)")]
        public string? FullName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string? DateOfBirth { get; set; } // Consider DateTime

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        // Add other necessary fields like Address, Region, District etc.
    }

    // ViewModel for NHIS Membership RENEWAL
    public class NhisRenewalViewModel
    {
        [Required]
        [Display(Name = "NHIS Membership Number")]
        public string? NhisNumber { get; set; }

        [Required]
        [Display(Name = "Phone Number (Linked to Membership)")]
        public string? PhoneNumber { get; set; }
    }
}