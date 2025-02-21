using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailServiceCustom
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlFilePath);
        Task SendPasswordResetToken(string email, string callbackUrl);
        Task SendConfirmationEmail(string email, string confirmationToken);
    }
}
