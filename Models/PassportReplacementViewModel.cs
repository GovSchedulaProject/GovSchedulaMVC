namespace GovSchedulaWeb.Models
{
    public class PassportReplacementViewModel
    {
        public string? OldPassportNumber { get; set; }
        public string? PoliceReportNumber { get; set; } // Optional field
        public string? ReasonForReplacement { get; set; }
    }
}