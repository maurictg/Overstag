using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Overstag.Services
{
    public class Pdf
    {
        public static string GenerateInvoiceHtml(XInvoice i, HttpContext context)
        {
            const string overstagAdres = 
                "Stichting Overstag<br/>" +
                "T.a.v. het secretariaat<br/><br/>" +
                "Kapucijnenweg 28<br/>" +
                "4411 NC RILLAND<br/>";

            var c = new StringBuilder();
            //HTML document
            //Header
            c.Append($"<!DOCTYPE html><html lang=\"nl\"><head><meta charset=\"UTF - 8\"><meta name=\"viewport\" content=\"width = device-width, initial-scale = 1.0\"></head>");
            
            //Body, navigation bar with invoice number
            c.Append("<body>");
            c.Append($"<nav><div class=\"nav-wrapper o-blue\"><a href=\"#\" class=\"brand-logo left\">&nbsp;Overstag Factuur #{i.GetInvoiceNumber()}</a></div></nav>");
            
            //Main container
            c.Append("<div class=\"container\"><div class=\"row\">");

            //Information
            c.Append($"<h4 class=\"o-blue-text\">Informatie</h4>" +
                $"<div class=\"col s6\"><h4 class=\"o-orange-text\">Afzender</h4><h6>{overstagAdres}</h6></div>");
            c.Append($"<div class=\"col s6\"><h4 class=\"o-orange-text\">Gegevens</h4><h6>{i.Fullname}<br/>{i.Address}</h6></div>");

            //The invoice itself
            c.Append("</div><div class=\"row\">");
            c.Append("<h4 class=\"o-blue-text\">Factuur</h4>");
            c.Append($"<h6><b>Datum: </b> {i.Timestamp.ToString("dd-MM-yyyy HH:mm")}<br/>" +
                $"<b>Betreft: </b> Facturering activiteiten <br/><b>Factuurnummer: </b> {i.GetInvoiceNumber()} <br/>" +
                $"<b>Online factuur: </b><a href=\"{i.GetInvoiceURL(context)}\">{i.GetInvoiceURL(context)}</a><br/></h6>");

            //Events (Table)
            c.Append("</div><div class=\"row\"><h4 class=\"o-blue-text\">Activiteiten</h4>");
            c.Append("<table><thead><tr><th>Titel</th><th>Datum</th><th>Kosten activiteit</th><th>Kosten aan drankjes</th><th>Aantal vrienden</th><th>Totaal</th></tr></thead>");
            c.Append("<tbody>");
            foreach(var e in i.Events)
            {
                c.Append($"<tr><td>{e.Key.Title}</td><td>{e.Key.When.ToString("dd-MM-yyyy 'om' HH:mm")}</td><td>{Html.Euro(e.Key.Cost, true)}</td><td>{Html.Euro(e.Value.Item2, true)}</td><td>{e.Value.Item1} (+{Html.Euro(e.Value.Item1 * e.Key.Cost)})</td><td>{Html.Euro(((e.Value.Item1 + 1) * e.Key.Cost) + e.Value.Item2, true)}</td></tr>");
            }
            c.Append($"<td colspan=\"6\" class=\"center-align\"><h6>Totaal: <b>{Html.Euro(i.Amount,true)}</b></h6></td>");
            c.Append("</tbody></table>");

            //End of document
            c.Append("</div></div></body></html>");

            return c.ToString();
        }
    }
}
