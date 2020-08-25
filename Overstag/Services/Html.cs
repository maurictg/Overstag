using Overstag.Models.Database.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Services
{
    public static class Html
    {
        public static string YesNo(bool yes)
            => $"<b class=\"{((yes) ? "green-text" : "red-text")}\">{((yes) ? "Ja" : "Nee")}</b>";

        public static string ManWoman(bool man)
            => $"<b class=\"{((man) ? "blue-text" : "red-text")}\">{((man) ? "Man" : "Vrouw")}</b>";

        public static string MV(bool man)
           => ManWoman(man).Replace("Man","M").Replace("Vrouw","V");

        public static string Euro(int cents, bool withSpace = false) 
            => $"&euro;{(withSpace? "&nbsp;":"")}{Math.Round((double)cents / 100, 2):F}";

        public static string PayStatus(PaymentStatus status)
        {
            string title = "Onbekend";
            string color = "grey";

            switch (status)
            {
                /*case Mollie.Api.Models.Payment.PaymentStatus.Open:
                    title = "Open";
                    color = "blue";
                    break;
                case Mollie.Api.Models.Payment.PaymentStatus.Canceled:
                    title = "Geannuleerd";
                    color = "orange";
                    break;
                case Mollie.Api.Models.Payment.PaymentStatus.Pending:
                    title = "In afwachting";
                    color = "teal";
                    break;
                case Mollie.Api.Models.Payment.PaymentStatus.Authorized:
                    title = "Geauthoriseerd";
                    color = "blue";
                    break;
                case Mollie.Api.Models.Payment.PaymentStatus.Expired:
                    title = "Verlopen";
                    color = "orange";
                    break;
                case Mollie.Api.Models.Payment.PaymentStatus.Failed:
                    title = "Mislukt";
                    color = "red";
                    break;
                case Mollie.Api.Models.Payment.PaymentStatus.Paid:
                    title = "Betaald";
                    color = "green";
                    break;*/
                default:
                    break;
            }   

            return $"<b class=\"{color}-text\">{title}</b>";
        }
    }
}
