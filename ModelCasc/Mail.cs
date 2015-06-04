using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ModelCasc
{
    public class Mail
    {
        public static void SendMail(string subject, string body, string mailTo, string mailFrom)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential =
                new NetworkCredential("gcruz@casc.com.mx", "Gbyhnuj7/");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(mailFrom + "@casc.com.mx");

            smtpClient.Host = "mail.casc.com.mx";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = subject;
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(mailTo);
            try
            {
                smtpClient.Send(message);
            }
            catch 
            {
                //Error, could not send the message
                throw;
            }
        }
    }
}
