using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Keep if using SelectListItem later

namespace GovSchedulaWeb.Models.ViewModels
{
    // ViewModel for the main single-step application
    public class PassportApplicationViewModel
    {
        // --- Fields from Figma 1 ---
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Other Names")]
        public string? OtherNames { get; set; }

        public string? Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        public string? DateOfBirth { get; set; }

        [Display(Name = "Place Of Birth")]
        public string? PlaceOfBirth { get; set; }

        // --- Fields from Figma 2 ---
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        // --- ADDED THIS (Ensure it's here) ---
        public string? Nationality { get; set; }

        // --- ADDED THIS (Ensure it's here) ---
        [Display(Name = "Postal Address")]
        public string? PostalAddress { get; set; }

        [Display(Name = "Occupation")]
        public string? Profession { get; set; } // Using Profession field for Occupation

        [Display(Name = "Residential Address")]
        public string? ResidentialAddress { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        // --- Other Fields (Needed for Review/Submission, not shown in initial form images) ---
        [Display(Name = "Marital Status")]
        public string? MaritalStatus { get; set; }

        public string? Height { get; set; }

        [Display(Name = "Father's Full Name")]
        public string? FatherName { get; set; }
        [Display(Name = "Father's Nationality")]
        public string? FatherNationality { get; set; }

        [Display(Name = "Mother's Full Name")]
        public string? MotherName { get; set; }
        [Display(Name = "Mother's Nationality")]
        public string? MotherNationality { get; set; }
        // --- ADD Spousal Info (Mark as optional ?) ---
        [Display(Name = "Spouse's Full Name")]
        public string? SpouseName { get; set; }
        [Display(Name = "Spouse's Nationality")]
        public string? SpouseNationality { get; set; }

        // --- ADD Type of Passport ---
        [Display(Name = "Type of Passport")]
        public string? PassportType { get; set; } // e.g., "Standard", "Diplomat"

        // --- ADD Guarantor Details ---
        [Display(Name = "Full Name")]
        public string? GuarantorFullName { get; set; }

        [Display(Name = "Profession")]
        public string? GuarantorProfession { get; set; }

        [Display(Name = "Phone Number")]
        public string? GuarantorPhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string? GuarantorAddress { get; set; }

        [Display(Name = "Ghana Passport Number")]
        public string? GuarantorPassportNumber { get; set; }

        [Display(Name = "Full Name")]
        public string? EmergencyName { get; set; }

        [Display(Name = "Phone Number")]
        public string? EmergencyPhone { get; set; }

        // --- ADD Identification Details ---
        [Display(Name = "Type of ID Used")]
        public string? IdType { get; set; } // For the dropdown
        [Display(Name = "ID Number")]
        public string? IdNumber { get; set; }

        // --- ADD THE DROPDOWN OPTIONS LIST HERE ---
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> IdTypeOptions { get; set; } = new();
    }

    // ViewModel for the Renewal start page
    public class PassportRenewalViewModel
    {
        [Required]
        [Display(Name = "Old Passport Number")]
        public string? OldPassportNumber { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }

    // ViewModel for the Replacement start page
    public class PassportReplacementViewModel
    {
        [Display(Name = "Old Passport Number")]
        public string? OldPassportNumber { get; set; }

        [Display(Name = "Police Report Number")]
        public string? PoliceReportNumber { get; set; }

        [Required]
        [Display(Name = "Reason for Replacement")]
        public string? ReasonForReplacement { get; set; }
    }

    // ViewModel for the Verify Identity step (placeholder)
    public class VerifyIdentityViewModel
    {
        public string? OldPassportNumber { get; set; }
        public string? Message { get; set; }
    }
} // End Namespace