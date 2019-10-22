#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "296c4e14a292f9645d53242914285150f9090656"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Events), @"mvc.1.0.view", @"/Views/Home/Events.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"296c4e14a292f9645d53242914285150f9090656", @"/Views/Home/Events.cshtml")]
    public class Views_Home_Events : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Event[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
  
    ViewBag.Title = "Overstag - Activiteiten";
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");
    ViewBag.Description = "Stichting Overstag's activiteiten. Bekijk onze agenda en schrijf je in voor een van onze activiteiten!";
    ViewBag.Keywords = "Overstag, stichting overstag, Rilland, Reimerswaal, jongerenavonden rilland, jongerenavonden reimerswaal, overstag inschrijven, overstag meedoen";
    Overstag.Models.Event[] Passed = ViewBag.Passed;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1 class=""blue-middle-text center-align"">Agenda</h1>
<h5 class=""blue-middle-text"">&nbsp;Komende evenementen, onder het voorbehoud van <a href=""https://herzienestatenvertaling.nl/teksten/jakobus/4#15"" target=""_blank"">Jakobus 4:15</a></h5>

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
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
         if (Model.Length > 0)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
             foreach (var e in Model)
            {
                bool iss = Overstag.Core.General.DateIsPassed(e.When);
                double c = (double)e.Cost;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 30 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(Html.Raw((iss) ? "(<b class=\"red-text\">te laat</b>) " : ""));

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                                                                                 Write(e.When.ToString("D", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral(" om ");
#nullable restore
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                                                                                                                   Write(e.When.ToString("t", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 32 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>&euro;");
#nullable restore
#line 33 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                         Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 35 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
             
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td colspan=\"5\"><h5>Nog geen activiteiten</h5></td>\r\n            </tr>\r\n");
#nullable restore
#line 42 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>

<br />
<h3 class=""center-align blue-middle-text"">Afgelopen activiteiten</h3>

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
#line 59 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
         if (Passed.Length > 0)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
             foreach (var e in Passed)
            {
                double c = (double)e.Cost;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 65 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 66 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.When.ToString("D", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral(" om ");
#nullable restore
#line 66 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                                                     Write(e.When.ToString("t", culture));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 67 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                   Write(e.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>&euro;");
#nullable restore
#line 68 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
                         Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 70 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
             
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td colspan=\"5\"><h5>Nog geen activiteiten</h5></td>\r\n            </tr>\r\n");
#nullable restore
#line 77 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Home\Events.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>
<br />
<script>
    var Events = {
        init: function () {
            $('#_events, #_mevents').addClass('active');
            if (Overstag.type == 1)
                $('#btnsub').addClass('disabled');
        },
        sub: function () {
            if (!Overstag.loggedin) {
                localStorage.setItem(""redirect"", ""/User/Events"");
                $(location).attr('href', '/Register/Login');
            } else {
                $(location).attr('href', '/User/Events');
            }
        }
    }
    Events.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Event[]> Html { get; private set; }
    }
}
#pragma warning restore 1591