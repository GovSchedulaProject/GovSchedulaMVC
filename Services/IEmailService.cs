namespace GovSchedulaWeb.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlContent);
        Task SendApprovalEmailAsync(string toEmail, string applicantName, string departmentName, DateTime? appointmentDate, string? appointmentTime);
        Task SendRejectionEmailAsync(string toEmail, string applicantName, string departmentName, string reason);
    }
}