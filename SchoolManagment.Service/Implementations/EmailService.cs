using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SchoolManagment.Data.Helper;
using SchoolManagment.Service.Abstracts;
namespace SchoolManagment.Service.Implementations
{
    public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
    {
        public async Task<string> SendEmail(string email, string Message, string? subject)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.Value.Host, emailSettings.Value.Port, true);
                    client.Authenticate(emailSettings.Value.FromEmail, emailSettings.Value.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(MailboxAddress.Parse(emailSettings.Value.FromEmail));
                    message.To.Add(MailboxAddress.Parse(email));
                    message.Subject = subject == null ? "No subject" : subject;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }

    }
}
