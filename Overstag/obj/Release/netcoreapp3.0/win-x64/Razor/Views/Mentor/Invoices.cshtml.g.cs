#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Invoices.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "751e8419fcc2cfd53be244255e044e8ba08c8a1e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Invoices), @"mvc.1.0.view", @"/Views/Mentor/Invoices.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"751e8419fcc2cfd53be244255e044e8ba08c8a1e", @"/Views/Mentor/Invoices.cshtml")]
    public class Views_Mentor_Invoices : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Invoices.cshtml"
  
    ViewBag.Title = "Facturering";
    Layout = "~/Views/_MentorLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""center-align"">
    <h2 class=""blue-middle-text"">Automatisch factureren</h2>
    <h6 class=""blue-dark-text"">Het algoritme op deze pagina brengt automatisch voor alle gebruikers die <b>5</b> of meer ongefactureerde activiteiten hebben staan de activiteiten in rekening dmv een factuur. 
    <br />Ook wordt naar alle betreffende gebruikers een email gestuurd met een link naar de factuur.</h6>
    <br /><br />
    <button class=""btn btn-large green darken-4 waves-effect"" id=""btngenerate"" onclick=""Invoice.generate();"">Facturen maken</button>
    <p class=""red-text"">Let op: Dit kan even duren</p>
    <br />
    
    <div id=""info"" style=""display: none;"">
        <h3 class=""blue-middle-text"">Resultaten</h3>
        <table class=""centered"">
            <thead>
                <tr>
                    <th>Aantal gebruikers gefactureerd</th>
                    <th>Aantal evenementen gefactureerd</th>
                    <th>Hoeveelheid geld gefactureerd</th>
                    <th>Aantal");
            WriteLiteral(@" mails mislukt te verzenden</th>
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

<script>
    var Invoice = {
        init: function () {
            $('#_invoices, #_minvoices').addClass('active');
        },
        generate: function () {
            $('#_progbar').show();
            $('#btngenerate').addClass('disabled');

            $.getJSON('/Mentor/autoInvoice', function (r) {
                if (r.status == 'success') {
                    M.toast({ html: 'Factuur maken gelukt', classes: 'green' });
                    $('#info').show();
                    $('#uc').text(r.usercount);
                    $('#ec').text(r.particount);
                    $('");
            WriteLiteral(@"#mc').text((r.moneycount / 100).toLocaleString(""nl-NL"", { style: ""currency"", currency: ""EUR"" }))
                    $('#fmc').text(r.failedmails);
                } else {
                    console.log(r);
                    M.toast({ html: r.error, classes: 'red' });
                }
            }).done(function () {
                $('#_progbar').hide();
            });
        }
    };

    $(function () {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591