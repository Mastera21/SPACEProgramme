using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SPACE_Programme_Task
{
    internal class EmailSender
    {
        public static void SendEmail(string fileDBPath, string fromEmailAddress, string password, string toEmailAddress)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmailAddress);
                mail.To.Add(toEmailAddress);
                mail.Subject = "Test Email";
                mail.Body = "Sending " + fileDBPath + " file";

                Attachment attachment = new Attachment(fileDBPath);
                mail.Attachments.Add(attachment);

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new NetworkCredential(fromEmailAddress, password);

                Console.WriteLine("Sending ...");
                SmtpServer.Send(mail);
            }
            catch (Exception ex) when (fileDBPath == "")
            {
                Console.WriteLine("Error, File database path parameter is empty in EmailSender class", ex.Message);
            }
            catch (Exception ex) when (fromEmailAddress == "")
            {
                Console.WriteLine("Error, From Email address parameter is empty in EmailSender class", ex.Message);
            }
            catch (Exception ex) when (password == "")
            {
                Console.WriteLine("Error, Password parameter is empty in EmailSender class", ex.Message);
            }
            catch (Exception ex) when (toEmailAddress == "")
            {
                Console.WriteLine("Error, To email address parameter is empty in EmailSender class", ex.Message);
            }
        }

    }
}
