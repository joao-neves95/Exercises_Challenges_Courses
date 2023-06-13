using eShop.Ordering.Application.Contracts.Infrastructure;
using eShop.Ordering.Application.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace eShop.Ordering.Infrastructure.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailConfig _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailConfig> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = (emailSettings ?? throw new ArgumentNullException(nameof(emailSettings))).Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var sendGridMessage = MailHelper.CreateSingleEmail(
                new EmailAddress(_emailSettings.FromAddress, _emailSettings.FromName),
                new EmailAddress(email.To),
                email.Subject, email.Body, email.Body);

            _logger.LogInformation("Sending email.");

            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("Email sent.");
                return true;
            }

            _logger.LogInformation("Email failed.");
            return false;
        }
    }
}
