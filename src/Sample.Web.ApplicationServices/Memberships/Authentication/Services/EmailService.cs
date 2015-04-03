using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Sample.Web.ApplicationServices.Configuration;
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Services
{
    public class EmailService : IIdentityMessageService
    {

        public EmailService()
        {
            if (!IsEnabled)
                return;

            SenderEmail = ""; //WebConfigExtensions.GetFromAppSettings("EmailService-SenderEmail");
            SenderName = ""; //WebConfigExtensions.GetFromAppSettings("EmailService-SenderName");
            Subject = ""; //WebConfigExtensions.GetFromAppSettings("EmailService-Subject");
        }

        private bool IsEnabled
        {
            get { return EmailServiceConfigurationSection.IsEnabled; }

        }

        private string SenderEmail { get; set; }
        private string SenderName { get; set; }
        private string Subject { get; set; }


        public Task SendAsync(IdentityMessage message)
        {
            return !IsEnabled ? Task.FromResult(0) : SendMail(message);
        }

        /// <summary>
        /// 
        /// </summary>
        private Task SendMail(IdentityMessage message)
        {
            var mailMessage = CreateMessage(message);
            var smtpClient = CreateSmtpClient();

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                var s = e.Message;
                //Debug.WriteLine(e);
            }

            return Task.FromResult(0);
        }

        private MailMessage CreateMessage(IdentityMessage message)
        {
            var text = HttpUtility.HtmlEncode(message.Body);

            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(message.Destination));
            mailMessage.Subject = Subject;
            mailMessage.From = new MailAddress(SenderEmail, SenderName);
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

            return mailMessage;
        }

        private SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Port = EmailServiceConfigurationSection.SmtpPort,
                Host = EmailServiceConfigurationSection.SmtpHost,
                Credentials = CreateNetworkCredential()
            };
            return smtpClient;
        }

        private static NetworkCredential CreateNetworkCredential()
        {
            return new NetworkCredential(EmailServiceConfigurationSection.NetworkUser,
                                         EmailServiceConfigurationSection.NetworkPassword);
        }
    }
}
