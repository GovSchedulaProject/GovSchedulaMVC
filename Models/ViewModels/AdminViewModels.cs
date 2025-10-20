using System.Collections.Generic; // For List

namespace GovSchedulaWeb.Models.ViewModels
{
    // Represents one person currently in the live queue
    public class QueueItemViewModel
    {
        public string? LiveQueueToken { get; set; } // e.g., "A001"
        public string? ServiceName { get; set; }
        public string? CitizenName { get; set; } // Optional, might fetch later
        public string? Status { get; set; } // e.g., "Waiting", "Serving"
        public string? CheckedInTime { get; set; } // e.g., "10:35 AM"
    }

    // Represents the data needed for the dashboard view
    public class AdminDashboardViewModel
    {
        public string? CurrentDate { get; set; }
        // We'll populate this list with mock data for now
        public List<QueueItemViewModel> LiveQueue { get; set; } = new List<QueueItemViewModel>();
        public int WaitingCount { get; set; }
        // Add other stats if needed (e.g., AverageWaitTime)
    }

    public class ScannerViewModel
    {
    public string? StatusMessage { get; set; } // e.g., "Ready to scan", "QR Code Invalid"
    }
}