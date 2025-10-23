using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GovSchedulaWeb.Models.Data.ViewModels
{
    // -----------------------------------------------------------------
    // NEW: For your "True" Dashboard Landing Page (Dashboard.cshtml)
    // -----------------------------------------------------------------
    public class AdminDashboardViewModel
    {
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }

        public string? DepartmentName { get; set; }
        //public string AverageWaitTime { get; set; } = "N/A";
        //public string CurrentDate { get; set; } = string.Empty;
    }

    // -----------------------------------------------------------------
    // NEW: For your "Application Review" List Page (ApplicationReview.cshtml)
    // -----------------------------------------------------------------
    //public class ApplicationReviewViewModel
    //{
    //    public List<ApplicationSummaryItem> PendingApplications { get; set; } = new();
    //    public List<ApplicationSummaryItem> RecentlyHandledApplications { get; set; } = new();
    //}

    //public class ApplicationSummaryItem
    //{
    //    public int ApplicationId { get; set; } // The Database Primary Key
    //    public string ApplicantName { get; set; } = string.Empty;
    //    public string ServiceName { get; set; } = string.Empty;
    //    public DateTime SubmittedOn { get; set; }
    //    public string Status { get; set; } = string.Empty;
    //}

    // -----------------------------------------------------------------
    // RENAMED: This is your *existing* model for the Live Queue
    // -----------------------------------------------------------------
    // RENAMED: This is your *existing* model for the Live Queue
    // This is your model for the Live Queue
    //public class LiveQueueViewModel
    //{
    //    public string CurrentDate { get; set; } = string.Empty;
    //    public int WaitingCount { get; set; }

    //    // --- THIS IS THE LINE FROM THE ERROR ---
    //    public string AverageWaitTime { get; set; } = "N/A";
    //    // ---

    //    public int CurrentlyServing { get; set; }
    //    public List<QueueItemViewModel> LiveQueue { get; set; } = new();
    //}

    // This is your *existing* sub-model
    //public class QueueItemViewModel 
    //{
    //    public string LiveQueueToken { get; set; } = string.Empty;
    //    public string ServiceName { get; set; } = string.Empty;
    //    public string Status { get; set; } = string.Empty;
    //    public string CheckedInTime { get; set; } = string.Empty;
    //}

    // (Your existing ScannerViewModel can stay the same)
    public class ScannerViewModel
    {
        public string? StatusMessage { get; set; }
    }

    public class AdminViewModel
    {
        public Family Family { get; set; }
        public GhanaCard GhanaCard { get; set; }
        public IdentityProof IdentityProof { get; set; }
        public Adminlogin Adminlogin { get; set; }

        public Department Department { get; set; }
        public GeneralDetail GeneralDetails { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }


        

        public Nhisregistration? Nhisregistration { get; set; }
        public VoterIdregistration? VoterIdregistration { get; set; }
        public PassportRegistration? PassportRegistration { get; set; }
        public GhanaCardRegistration? GhanaCardRegistration { get; set; }
        public DriverLicenceRegistration? DriverLicenceRegistration { get; set; }

        public int MainDepartmentId => VoterIdregistration?.VoterId ?? 
                         PassportRegistration?.PassportId ??
                         GhanaCardRegistration?.GhanaCardId ??
                         Nhisregistration?.Nhisid ??
                         DriverLicenceRegistration?.DriverLicenceId ?? 0;

    }

    public class AdminApplicationsViewModel
    {
        public int DepartmentId { get; set; }
        public List<AdminViewModel> Applications { get; set; } = new();
        public List<ApprovalStatus> Statuses { get; set; } = new();
    }
    public class AdminReviewPageViewModel
    {
        public AdminViewModel Application { get; set; }
        public List<ApprovalStatus> Statuses { get; set; } = new();
    }

    public class AdminLoginViewModel
    {
        [Key]
        public int AdminId { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Social Security Number")]
        public int Ssn { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}