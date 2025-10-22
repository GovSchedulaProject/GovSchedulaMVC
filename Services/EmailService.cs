using SendGrid;
using SendGrid.Helpers.Mail;

namespace GovSchedulaWeb.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        // This 'constructor' gets the API key from your secrets.json
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            // Get the API key from secrets.json
            var apiKey = _configuration["SendGridKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGridKey is not configured.");
            }

            var client = new SendGridClient(apiKey);

            // Set your "From" email. 
            // NOTE: SendGrid requires you to verify this email address in their dashboard.
            var from = new EmailAddress("philip@traveltide.uk", "GovSchedula Booking Appointment");

            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

            var response = await client.SendEmailAsync(msg);

            // You can add logging here to check if response.IsSuccessStatusCode is true or false
        }
    }
}