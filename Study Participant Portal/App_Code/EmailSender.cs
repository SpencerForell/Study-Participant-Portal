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
    private List<string> addressTo;
    private string subject;
    private StringBuilder body;

    public List<string> AddressTo {
        set {
            if (addressTo == null) {
                addressTo = new List<string>();
            }
            else {
                addressTo = value;
            }
        }
    }

    public string Subject {
        set { subject = value; }
    }

    public StringBuilder Body {
        set { body = value; }
    }

	public EmailSender(List<string> to, string subject, StringBuilder body)
	{
        body = new StringBuilder();
        this.addressTo = to;
        this.subject = subject;
        this.body = body;
	}

    private void sendEmail() {
        MailMessage mail = new MailMessage();
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

        mail.From = new MailAddress("muellmax36@gmail.com");
        foreach (string address in this.addressTo) {
            mail.To.Add(address);
        }

        mail.Subject = this.subject;
        mail.Body = this.body.ToString();
        smtpServer.Port = 587;

        
    }
}