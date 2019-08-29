#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6dbbe18b451dc906151d958a962be92607398fb4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Events), @"mvc.1.0.view", @"/Views/Home/Events.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Events.cshtml", typeof(AspNetCore.Views_Home_Events))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6dbbe18b451dc906151d958a962be92607398fb4", @"/Views/Home/Events.cshtml")]
    public class Views_Home_Events : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Event[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
  
    ViewBag.Title = "Activiteiten";
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");

#line default
#line hidden
            BeginContext(171, 254, true);
            WriteLiteral("<table class=\"responsive-table\">\r\n    <thead>\r\n        <tr>\r\n            <th>Wat</th>\r\n            <th>Wanneer</th>\r\n            <th>Omschrijving</th>\r\n            <th>Kosten</th>\r\n            <th>Meedoen?</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 17 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
         if (Model.Length > 0)
        {
            

#line default
#line hidden
#line 19 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
             foreach (var e in Model)
            {
                double c = (double)e.Cost;

#line default
#line hidden
            BeginContext(566, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(613, 7, false);
#line 23 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.Title);

#line default
#line hidden
            EndContext();
            BeginContext(620, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(652, 29, false);
#line 24 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.When.ToString("D", culture));

#line default
#line hidden
            EndContext();
            BeginContext(681, 4, true);
            WriteLiteral(" om ");
            EndContext();
            BeginContext(686, 29, false);
#line 24 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
                                                     Write(e.When.ToString("t", culture));

#line default
#line hidden
            EndContext();
            BeginContext(715, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(747, 13, false);
#line 25 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.Description);

#line default
#line hidden
            EndContext();
            BeginContext(760, 37, true);
            WriteLiteral("</td>\r\n                    <td>&euro;");
            EndContext();
            BeginContext(798, 36, false);
#line 26 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
                         Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
            EndContext();
            BeginContext(834, 33, true);
            WriteLiteral("</td>\r\n                    <td><a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 867, "\"", 901, 3);
            WriteAttributeValue("", 877, "Events.subscribe(", 877, 17, true);
#line 27 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
WriteAttributeValue("", 894, e.Id, 894, 5, false);

#line default
#line hidden
            WriteAttributeValue("", 899, ");", 899, 2, true);
            EndWriteAttribute();
            BeginContext(902, 46, true);
            WriteLiteral(">Inschrijven</a></td>\r\n                </tr>\r\n");
            EndContext();
#line 29 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
            }

#line default
#line hidden
#line 29 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
             
        }
        else
        {

#line default
#line hidden
            BeginContext(999, 106, true);
            WriteLiteral("            <tr>\r\n                <td colspan=\"5\"><h5>Nog geen activiteiten</h5></td>\r\n            </tr>\r\n");
            EndContext();
#line 36 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Events.cshtml"
        }

#line default
#line hidden
            BeginContext(1116, 1029, true);
            WriteLiteral(@"    </tbody>
</table>
<script>
    var Events = {
        init: function () {
            $('#_events').addClass('active');
            $('#_mevents').addClass('active');
        },
        subscribe: async function (id) {
            if (!Overstag.loggedin) {
                localStorage.setItem(""redirect"", ""/Home/Events"");
                $(location).attr('href', '/Register');
            } else {
                //Inschrijven
                $.post(""/User/postSubscribeEvent"", { EventId: id }, function (response) {
                    if (response.status == 'success') {
                        M.toast({ html: 'Inschrijving gelukt! Leuk dat je komt!', classes: 'green' });
                        User.loadcontent();
                    }
                    else {
                        //status = error
                        M.toast({ html: response.error, classes: 'red' });
                    }
                }, 'json');
            }
        }
    }

    Events.init();
</sc");
            WriteLiteral("ript>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Event[]> Html { get; private set; }
    }
}
#pragma warning restore 1591
