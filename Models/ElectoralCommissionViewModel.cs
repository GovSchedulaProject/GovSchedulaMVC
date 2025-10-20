using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models
{
    public class VoterRegistrationViewModel
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
        public string? Gender { get; set; }

        [Required]
        [Display(Name = "Residential Address")]
        public string? ResidentialAddress { get; set; }

        [Required]
        [Display(Name = "Digital Address (GhanaPost GPS)")]
        public string? DigitalAddress { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string? Region { get; set; }

        [Required]
        [Display(Name = "District")]
        public string? District { get; set; }

        [Required]
        [Display(Name = "Constituency")]
        public string? Constituency { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}