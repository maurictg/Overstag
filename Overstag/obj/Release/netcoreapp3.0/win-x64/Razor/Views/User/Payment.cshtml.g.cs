#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7422d356a782a1ca6a10635bc825250b996448d4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Payment), @"mvc.1.0.view", @"/Views/User/Payment.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7422d356a782a1ca6a10635bc825250b996448d4", @"/Views/User/Payment.cshtml")]
    public class Views_User_Payment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.NoDB.UnpayedEvents>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
  
    Layout = "~/Views/_UserLayout.cshtml";
    ViewBag.Title = "Betalingen";
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");
    var currentuser = new Overstag.Models.OverstagContext().Accounts.Include(f => f.Family).First(g => g.Token == Context.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div>\r\n");
#nullable restore
#line 12 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
         if (currentuser.Family != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h4 class=\"red-text center-align\" style=\"font-weight: bold;\">De facturering wordt beheert door uw ouder.</h4>\r\n");
#nullable restore
#line 15 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h3>Niet-gefactureerde activiteiten</h3>\r\n            <div id=\"unpayedevents\">\r\n");
#nullable restore
#line 20 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                 if (Model.UnfacturedEvents.Count() > 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <table class=""responsive-table"">
                        <thead>
                            <tr>
                                <th>Wat</th>
                                <th>Wanneer</th>
                                <th>Omschrijving</th>
                                <th>Aantal drankjes</th>
                                <th>Aantal vriend(inn)en</th>
                                <th>Kosten</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 34 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                              double topay = 0; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                             foreach (var e in Model.UnfacturedEvents)
                            {
                                var p = Model.Subscriptions.First(f => f.EventID == e.Id);
                                double c = (double)e.Cost;
                                topay += c * (p.FriendCount + 1);
                                topay += p.ConsumptionTax;

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td>");
#nullable restore
#line 42 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                               Write(e.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 43 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                               Write(e.When.ToString("D", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral(" om ");
#nullable restore
#line 43 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                                 Write(e.When.ToString("t", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 44 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                               Write(e.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 45 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                 if (p.ConsumptionCount > 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td>");
#nullable restore
#line 47 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                   Write(p.ConsumptionCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (<b>+&euro;");
#nullable restore
#line 47 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                                  Write(Math.Round(Convert.ToDouble(p.ConsumptionTax) / 100,2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>)</td>\r\n");
#nullable restore
#line 48 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td>-</td>\r\n");
#nullable restore
#line 52 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                 if (p.FriendCount > 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td>");
#nullable restore
#line 55 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                   Write(p.FriendCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (<b>+&euro;");
#nullable restore
#line 55 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                             Write(Math.Round(c * (p.FriendCount) / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>)</td>\r\n");
#nullable restore
#line 56 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td>-</td>\r\n");
#nullable restore
#line 60 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <td>&euro;");
#nullable restore
#line 61 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                     Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                            </tr>\r\n");
#nullable restore
#line 63 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr class=\"grey lighten-4\">\r\n                                <td colspan=\"6\" class=\"center\">\r\n                                    <b>Totaal te verrekenen: &euro;");
#nullable restore
#line 66 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                              Write(Math.Round(Convert.ToDecimal(topay) / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n");
#nullable restore
#line 71 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Geen ongefactureerde activiteiten!</h5>\r\n");
#nullable restore
#line 75 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <h3>Facturen</h3>\r\n            <div id=\"factures\">\r\n");
#nullable restore
#line 79 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                 if (Model.UnfacturedEvents.Count() > 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h6>&nbsp;Factuur genereren van ongefactureerde avonden</h6>\r\n                    <a href=\"#ginvoice\" class=\"waves-effect waves-light btn modal-trigger\">Factuur genereren</a>\r\n                    <br /><br />\r\n");
#nullable restore
#line 84 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 85 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                 if (Model.Invoices.Count() > 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <ul class=\"collapsible\">\r\n");
#nullable restore
#line 88 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                         foreach (var item in Model.Invoices)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <li>
                                <div class=""collapsible-header"">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td><i class=""material-icons"">calendar_today</i>");
#nullable restore
#line 95 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                                                           Write(item.Timestamp.ToString("d", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                <td>Totaal: &euro;");
#nullable restore
#line 96 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                             Write(Math.Round((double)item.Amount / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                <td>Betaald: ");
#nullable restore
#line 97 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                        Write(Html.Raw((item.Payed) ? "<b class='green-text'>Ja</b>" : "<b class='red-text'>Nee</b>"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class=""collapsible-body"">
");
#nullable restore
#line 103 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                     if (!item.Payed)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <a class=\"waves-effect waves-light btn blue\"");
            BeginWriteAttribute("onclick", " onclick=\"", 5328, "\"", 5389, 3);
            WriteAttributeValue("", 5338, "Payment.getPay(\'", 5338, 16, true);
#nullable restore
#line 105 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
WriteAttributeValue("", 5354, Uri.EscapeDataString(item.PayID), 5354, 33, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5387, "\')", 5387, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Betalen</a>\r\n                                        <br />\r\n");
#nullable restore
#line 107 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <a class=\"waves-effect waves-light btn green\" target=\"_blank\"");
            BeginWriteAttribute("href", " href=\"", 5673, "\"", 5725, 2);
            WriteAttributeValue("", 5680, "/Pay/Direct/", 5680, 12, true);
#nullable restore
#line 110 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
WriteAttributeValue("", 5692, Uri.EscapeDataString(item.PayID), 5692, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Factuur openen</a>\r\n");
#nullable restore
#line 111 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    <table class=""responsive-table"">
                                        <thead>
                                            <tr>
                                                <th>Wat</th>
                                                <th>Wanneer</th>
                                                <th>Omschrijving</th>
                                                <th>Kosten</th>
                                            </tr>
                                        </thead>
                                        <tbody>
");
#nullable restore
#line 123 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                             foreach (var e in item.Events)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <tr>\r\n                                                    <td>");
#nullable restore
#line 126 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                   Write(e.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                    <td>");
#nullable restore
#line 127 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                   Write(e.When.ToString("D", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral(" om ");
#nullable restore
#line 127 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                                                     Write(e.When.ToString("t", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                    <td>");
#nullable restore
#line 128 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                   Write(e.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                    <td>&euro;");
#nullable restore
#line 129 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                         Write(Math.Round((double)e.Cost / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                </tr>\r\n");
#nullable restore
#line 131 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            <tr>\r\n                                                <td colspan=\"4\" class=\"center-align white-text grey darken-1\">\r\n                                                    Totaal: &euro;");
#nullable restore
#line 135 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                                             Write(Math.Round((double)item.Amount / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                                    ");
#nullable restore
#line 136 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                                               Write(Html.Raw((item.Additions > 0) ? $"<br />Let op: Dit is <b>inclusief</b> de drankjes. ({item.Additions})" : ""));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </li>
");
#nullable restore
#line 143 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </ul>\r\n");
#nullable restore
#line 145 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5>Geen facturen gevonden</h5><br />\r\n");
#nullable restore
#line 149 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n");
#nullable restore
#line 151 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Payment.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>

<!-- Modals -->
<div id=""pay"" class=""modal"">
    <div class=""modal-content"">
        <h4>Betalen</h4>
        <h6>Je kunt er voor kiezen om zelf te betalen of om deze betaallink naar iemand anders te sturen</h6>
        <input id=""payid"" type=""text"" placeholder=""payid"" data-pid="""" />
        <a class=""btn btn-small"" onclick=""Payment.gotoPay()"">Nu zelf betalen</a>
        <a class=""btn btn-small"" onclick=""Payment.copyPay()"">Link kopiëren</a>
    </div>
    <div class=""modal-footer"">
        <a href=""#!"" class=""modal-close waves-effect waves-green btn-flat"">Sluiten</a>
    </div>
</div>

<div id=""ginvoice"" class=""modal"">
    <div class=""modal-content"">
        <h4>Factuur maken</h4>
        <p>
            Wanneer je een factuur maakt, worden alle tot nu toe niet betaalde of gefactureerde avonden
            (die staan hier boven) gefactureerd. Vervolgens kun je er zelf voor kiezen of je de factuur zelf, nu
            of later, of door een ander laat betalen door middel van ee");
            WriteLiteral(@"n betaallink.
        </p>
    </div>
    <div class=""modal-footer"">
        <a href=""#!"" class=""waves-effect waves-light btn modal-close orange"">Laat maar</a>
        <a href=""#!"" onclick=""Payment.generateInvoice()"" class=""waves-effect waves-light btn modal-close green"">Oké!</a>
    </div>
</div>

<!--Script -->
<script>
    var Payment = {
        init: function () {
            $('#_apay, #_mapay').addClass('active');
        },
        generateInvoice: function () {
            $.getJSON('/User/GenerateInvoice', function (data) {
                if (data.status == 'success') {
                    M.toast({ html: 'Factuur gemaakt', classes: 'green' })
                    window.location.reload();
                } else {
                    M.toast({ html: data.error, classes: 'red' })
                }
            });
        },
        getPay: function (payid) {
            $('#payid').data('pid', payid);
            $('#payid').val(window.location.origin+'/Pay/Direct/' + payid");
            WriteLiteral(@");
            M.Modal.getInstance($('#pay')).open();
        },
        gotoPay: function () {
            window.location.href = '/Pay/Direct/' + $('#payid').data('pid');
        },
        copyPay: function () {
            $('#payid').select();
            document.execCommand(""copy"");
            M.toast({ html: 'Link gekopieërd!', classes: 'blue' });
        }
    }

    Payment.init();

    $(document).ready(function () {
        $('.collapsible').collapsible();
        $('.modal').modal();
    });
</script>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.NoDB.UnpayedEvents> Html { get; private set; }
    }
}
#pragma warning restore 1591
