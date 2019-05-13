using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Overstag.Core
{
    public static class General
    {
        public static bool DateIsPassed(string check){return DateTime.Now.CompareTo(DateTime.Parse(check).Add(new TimeSpan(1,0, 0, 0))) > 0;}
    }

    public static class Mail
    {
        public static string SendMail(string title, string body, string to)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                string pass = System.IO.File.ReadAllText(@"C:\Overstag\mailcredential.txt");
                if(pass != string.Empty)
                {
                    client.Credentials = new NetworkCredential("overstagrilland@gmail.com", pass);
                    client.EnableSsl = true;
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("overstagrilland@gmail.com");
                    mail.To.Add(to);
                    mail.Subject = title;
                    mail.IsBodyHtml = true;
                    mail.Body = body;
                    client.Send(mail);
                    return "OK";
                }
                else
                {
                    return "ERR: pass is empty";
                }
                
            }
            catch(Exception e) { return "ERR: "+e.ToString(); }
        }
    }


}
