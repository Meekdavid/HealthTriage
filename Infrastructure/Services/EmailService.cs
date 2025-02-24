using Common.ConfigurationSettings;
using Common.Services;
using Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
//using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmailService : IEmailServiceCustom
    {
        private readonly ILogger<EmailService> _logger;
        private readonly string _smtpServer = ConfigSettings.ApplicationSetting.EmailDetails.SMTPServer;
        private readonly int _smtpPort = ConfigSettings.ApplicationSetting.EmailDetails.Port; // Use 465 for SSL, 587 for TLS
        private readonly string _smtpUser = AESHelper.Decrypt(ConfigSettings.ApplicationSetting.EmailDetails.UserName) ?? /*"mbokodavid@gmail.com"*/"MS_WtsoKH@trial-3vz9dlen3r1lkj50.mlsender.net";
        private readonly string _smtpPass = AESHelper.Decrypt(ConfigSettings.ApplicationSetting.EmailDetails.Password) ?? "mssp.Skh7zVF.ynrw7gyqd2n42k8e.djbqXKt" /*"ednnppsbnlhjykav"*/;
        private readonly SmtpClient _smtpClient;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
            _smtpClient = new SmtpClient();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("HealthTriage", _smtpUser));
                email.To.Add(new MailboxAddress(toEmail, toEmail));
                email.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };
                email.Body = bodyBuilder.ToMessageBody();

                if (!_smtpClient.IsConnected)
                {
                    await _smtpClient.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                    await _smtpClient.AuthenticateAsync(_smtpUser, _smtpPass);
                }
                await _smtpClient.SendAsync(email);
            }
            catch (Exception ex)
            {
            }

        }

        public async Task SendPasswordResetToken(string email, string callbackUrl)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("HealthTriage", _smtpUser));
            mailMessage.To.Add(new MailboxAddress(email, email));
            mailMessage.Subject = "Password Reset";
            mailMessage.Body = new TextPart("html")
            {
                Text = $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>."
            };

            if (!_smtpClient.IsConnected)
            {
                await _smtpClient.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await _smtpClient.AuthenticateAsync(_smtpUser, _smtpPass);
            }

            await _smtpClient.SendAsync(mailMessage);
        }

        public void Dispose()
        {
            if (_smtpClient.IsConnected)
            {
                _smtpClient.Disconnect(true);
            }
            _smtpClient.Dispose();
        }

        public async Task SendConfirmationEmail(string email, string confirmationToken)
        {
            // Encode the token and email for use in the URL
            string encodedToken = Uri.EscapeDataString(confirmationToken);
            string encodedEmail = Uri.EscapeDataString(email);

            // Construct the confirmation link
            string confirmationLink = $"{ConfigSettings.ApplicationSetting.BaseLocalStorageDomain}api/auth/confirm-email?token={encodedToken}&email={encodedEmail}";

            // Create the MimeMessage
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("HealthTriage", _smtpUser));
            mailMessage.To.Add(new MailboxAddress(email, email));
            mailMessage.Subject = "Confirm your email";

            // Create the HTML body
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
        <p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'> 
            Hello and welcome to <strong>HealthTriage</strong>! 🎉  
        </p> 
        <p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>  
            You're just one step away from unlocking a world of expert healthcare at your fingertips.  
            To activate your account, simply click the button below:  
        </p>  
        <p style='text-align: center;'>  
            <a href='{confirmationLink}' 
               style='background-color: #4CAF50; color: white; padding: 12px 20px; text-decoration: none; font-size: 16px; border-radius: 5px; display: inline-block;'> 
                Confirm My Account 
            </a>  
        </p>  
        <p style='font-family: Arial, sans-serif; font-size: 14px; color: #555;'>  
            This link will expire in <strong>24 hours</strong>. If you didn’t sign up for HealthTriage, you can safely ignore this email.  
        </p>  
        <p style='font-family: Arial, sans-serif; font-size: 14px; color: #555;'>  
            Stay healthy, stay informed! 💙  
        </p>  
        <p style='font-family: Arial, sans-serif; font-size: 14px; color: #555;'>  
            — The HealthTriage Team  
        </p>"
            };

            mailMessage.Body = bodyBuilder.ToMessageBody();

            // Ensure the SMTP client is connected
            if (!_smtpClient.IsConnected)
            {
                await _smtpClient.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await _smtpClient.AuthenticateAsync(_smtpUser, _smtpPass);
            }

            // Send the email
            await _smtpClient.SendAsync(mailMessage);
        }
    }
}