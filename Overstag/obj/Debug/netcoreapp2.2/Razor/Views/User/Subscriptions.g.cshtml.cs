#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2b011cc1cf0fdc0b16023097d17e1ac73ab59ede"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Subscriptions), @"mvc.1.0.view", @"/Views/User/Subscriptions.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Subscriptions.cshtml", typeof(AspNetCore.Views_User_Subscriptions))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2b011cc1cf0fdc0b16023097d17e1ac73ab59ede", @"/Views/User/Subscriptions.cshtml")]
    public class Views_User_Subscriptions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.NoDB.Subscriptions>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
   
    Layout = null;
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");    

#line default
#line hidden
            BeginContext(170, 252, true);
            WriteLiteral("<table class=\"responsive-table\">\r\n    <thead>\r\n        <tr>\r\n            <th>Wat</th>\r\n            <th>Wanneer</th>\r\n            <th>Omschrijving</th>\r\n            <th>Kosten</th>\r\n            <th>Acties</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 17 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
         foreach (var e in Model.Events)
        {
            if (!Overstag.Core.General.DateIsPassed(e.When))
            {
                double c = (double)e.Cost;

#line default
#line hidden
            BeginContext(596, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(643, 7, false);
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
                   Write(e.Title);

#line default
#line hidden
            EndContext();
            BeginContext(650, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(682, 28, false);
#line 24 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
                   Write(e.When.ToString("D",culture));

#line default
#line hidden
            EndContext();
            BeginContext(710, 4, true);
            WriteLiteral(" om ");
            EndContext();
            BeginContext(715, 28, false);
#line 24 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
                                                    Write(e.When.ToString("t",culture));

#line default
#line hidden
            EndContext();
            BeginContext(743, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(775, 13, false);
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
                   Write(e.Description);

#line default
#line hidden
            EndContext();
            BeginContext(788, 37, true);
            WriteLiteral("</td>\r\n                    <td>&euro;");
            EndContext();
            BeginContext(826, 36, false);
#line 26 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
                         Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
            EndContext();
            BeginContext(862, 60, true);
            WriteLiteral("</td>\r\n                    <td><a data-target=\"uitschrijven\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 922, "\"", 954, 4);
            WriteAttributeValue("", 932, "eventtoremove", 932, 13, true);
            WriteAttributeValue(" ", 945, "=", 946, 2, true);
#line 27 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
WriteAttributeValue(" ", 947, e.Id, 948, 5, false);

#line default
#line hidden
            WriteAttributeValue("", 953, ";", 953, 1, true);
            EndWriteAttribute();
            BeginContext(955, 69, true);
            WriteLiteral(" class=\"modal-trigger\">Uitschrijven</a></td>\r\n                </tr>\r\n");
            EndContext();
#line 29 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Subscriptions.cshtml"
            }
        }

#line default
#line hidden
            BeginContext(1050, 22, true);
            WriteLiteral("    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.NoDB.Subscriptions> Html { get; private set; }
    }
}
#pragma warning restore 1591
