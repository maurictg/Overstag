#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ca3240784d10e281a71922711ef1bf0916cff16"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pay_Index), @"mvc.1.0.view", @"/Views/Pay/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ca3240784d10e281a71922711ef1bf0916cff16", @"/Views/Pay/Index.cshtml")]
    public class Views_Pay_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.NoDB.OPayInfo>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/logo_2.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("responsive-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
  
    Layout = "_PayLayout";
    ViewBag.Title = "Welkom";
    ViewBag.InvoiceURL = Model.Invoice.PayID;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div id=\"content\">\r\n    <div class=\"row\">\r\n        <div class=\"col s12 m2\">\r\n            <br />\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "0ca3240784d10e281a71922711ef1bf0916cff163665", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col s12 m10 center-align\">\r\n            <h3>Factuur van ");
#nullable restore
#line 15 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                       Write(Model.User.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 15 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                             Write(Model.User.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
        </div>
    </div>
    <div class=""row"">
        <br />
        <ul class=""collapsible expandable"" id=""info"">
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">account_circle</i><b>Gebruikersinformatie</b></div>
                <div class=""collapsible-body"">
                    <table class=""responsive-table"">
                        <thead>
                            <tr>
                                <th>Naam</th>
                                <th>Achternaam</th>
                                <th>Email</th>
                                <th>Adres</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>");
#nullable restore
#line 35 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                               Write(Model.User.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 36 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                               Write(Model.User.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td><i>");
#nullable restore
#line 37 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                  Write(Model.User.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</i></td>\r\n                                <td>");
#nullable restore
#line 38 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                               Write(Model.User.Adress);

#line default
#line hidden
#nullable disable
            WriteLiteral("&nbsp;");
#nullable restore
#line 38 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                       Write(Model.User.Postalcode);

#line default
#line hidden
#nullable disable
            WriteLiteral("&nbsp;");
#nullable restore
#line 38 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                   Write(Model.User.Residence);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">calendar_today</i><b>Evenementen</b></div>
                <div class=""collapsible-body"">
                    <table class=""responsive-table"">
                        <thead>
                            <tr>
                                <th>Titel</th>
                                <th>Omschrijving</th>
                                <th>Datum en tijd</th>
                                <th>Kosten</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 57 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                               int total = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                             foreach (var e in Model.Invoice.Events)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>");
#nullable restore
#line 61 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                   Write(e.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 62 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                   Write(string.Concat(e.Description.Take(30)));

#line default
#line hidden
#nullable disable
            WriteLiteral("...</td>\r\n                                    <td>");
#nullable restore
#line 63 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                   Write(e.When.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" om ");
#nullable restore
#line 63 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                     Write(e.When.ToString("HH:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>&euro;");
#nullable restore
#line 64 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                         Write(Math.Round((double)e.Cost / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#nullable restore
#line 66 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                total += e.Cost;
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td colspan=\"4\" class=\"grey darken-1 white-text\">\r\n                                    Totaal: <b>&euro;");
#nullable restore
#line 70 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                Write(Math.Round((double)total / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> (exclusief drankjes)<br />\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                    <h6><b>");
#nullable restore
#line 75 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                      Write(Model.Invoice.Additions);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> drankje(s) gekocht met een totale waarde van <b>&euro;");
#nullable restore
#line 75 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                                         Write(Model.Invoice.Additions);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",00</b></h6>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">description</i><b>Factuurdetails</b></div>
                <div class=""collapsible-body"">
                    <table class=""responsive-table"">
                        <thead>
                            <tr>
                                <th>Datum</th>
                                <th>Tijd</th>
                                <th>Factuurkenmerk</th>
                                <th>Totaalbedrag</th>
                                <th>Betaald</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>");
#nullable restore
#line 93 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                               Write(Model.Invoice.Timestamp.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 94 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                               Write(Model.Invoice.Timestamp.ToString("HH:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td><i>");
#nullable restore
#line 95 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                  Write(Model.Invoice.PayID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</i></td>\r\n                                <td><b>&euro;");
#nullable restore
#line 96 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                        Write(Math.Round((double)Model.Invoice.Amount / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                                <td><b");
            BeginWriteAttribute("class", " class=\"", 4640, "\"", 4708, 1);
#nullable restore
#line 97 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
WriteAttributeValue("", 4648, Html.Raw((Model.Invoice.Payed) ? "green-text" : "red-text"), 4648, 60, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 97 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                                       Write(Html.Raw((Model.Invoice.Payed) ? "Ja" : "Nee"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n\r\n");
#nullable restore
#line 102 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                     if (ViewBag.Payment != null)
                    {
                        Overstag.Models.Payment p = ViewBag.Payment;
                        DateTime dt = p.PayedAt ?? new DateTime(2000, 1, 1);
                        bool validated = (Model.Invoice.Payed == (p.PayedAt != null && !string.IsNullOrEmpty(p.PaymentID) && p.InvoiceID == Model.Invoice.PayID && Convert.ToInt32(p.Status) == 6));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <br />
                        <table class=""responsive-table"">
                            <thead>
                                <tr>
                                    <th>Betalingskenmerk</th>
                                    <th>Geplaatst op</th>
                                    <th>Betaald op</th>
                                    <th>Betaling gevalideerd</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><b>");
#nullable restore
#line 119 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                      Write(p.PaymentID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                                    <td>");
#nullable restore
#line 120 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                   Write(p.PlacedAt.ToString("dd-MM-yyyy hh:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 121 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                   Write(dt.ToString("dd-MM-yyyy hh:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td><b");
            BeginWriteAttribute("class", " class=\"", 6132, "\"", 6190, 1);
#nullable restore
#line 122 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
WriteAttributeValue("", 6140, Html.Raw((validated) ? "green-text" : "red-text"), 6140, 50, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 122 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                                 Write(Html.Raw((validated) ? "Ja" : "Nee"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n");
#nullable restore
#line 126 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                   Write(Html.Raw((!validated) ? "<h5 class=\"red-text\">Er is iets fout gegaan met de validatie. Neem a.u.b. contact op met de beheerder</h5>" : ""));

#line default
#line hidden
#nullable disable
#nullable restore
#line 126 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                                                                                                     ;
                    }
                    else
                    {
                        if (Model.Invoice.Payed)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 132 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                       Write(Html.Raw("<h5 class=\"red-text\">Er is iets fout gegaan met de validatie. Neem a.u.b. contact op met de beheerder</h5>"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 132 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                                                                                     
                        }
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <br />\r\n                    <h6 id=\"printed_at\" style=\"display: none;\">Factuur geprint op: <b>");
#nullable restore
#line 136 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                 Write(DateTime.Now.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("&nbsp;om&nbsp;");
#nullable restore
#line 136 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                                                                                                                                   Write(DateTime.Now.ToString("HH:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></h6>
                </div>
            </li>
        </ul>
    </div>

    <div class=""row"">
        <div class=""col s12 center-align"">
            <button class=""btn btn-large green waves-effect"" id=""btnprint"" onclick=""Invoice.startPrint();"">Printen</button>
");
#nullable restore
#line 145 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
             if (!Model.Invoice.Payed)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <button class=\"btn btn-large blue waves-effect\" id=\"btnnext\" onclick=\"Invoice.checkout();\">Betalen &rarr;</button>\r\n");
#nullable restore
#line 148 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n<form id=\"frmCheckout\" style=\"display: none;\" method=\"post\" action=\"/Pay/Checkout\">\r\n    <input name=\"invoiceid\"");
            BeginWriteAttribute("value", " value=\"", 7694, "\"", 7744, 1);
#nullable restore
#line 154 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
WriteAttributeValue("", 7702, Uri.EscapeDataString(Model.Invoice.PayID), 7702, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n</form>\r\n\r\n<script>\r\n    var invoiceid = \'");
#nullable restore
#line 158 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Pay\Index.cshtml"
                Write(Uri.EscapeDataString(Model.Invoice.PayID));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
    var cancelPayment = true;

    var Invoice = {
        init: function () {
            $('#_invoice, #_minvoice').addClass('active');
            M.Collapsible.getInstance($('#info')).open(2);
        },
        startPrint: function () {
            var elem = document.querySelector('.collapsible.expandable');
            var instance = M.Collapsible.init(elem, { accordion: false });
            instance.open(0); instance.open(1); instance.open(2);
            $('nav, #btnprint, #btnnext').hide();
            M.toast({ html: ""Printen..."", classes: ""blue rounded"", displayLength: 500, completeCallback: function() { Invoice.doPrint() } });
        },
        doPrint: function () {
            window.print();
            $('nav, #btnprint, #btnnext').show();
            var elem = document.querySelector('.collapsible.expandable');
            var instance = M.Collapsible.init(elem, { accordion: true });
            instance.close(0); instance.close(1); instance.close(2);
            $(");
            WriteLiteral(@"'#printedat').hide();
        },
        checkout: function () {
            $('#_progbar').show();
            $('#frmCheckout').submit();
            setTimeout(Invoice.hidePB, 10000);
        },
        hidePB: function () {
            $('#_progbar').hide();
        }
    };

    $(function () {
        $('.collapsible').collapsible();
        Invoice.init();
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.NoDB.OPayInfo> Html { get; private set; }
    }
}
#pragma warning restore 1591
