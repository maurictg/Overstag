using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
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
        public string msSqlDebugCString { get; set; }

        /// <summary>
        /// Get credentials from file on server
        /// </summary>
        /// <returns>Credentials object</returns>
        public Credentials Get()
            => JsonSerializer.Deserialize<Credentials>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "credentials.json")));
    }

    public static class Auth
    {
        private static List<Models.Auth> Auths = new List<Models.Auth>();

        public static bool IsAuthenticated(string token)
        {
            if (!Auths.Any(f => f.Token == token))
                return false;

            var auth = Auths.First(f => f.Token == token);
            int index = Auths.IndexOf(auth);
            if (DateTime.Now.AddMonths(-1) > auth.LastSeen)
            {
                Auths.Remove(auth);
                return false;
            }
            else
            {
                auth.LastSeen = DateTime.Now;
                Auths[index] = auth;
                return true;
            }
        }

        public static string Register(string ip, int id)
        {
            if (Auths.Count() > 50)
                Clear();

            var auth = new Models.Auth { IP = ip, UserID = id, FirstLogin = DateTime.Now, LastSeen = DateTime.Now, Token = Overstag.Encryption.Random.rString(Encryption.Random.rInt(15, 30)) };
            Auths.Add(auth);
            return auth.Token;
        }

        public static int getUserID(string authtoken)
            => Auths.First(f => f.Token == authtoken).UserID;

        public static bool UnRegister(string token)
           => Auths.Remove(Auths.First(f => f.Token == token));

        public static List<Models.Auth> getAuth(int userid = -1)
            => (userid == -1) ? Auths : Auths.Where(f => f.UserID == userid).ToList();

        public static int ClearUser(int userid)
            => Auths.RemoveAll(f => f.UserID == userid);

        public static int Clear()
        {
            var outdated = Auths.Where(f => DateTime.Now.AddMonths(-1) > f.LastSeen);
            foreach (var o in outdated)
                Auths.Remove(o);

            return outdated.Count();
        }
    }

    public static class General
    {
        public static Credentials Credentials = new Credentials().Get();

        /// <summary>
        /// Create a backup in JSON
        /// </summary>
        public static void CreateBackup()
        {
            int errorcount = 0;
            List<Exception> exceptions = new List<Exception>();

            Overstag.Security.Backup b = new Security.Backup("");
            b.Create(ref errorcount, ref exceptions);
        }

        public static bool DateIsPassed(DateTime check)
            => check < DateTime.Now;

        public static int getAge(DateTime bd)
         => (new DateTime(DateTime.Now.Year, bd.Month, bd.Day) > DateTime.Now ? (DateTime.Now.Year - bd.Year)-1 : (DateTime.Now.Year - bd.Year));

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="title">The subject</param>
        /// <param name="body">The message (in HTML)</param>
        /// <param name="to">The to email adress</param>
        /// <returns>string OK or string ERR</returns>
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
