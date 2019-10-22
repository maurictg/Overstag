#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02185a72b52e2ff0bcbe247319a284eec2a3cff2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Parent_Billing), @"mvc.1.0.view", @"/Views/Parent/Billing.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02185a72b52e2ff0bcbe247319a284eec2a3cff2", @"/Views/Parent/Billing.cshtml")]
    public class Views_Parent_Billing : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.NoDB.FUnpayed>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
  
    Layout = "~/Views/_UserLayout.cshtml";
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");
    int eventcnt = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>Overzicht van onverwerkte activiteiten per kind</h3>\r\n<table>\r\n    <thead>\r\n        <tr>\r\n            <th>Naam</th>\r\n            <th>Facturering</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 17 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
         foreach (var u in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td><b>");
#nullable restore
#line 20 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                  Write(u.User.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></td>
                <td>
                    <table>
                        <thead>
                            <tr>
                                <th>Titel</th>
                                <th>Datum</th>
                                <th>Kosten</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                             if (u.UnpayedEvents.Count() > 0)
                            {
                                double total = 0;
                                total += u.ConsumptionCost;
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                 foreach (var e in u.UnpayedEvents)
                                {
                                    eventcnt++;
                                    total += (double)e.Cost;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 40 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                       Write(e.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 41 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                       Write(e.When.ToString("D", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral(" om ");
#nullable restore
#line 41 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                                                         Write(e.When.ToString("t", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>&euro;");
#nullable restore
#line 42 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                             Write(Math.Round((double)e.Cost / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 44 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td class=\"center-align\" colspan=\"3\">\r\n                                        Totaal aantal drankjes: <b>");
#nullable restore
#line 47 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                                              Write(u.ConsumptionCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> (&euro;1,00/st.)<br />\r\n                                        Bedrag: <b>&euro;");
#nullable restore
#line 48 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                                    Write(Math.Round(total / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n");
#nullable restore
#line 49 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                         if(u.FriendCount > 0)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <p class=\"red-text\">LET OP: tijdens de avonden had ");
#nullable restore
#line 51 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                                                                          Write(u.User.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <b>");
#nullable restore
#line 51 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                                                                                               Write(u.FriendCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> keer een vriend(in) mee. Dit kan vanwege technische redenen niet hier berekend worden. Op de factuur wordt dit er nog bij gerekend. Op de factuur worden deze avonden dubbel gerekend.</p>\r\n");
#nullable restore
#line 52 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 55 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td colspan=\"3\" class=\"center-align\"><h6>Geen onverwerkte ongefactureerde avonden. Kijk onder &quot;Betalingen&quot;</h6></td>\r\n                                </tr>\r\n");
#nullable restore
#line 61 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 66 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
#nullable restore
#line 70 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
 if (eventcnt > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""center-align"">
        <br />
        <a class=""waves-effect waves-light btn blue-dark"" id=""btngenerate"">Verwerken</a>
        <div class=""progress"" style=""display: none;""><div class=""indeterminate""></div></div>
    </div>
    <br />
");
#nullable restore
#line 78 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Billing.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h3>Facturen</h3>
<h6>De facturen staan onder <b>&quot;<a href=""/User/Payment"">Betalingen</a>&quot;</b> in het menu.</h6>

<div class=""fixed-action-btn"">
    <a class=""btn-floating btn-large blue-dark"" href=""/Parent"">
        <i class=""large material-icons"">arrow_back</i>
    </a>
</div>


<script>
    var Billing = {
        init: function () {
            $('#_family, #_mfamily').addClass('active');
            Billing.mapEvents();
        },
        mapEvents: function () {
            $('#btngenerate').click(function () {
                $('#progress').show();
                $.get('/Parent/GenerateInvoice', function (r) {
                    if (r.status == 'success') {
                        M.toast({ html: 'Factuur successvol gemaakt!', classes: 'green' })
                        setTimeout(window.location.reload.bind(window.location), 1000);
                    } else {
                        M.toast({ html: 'Er is iets fout gegaan. Probeer het later opnieuw', classes: 'red");
            WriteLiteral("\' })\r\n                    }\r\n                    $(\'#progress\').hide();\r\n                },\'json\');\r\n            });\r\n        }\r\n    }\r\n\r\n    $(function () {\r\n        Billing.init();\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.NoDB.FUnpayed>> Html { get; private set; }
    }
}
#pragma warning restore 1591
