using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;

/// <summary>
/// A simple class to send an email.
/// </summary>
public class EmailSender {

    /// <summary>
    /// A method to send and email. It accepts the sender of the email, a list 
    /// of recipients, the subject of the email, and the body of the email.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="recipients"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
	public void sendEmail(string sender, List<string> recipients, string subject, StringBuilder body) {
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