using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Net.Mail;

namespace Overstag.Core
{
    public class Credentials
    {
        public string mailUsername { get; set; }
        public string mailPass { get; set; }
        public string mySqlConnectionString { get; set; }
        public string msSqlConnectionString { get; set; }
        public string mollieApiToken { get; set; }

        public Credentials Get()
        {
            var o =  JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "credentials.json")));
            return o;
        }
    }


    public static class General
    {
        public static Credentials Credentials = new Credentials().Get();

        public static bool DateIsPassed(DateTime check)
            => check < DateTime.Now;
        public static int getAge(DateTime bd)
         => (new DateTime(DateTime.Now.Year, bd.Month, bd.Day) > DateTime.Now ? (DateTime.Now.Year - bd.Year)-1 : (DateTime.Now.Year - bd.Year));
        public static string SendMail(string title, string body, string to)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mijnhostingpartner.nl", 25); //587
                client.UseDefaultCredentials = false;
                if (Credentials.mailPass != string.Empty)
                {
                    client.Credentials = new NetworkCredential(Credentials.mailUsername, Credentials.mailPass);

                    //Code toegevoegd voor overstag mailsender
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = false;

                    client.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(Credentials.mailUsername);
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
            catch (Exception e) { throw e; /*return "ERR: " + e.ToString();*/ }
        }
    }


}
