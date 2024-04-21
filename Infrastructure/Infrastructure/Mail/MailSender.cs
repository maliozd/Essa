using Application;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Infrastructure.Mail
{
    public class MailSender(IOptions<MailConfiguration> mailConfiguration) : IMailSender
    {
        readonly MailConfiguration _mailConfiguration = mailConfiguration.Value ?? throw new ArgumentNullException(nameof(mailConfiguration));

        public async Task<bool> SendAsync(MailDto mail, CancellationToken cancellationToken)
        {

            try
            {
                MailMessage mailMessage = new();
                mailMessage.From = new MailAddress(_mailConfiguration.Protocols.Smtp.Sender);
                mailMessage.To.AddRange(mail.Recipants);
                mailMessage.Subject = mail.Subject;
                mailMessage.Body = mail.Body;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new()
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(_mailConfiguration.Credentials.Mail, _mailConfiguration.Credentials.Password),
                    EnableSsl = true,
                    Host = _mailConfiguration.Protocols.Smtp.HostName,
                    Port = _mailConfiguration.Protocols.Smtp.Port
                };

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
                return false;
            }
        }


    }
}
