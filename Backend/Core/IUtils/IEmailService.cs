using System.Net.Mail;

namespace Core.IUtils
{
    public interface IEmailService
    {
        interface IEmailMessageBuilder
        {
            IEmailMessageBuilder AddMessage(string msg, bool withBreak = true, bool isBold = false);
            IEmailMessageBuilder BreakLine();
            string Build();
        }
        public void SendEmail(string subject, string message, List<string> receviers, List<Attachment>? attachments = null);
        IEmailMessageBuilder GetMessageBuilder();
    }
}
