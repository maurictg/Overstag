using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Overstag.Models;

namespace Overstag.Services
{
    public class Email
    {
        protected List<string> to;
        protected string title;
        protected string message;
        protected string from;
        protected NetworkCredential credentials;
        public Exception error;

        public Email(string title, string message, string to, string from, string username, string password) : this(title, message, new string[] { to }, from, username,password) { }
        public Email(string title, string message, string[] to, string from, string username, string password)
        {
            this.to = to.ToList();
            this.from = from;
            this.title = title;
            this.message = message;
            this.credentials = new NetworkCredential(username, password);
        }

        public async Task<bool> SendAsync(bool force = false)
        {

#if !DEBUG
            force = true;
#endif
            if (force)
                return await _SendAsync();
            else
                return true;
        }

        private async Task<bool> _SendAsync()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mijnhostingpartner.nl", 25); //587 for gmail?
                client.UseDefaultCredentials = false;
                client.Credentials = this.credentials;

                //Code toegevoegd voor overstag mailsender
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false; //enable for gmail

                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(this.from);

                this.to.ForEach(f => mail.To.Add(f));

                mail.Subject = this.title;
                mail.IsBodyHtml = true;
                mail.Body = this.message;

                await client.SendMailAsync(mail);

                return true;
            }
            catch (Exception e)
            {
                this.error = e;
                return false;
            }
        }
    }

    public class InvoiceEmail : Email
    {
        public InvoiceEmail(Account user, string invoiceToken) : base("Factuur gemaakt", "", "", "noreply@stoverstag.nl", null, null)
        {
            this.credentials = new NetworkCredential(Core.General.Credentials.mailUsername, Core.General.Credentials.mailPass);
            this.to.Clear();
            this.to.Add(user.Email);
            this.message = $"<h1>Er is een factuur gemaakt</h1><h4>Beste {user.Firstname},<br>Er is automatisch een factuur gemaakt van de afgelopen avonden.<br>Deze kun je vinden onder <i>&quot;Betalingen&quot;</i> in je account op de website.<br>Hier is de link naar je factuur:<br><br><a href=\"https://stoverstag.nl/Pay/Invoice/{Uri.EscapeDataString(invoiceToken)}\">https://stoverstag.nl/Pay/Invoice/{Uri.EscapeDataString(invoiceToken)}</a><br></h4>";
        }
    }
}
