using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models.ViewModels
{
    // You can add this class to your ElectoralCommissionViewModel.cs file
    public class VoterRegistrationViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Other Names")]
        public string? OtherNames { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string? DateOfBirth { get; set; } // Using string for placeholder "mm/dd/yyyy"

        [Required]
        [Display(Name = "Place Of Birth")]
        public string? PlaceOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Nationality { get; set; }

        [Required]
        [Display(Name = "Postal Address")]
        public string? PostalAddress { get; set; }

        public string? Occupation { get; set; }

        [Required]
        [Display(Name = "Residential Address")]
        public string? ResidentialAddress { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
    }
}