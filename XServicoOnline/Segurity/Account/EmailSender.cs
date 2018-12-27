using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace XServicoOnline.Segurity.Account
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; }
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        //public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        //{
        //    Options = optionsAccessor.Value;
        //}
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email,subject, message);
        }
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            return Task.Run(() => null);
        }
        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Sistema Serviço online")
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //
                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
