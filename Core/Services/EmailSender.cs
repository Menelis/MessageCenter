using Core.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task SendEmail(Message message)
        {
            
            await SendAsync(CreateMessage(message));
        }
        private MimeMessage CreateMessage(Message message)
        {
            BodyBuilder bodyBuilder = new()
            {
                TextBody = message.EmailFormat == TextFormat.Text ? message.Content : string.Empty,
                HtmlBody = message.EmailFormat == TextFormat.Html ? message.Content : string.Empty
            };

            MimeMessage emailMessage = new()
            {
                Subject = message.Subject,
                Body = bodyBuilder.ToMessageBody()
            };
            emailMessage.From.Add(new MailboxAddress(_emailSettings.From));
            emailMessage.To.AddRange(message.To);
            return emailMessage;
        }
        
        private async Task SendAsync(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port);
                if (client.AuthenticationMechanisms.Count > 0)
                    await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);

                await client.SendAsync(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
