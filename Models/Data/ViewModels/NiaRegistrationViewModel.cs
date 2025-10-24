using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models.ViewModels
{
    public class NiaApplicationViewModel
    {
        // Identity Proof Selection
        [Required(ErrorMessage = "Please select an identity proof type")]
        [Display(Name = "Identity Proof Type")]
        public string SelectedIdentityProofType { get; set; } = string.Empty;

        public List<SelectListItem> IdentityProofTypes { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "-- Select Identity Proof --" },
            new SelectListItem { Value = "GhanaCard", Text = "Ghana Card" },
            new SelectListItem { Value = "BirthCertificate", Text = "Birth Certificate" },
            new SelectListItem { Value = "VoterId", Text = "Voter ID" },
            new SelectListItem { Value = "NHIS", Text = "NHIS Card" },
            new SelectListItem { Value = "Guarantor", Text = "Guarantor" }
        };

        // Main Entities (Required)
        [Required]
        public GhanaCardRegistration GhanaCardRegistration { get; set; } = new();

        [Required]
        public GeneralDetail? GeneralDetail { get; set; } = new();

        // Optional: Family Information
        public Family? Family { get; set; } = new();

        // Identity Proof Objects (only one will be populated based on selection)
        public GhanaCard? GhanaCard { get; set; }
        public BirthSet? BirthCertificate { get; set; }
        public VoterId? VoterId { get; set; }
        public Nhi? Nhis { get; set; }
        public Garantor? Guarantor { get; set; }
    }
}