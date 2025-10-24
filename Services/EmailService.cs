using SendGrid;
using SendGrid.Helpers.Mail;

namespace GovSchedulaWeb.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var apiKey = _configuration["SendGridKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGridKey is not configured.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("philip@traveltide.uk", "GovSchedula Booking Appointment");
            var to = new EmailAddress(toEmail);
            var plainTextContent = "Please view this email in HTML format.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            // Optional logging:
            // Console.WriteLine($"Email sent to {toEmail}, status: {response.StatusCode}");
        }

        // ✉️ Send Approval Email
        public async Task SendApprovalEmailAsync(string toEmail, string applicantName, string departmentName, DateTime? appointmentDate, string? appointmentTime)
        {
            var subject = $"Your {departmentName} Application Has Been Approved";
            string htmlContent = $@"
                <h2>Dear {applicantName},</h2>
                <p>We are pleased to inform you that your <strong>{departmentName}</strong> application has been approved.</p>
                <p><strong>Appointment Details:</strong></p>
                <ul>
                    <li>Date: {appointmentDate?.ToString("dddd, MMMM dd yyyy")}</li>
                    <li>Time: {appointmentTime}</li>
                </ul>
                <p>Please remember to bring all required documents to your appointment.</p>
                <p>Thank you for using GovSchedula.</p>
                <br/>
                <p>Best regards,<br/>GovSchedula Team</p>
            ";

            await SendEmailAsync(toEmail, subject, htmlContent);
        }

        // ❌ Send Rejection Email
        public async Task SendRejectionEmailAsync(string toEmail, string applicantName, string departmentName, string reason)
        {
            var subject = $"Your {departmentName} Application Has Been Rejected";
            string htmlContent = $@"
                <h2>Dear {applicantName},</h2>
                <p>We regret to inform you that your <strong>{departmentName}</strong> application has been rejected.</p>
                <p><strong>Reason:</strong> {reason}</p>
                <p>You may contact the {departmentName} office for more information or reapply if applicable.</p>
                <br/>
                <p>Best regards,<br/>GovSchedula Team</p>
            ";

            await SendEmailAsync(toEmail, subject, htmlContent);
        }
        public async Task SendBookingConfirmationEmailAsync(string toEmail, string applicantName, string departmentName, DateTime? bookingDate)
        {
            var subject = $"Your {departmentName} Booking Has Been Received";

            string htmlContent = $@"
                <h2>Dear {applicantName},</h2>
                <p>Thank you for booking your <strong>{departmentName}</strong> appointment through <strong>GovSchedula</strong>.</p>
                <p>We have received your booking on <strong>{bookingDate?.ToString("dddd, MMMM dd yyyy")}</strong>.</p>
                <p>Our team will review your application shortly. You will receive another email once your booking is 
                <strong>approved</strong> or <strong>rejected</strong>.</p>
                <br/>
                <p>In the meantime, please make sure all required documents are ready for verification.</p>
                <br/>
                <p>Best regards,<br/>GovSchedula Team</p>
             ";

            await SendEmailAsync(toEmail, subject, htmlContent);
        }
    }
}
