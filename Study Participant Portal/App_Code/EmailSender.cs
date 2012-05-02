using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;

/// <summary>
/// Summary description for EmailSender
/// </summary>
public class EmailSender
{
	public void sendEmail(string sender, List<string> recipients, string subject, StringBuilder body)
	{
        string username = "studyparticipantportal@gmail.com";
        string password = "2012SPP_user";
        // Set the email parameters
        MailMessage mail = new MailMessage();
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;

        System.Net.NetworkCredential credential = new System.Net.NetworkCredential(username, password);
        client.Credentials = credential;
        mail.From = new MailAddress(sender);
        foreach (string recipient in recipients) {
            mail.To.Add(recipient);
        }
        mail.Subject = subject;
        mail.Body = body.ToString();

        // Send the email
        client.Send(mail);

        // Clean up
        client.Dispose();
        mail.Dispose();
	}
}