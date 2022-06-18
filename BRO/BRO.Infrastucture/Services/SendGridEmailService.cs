using BRO.Domain.IServices;
using BRO.Infrastructure.Services.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BRO.Infrastructure.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _apiKey;
        public SendGridEmailService(IOptions<EmailSettings> options)
        {
            _apiKey = options.Value.SendGridKey;

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage,string plainTextMessage)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("BROShops@outlook.com", "BRO Shops");
            var to = new EmailAddress(email, "End User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject,plainTextMessage,htmlMessage);  
            var result= await client.SendEmailAsync(msg);
            if(!result.IsSuccessStatusCode)
                throw new SmtpException("Email was not sent, something went wrong");
        }
    }
}
