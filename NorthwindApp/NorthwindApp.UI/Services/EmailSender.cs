using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NorthwindApp.UI.Services
{
    public class EmailSender : IEmailSender
    {
        private const string FromEmail = "auto-message@northwind.com";
        private const string FromName = "Northwind";

        private readonly AuthMessageSenderOptions _senderOptions;

        public EmailSender(IOptions<AuthMessageSenderOptions> senderOptions)
        {
            _senderOptions = senderOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var client = new SendGridClient(_senderOptions.SendGridKey);
            var message = new SendGridMessage
            {
                From = new EmailAddress(FromEmail, FromName),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = content
            };
            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);

            await client.SendEmailAsync(message);
        }
    }
}
