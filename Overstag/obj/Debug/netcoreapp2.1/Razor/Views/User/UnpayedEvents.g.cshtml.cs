#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b76891b0fd9b55d629dc966db22feb93b762f2b9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UnpayedEvents), @"mvc.1.0.view", @"/Views/User/UnpayedEvents.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/UnpayedEvents.cshtml", typeof(AspNetCore.Views_User_UnpayedEvents))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b76891b0fd9b55d629dc966db22feb93b762f2b9", @"/Views/User/UnpayedEvents.cshtml")]
    public class Views_User_UnpayedEvents : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Event>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
  
    Layout = null;
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");    

#line default
#line hidden
            BeginContext(162, 263, true);
            WriteLiteral(@"    <table class=""responsive-table"">
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
            EndContext();
#line 16 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
              double topay = 0; 

#line default
#line hidden
            BeginContext(460, 12, true);
            WriteLiteral("            ");
            EndContext();
#line 17 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
             foreach (var e in Model)
            {
                double c = (double)e.Cost;
                topay += c;

#line default
#line hidden
            BeginContext(587, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(634, 7, false);
#line 22 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
                   Write(e.Title);

#line default
#line hidden
            EndContext();
            BeginContext(641, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(673, 28, false);
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
                   Write(e.When.ToString("D",culture));

#line default
#line hidden
            EndContext();
            BeginContext(701, 4, true);
            WriteLiteral(" om ");
            EndContext();
            BeginContext(706, 28, false);
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
                                                    Write(e.When.ToString("t",culture));

#line default
#line hidden
            EndContext();
            BeginContext(734, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(766, 13, false);
#line 24 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
                   Write(e.Description);

#line default
#line hidden
            EndContext();
            BeginContext(779, 37, true);
            WriteLiteral("</td>\r\n                    <td>&euro;");
            EndContext();
            BeginContext(817, 36, false);
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
                         Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
            EndContext();
            BeginContext(853, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 27 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
            }

#line default
#line hidden
            BeginContext(898, 119, true);
            WriteLiteral("            <tr class=\"grey lighten-4\">\r\n                <td colspan=\"4\" class=\"center\"><b>Totaal te verrekenen: &euro;");
            EndContext();
            BeginContext(1018, 59, false);
#line 29 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\UnpayedEvents.cshtml"
                                                                         Write(Math.Round(Convert.ToDecimal(topay) / 100, 2).ToString("F"));

#line default
#line hidden
            EndContext();
            BeginContext(1077, 62, true);
            WriteLiteral("</b></td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Event>> Html { get; private set; }
    }
}
#pragma warning restore 1591
