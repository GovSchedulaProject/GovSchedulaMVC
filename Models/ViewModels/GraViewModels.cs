using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models.ViewModels
{
    public class TaxFilingViewModel
    {
        [Required]
        [Display(Name = "Taxpayer Identification Number (TIN)")]
        public string? Tin { get; set; }

        [Required]
        [Display(Name = "Tax Year")]
        public string? TaxYear { get; set; } // Could use int or DateTime depending on needs

        [Required]
        [Display(Name = "Assessable Income (GHS)")]
        [DataType(DataType.Currency)]
        public decimal? AssessableIncome { get; set; }

        // Add other necessary fields (e.g., reliefs, deductions, tax paid)
    }
}