using System;
using System.Threading.Tasks;
using Fixit.Application.Common.Services;
using Fixit.Application.Common.Services.Mail;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Fixit.Infrastructure.Services.Mail
{
    public class SendgridMailService : IMailService
    {
        private readonly SendGridClient _client;
        private readonly string _defaultSenderEmail;

        public SendgridMailService(IOptions<SendgridMailServiceOptions> optionsAccessor)
        {
            var options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
            _client = new SendGridClient(options.ApiKey);
            _defaultSenderEmail = options.DefaultSenderEmail;
        }

        public Task SendEmailAsync(string to, string subject, string content)
        {
            return SendEmailAsync(_defaultSenderEmail, to, subject, content);
        }

        public async Task SendEmailAsync(string from, string to, string subject, string content)
        {
            var toEmail = new EmailAddress(to);
            var fromEmail = new EmailAddress(from);
            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, content, null);
            try
            {
                await _client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                // log
                Console.WriteLine(e);
            }
        }
    }
}