#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3d8cdde46e18216be99cfa60f25added3c37d0db"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Accountancy_Invoices), @"mvc.1.0.view", @"/Views/Mentor/Accountancy/Invoices.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d8cdde46e18216be99cfa60f25added3c37d0db", @"/Views/Mentor/Accountancy/Invoices.cshtml")]
    public class Views_Mentor_Accountancy_Invoices : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Invoice>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
  
    ViewBag.Title = "Facturering";
    Layout = "_AccountancyLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <div class=""center-align"">
        <h2 class=""blue-middle-text"">Openstaande facturen</h2>
        <table>
            <thead>
                <tr>
                    <th>Naam</th>
                    <th>Datum</th>
                    <th>Bedrag</th>
                    <th>Acties</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 19 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n");
#nullable restore
#line 23 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                          
                            var user = new Overstag.Models.OverstagContext().Accounts.First(f => f.Id == item.UserID);
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                       Write(Html.Raw(user.Firstname+"&nbsp;"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                                                         Write(Html.Raw(user.Lastname));

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                        <td>");
#nullable restore
#line 28 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                       Write(item.Timestamp.ToString("dd-MM-yyyy HH:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>&euro;");
#nullable restore
#line 29 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                             Write(Math.Round((double)item.Amount/100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            <a class=\"btn red darken-4 waves-effect\"");
            BeginWriteAttribute("href", " href=\"", 1175, "\"", 1230, 2);
            WriteAttributeValue("", 1182, "/Pay/Invoice/", 1182, 13, true);
#nullable restore
#line 31 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
WriteAttributeValue("", 1195, Uri.UnescapeDataString(item.PayID), 1195, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\">Openen</a>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 34 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Invoices.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>

        <h2 class=""blue-middle-text"">Automatisch factureren</h2>
        <h6 class=""blue-dark-text"">
            Het algoritme op deze pagina brengt automatisch voor alle gebruikers die <input style=""width: 3em"" type=""number"" max=""10"" min=""1"" id=""amount"" value=""5"" class=""col s1 center-align centered"" /> of meer ongefactureerde activiteiten hebben staan de activiteiten in rekening dmv een factuur.
            <br />Ook wordt naar alle betreffende gebruikers een email gestuurd met een link naar de factuur.
        </h6>
        <br /><br />

        <a class=""btn btn-large red darken-4 waves-effect disabled"" id=""btngenerate"" onclick=""M.Modal.getInstance($('#areusure')).open()"">Facturen maken</a>
        <p class=""red-text"">Let op: Dit kan even duren</p>
        <br />

        <div id=""info"" style=""display: none;"">
            <h3 class=""blue-middle-text"">Resultaten</h3>
            <table class=""centered"">
                <thead>
                    <tr>");
            WriteLiteral(@"
                        <th>Aantal gebruikers gefactureerd</th>
                        <th>Aantal evenementen gefactureerd</th>
                        <th>Hoeveelheid geld gefactureerd</th>
                        <th>Aantal mails mislukt te verzenden</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><b id=""uc"">0</b></td>
                        <td><b id=""ec"">0</b></td>
                        <td><b id=""mc"">0</b></td>
                        <td><b id=""fmc"" class=""red-text"">0</b></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

<div id=""areusure"" class=""modal"">
    <div class=""modal-content"">
        <h4>Weet je zeker dat je dit algoritme uit wilt voeren?</h4>
        <h6>Alle inschrijvingen van leden met een familie worden verwerkt (toegevoegd aan ouder) en alle mensen met meer dan 5 openstaande avonden krijgen een factuur.</h6>
    </div>
    ");
            WriteLiteral(@"<div class=""modal-footer"">
        <button class=""btn modal-close waves-effect"">Annuleren</button>
        <button class=""btn green modal-close waves-effect"" onclick=""Invoice.preGenerate();"">Uitvoeren</button>
    </div>
</div>

<script>
    var generateSucceed = false;
    var preGenerateSucceed = false;

    var Invoice = {
        init: function () {
            $('#_invoices, #_minvoices').addClass('active');
            $('#btngenerate').removeClass('disabled');

            if (sessionStorage.getItem('disablegenerate') == 1) {
                $('#btngenerate').addClass('disabled');
                if (sessionStorage.getItem('info')) {
                    $('#info').html(sessionStorage.getItem('info'));
                    $('#info').show();
                }
            }
        },
        generate: function () {
            $.post('/Accountancy/autoInvoice', {amount: $('#amount').val()}, function (r) {
                if (r.status == 'success') {
                    generateS");
            WriteLiteral(@"ucceed = true;
                    M.toast({ html: 'Automatisch factureren gelukt', classes: 'green' });
                    sessionStorage.setItem('disablegenerate', 1);
                    $('#info').show();
                    $('#uc').text(r.usercount);
                    $('#ec').text(r.particount);
                    $('#mc').text((r.moneycount / 100).toLocaleString(""nl-NL"", { style: ""currency"", currency: ""EUR"" }))
                    $('#fmc').text(r.failedmails);
                    sessionStorage.setItem('info', $('#info').html());
                } else {
                    console.log(r);
                    M.toast({ html: r.error, classes: 'red' });
                }
            }).done(function () {
                setTimeout(OverstagJS.Loader.hide, 200);
                Core.doReload(500);
            }).fail(function () {
                OverstagJS.Loader.hide();
                $('#_progbar').hide();
                M.toast({ html: 'Er is iets flink fout gegaan', classes");
            WriteLiteral(@": 'red' });
            });
        },
        preGenerate: function () {
            OverstagJS.Loader.show();
            $('#_progbar').show();
            $('#btngenerate').addClass('disabled');

            $.getJSON('/Accountancy/processPUE', function (r) {
                if (r.succeed >= 0) {
                    M.toast({ html: 'Automatisch verwerken families voltooid:<br> ' + r.count + ' inschrijvingen verwerkt', classes: 'blue' });
                    preGenerateSucceed = true;
                    if (r.failed > 0) {
                        M.toast({ html: 'Verwerken van ' + r.failed + ' mislukt', classes: 'orange' });
                    }
                }
            }).done(function () {
                Invoice.generate();
            }).fail(function () {
                setTimeout(Invoice.generate, 200);
                M.toast({ html: 'Automatisch verwerken van families mislukt', classes: 'red' });
            });
        },
        preInit: function () {
            O");
            WriteLiteral("verstagJS.Loader.init(Invoice.init);\r\n        }\r\n    };\r\n\r\n    $(function () {\r\n        Invoice.preInit();\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Invoice>> Html { get; private set; }
    }
}
#pragma warning restore 1591
