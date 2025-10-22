using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.ViewModels
{
    // -----------------------------------------------------------------
    // NEW: For your "True" Dashboard Landing Page (Dashboard.cshtml)
    // -----------------------------------------------------------------
    public class AdminDashboardViewModel
    {
        public int PendingApplicationsCount { get; set; }
        public int AppointmentsTodayCount { get; set; }
        public int CitizensWaitingCount { get; set; }
        public string AverageWaitTime { get; set; } = "N/A";
        public string CurrentDate { get; set; } = string.Empty;
    }

    // -----------------------------------------------------------------
    // NEW: For your "Application Review" List Page (ApplicationReview.cshtml)
    // -----------------------------------------------------------------
    public class ApplicationReviewViewModel
    {
        public List<ApplicationSummaryItem> PendingApplications { get; set; } = new();
        public List<ApplicationSummaryItem> RecentlyHandledApplications { get; set; } = new();
    }

    public class ApplicationSummaryItem
    {
        public int ApplicationId { get; set; } // The Database Primary Key
        public string ApplicantName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public DateTime SubmittedOn { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    // -----------------------------------------------------------------
    // RENAMED: This is your *existing* model for the Live Queue
    // -----------------------------------------------------------------
    // RENAMED: This is your *existing* model for the Live Queue
    // This is your model for the Live Queue
    public class LiveQueueViewModel
    {
        public string CurrentDate { get; set; } = string.Empty;
        public int WaitingCount { get; set; }
        
        // --- THIS IS THE LINE FROM THE ERROR ---
        public string AverageWaitTime { get; set; } = "N/A";
        // ---

        public int CurrentlyServing { get; set; }
        public List<QueueItemViewModel> LiveQueue { get; set; } = new();
    }
    
    // This is your *existing* sub-model
    public class QueueItemViewModel 
    {
        public string LiveQueueToken { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CheckedInTime { get; set; } = string.Empty;
    }
    
    // (Your existing ScannerViewModel can stay the same)
    public class ScannerViewModel
    {
        public string? StatusMessage { get; set; }
    }
}