using Core.IUtils;
using Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Text;
using static Core.IUtils.IEmailService;

namespace Services.Utils
{
    public class EmailService : IEmailService
    {
        private const string Host = Constants.MailHost;
        private const int Port = Constants.MailPort;
        private const string Password = Constants.EmailPassword;
        private const string From = Constants.EmailAddress;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }


        public class MailMessageBuilder : IEmailMessageBuilder
        {
            public StringBuilder builder;

            public MailMessageBuilder()
            {
                builder = new StringBuilder();
            }

            public IEmailMessageBuilder AddMessage(string msg, bool withBreak = true, bool isBold = false)
            {
                if (isBold)
                    builder.Append("<b>");

                builder.Append(msg);

                if (isBold)
                    builder.Append("</b>");

                return withBreak ? BreakLine() : this;
            }

            public IEmailMessageBuilder BreakLine()
            {
                builder.Append("<br/>");
                return this;
            }

            public string Build() => builder.ToString();
        }



        public void SendEmail(string subject, string message, List<string> receviers, List<Attachment>? attachments = null)
        {
            MailMessage msg = CreateMailMessage(subject, message, receviers, attachments);
            using var client = new SmtpClient(Host, Port)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(From, Password),
            };
            client.Send(msg);
        }



        /// <summary>
        /// Creates a mail message 
        /// </summary>
        /// <param name="subject">
        /// The subject of the Mail
        /// </param>
        /// <param name="message">
        /// The message of the mail
        /// </param>
        /// <param name="attachments">
        /// Files if you want to send with the email
        /// </param>
        /// <param name="receiverLists">
        /// Receivers that will get the mail
        /// </param>
        /// <returns></returns>
        private MailMessage CreateMailMessage(string subject, string message, List<string> receiverLists, List<Attachment>? attachments = null, MailPriority priority = MailPriority.Normal)
        {
            var htmlTemplateSP = new StringBuilder(GetMailTemplate());

            htmlTemplateSP.Replace("{{body}}", message);

            var bodyHtml = htmlTemplateSP.ToString();

            MailMessage mailMessage = new()
            {
                Priority = priority,
                IsBodyHtml = true,
            };

            receiverLists.ForEach(mailMessage.To.Add);
            attachments?.ForEach(mailMessage.Attachments.Add);

            mailMessage.From = new MailAddress(From);
            mailMessage.Body = bodyHtml;
            mailMessage.Subject = subject;
            return mailMessage;
        }




        /// <summary>
        /// Gets mail template from server/Local
        /// </summary>
        /// <returns></returns>

        private string GetMailTemplate()
        {
            string template = string.Empty;
            template = ReadMailTemplateFromLocal();
            return template;
        }



        /// <summary>
        /// Reads html template from the file on the local pc
        /// </summary>
        /// <returns></returns>
        private string ReadMailTemplateFromLocal()
        {
            try
            {
                return File.ReadAllText(Directory.GetCurrentDirectory() + "\\Templates\\EmailTemplates\\DefaultEmailTemplate.html");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Could'nt read mail template");
            }
        }

        public IEmailMessageBuilder GetMessageBuilder() => new MailMessageBuilder();
    }
}
