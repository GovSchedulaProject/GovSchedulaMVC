namespace GovSchedulaWeb.Models
{   
    public class PassportApplicationViewModel
    {
        // Personal Details
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherNames { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; } // Consider using DateTime later
        public string? PlaceOfBirth { get; set; }

        // Biographical & Contact
        public string? Profession { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Height { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? ResidentialAddress { get; set; }

        // Parental Info
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }

        // Emergency Contact
        public string? EmergencyName { get; set; }
        public string? EmergencyPhone { get; set; }

        // Add lists for dropdowns if needed later
        // public List<SelectListItem> GenderOptions { get; set; } = new();
        // public List<SelectListItem> MaritalStatusOptions { get; set; } = new();
    }
    public class PassportReplacementViewModel
    {
        public string? OldPassportNumber { get; set; }
        public string? PoliceReportNumber { get; set; } // Optional field
        public string? ReasonForReplacement { get; set; }
    }

    public class PassportRenewalViewModel
    {
        // Properties needed for initial verification
        public string? OldPassportNumber { get; set; }
        public string? PhoneNumber { get; set; }
    }

    // ViewModel for the Identity Verification step
    public class VerifyIdentityViewModel
    {
        // We might pass the OldPassportNumber or a temporary ID here later
        public string? OldPassportNumber { get; set; }
        public string? Message { get; set; } // To display instructions
    }
}