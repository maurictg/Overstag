#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "74ed100c378da9590adea132c29f901c8519086b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Participators), @"mvc.1.0.view", @"/Views/Admin/Participators.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Participators.cshtml", typeof(AspNetCore.Views_Admin_Participators))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"74ed100c378da9590adea132c29f901c8519086b", @"/Views/Admin/Participators.cshtml")]
    public class Views_Admin_Participators : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.NoDB.AParticipator>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
  
    Layout = null;
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");
    int index = 0;

#line default
#line hidden
            BeginContext(191, 64, true);
            WriteLiteral("<div>\r\n    <div class=\"accordion bg-dark\" id=\"partiaccordion\">\r\n");
            EndContext();
#line 9 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
         foreach (var e in Model.ToArray().Reverse())
        {
            index++;

#line default
#line hidden
            BeginContext(343, 80, true);
            WriteLiteral("            <div class=\"card bg-dark\">\r\n                <div class=\"card-header\"");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 423, "\"", 437, 2);
            WriteAttributeValue("", 428, "hc_", 428, 3, true);
#line 13 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
WriteAttributeValue("", 431, index, 431, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(438, 199, true);
            WriteLiteral(">\r\n                    <div class=\"row\">\r\n                        <h2 class=\"mb-0\">\r\n                            <button class=\"btn text-white\" type=\"button\" data-toggle=\"collapse\" data-target=\"#col_");
            EndContext();
            BeginContext(638, 5, false);
#line 16 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                                                                                             Write(index);

#line default
#line hidden
            EndContext();
            BeginContext(643, 22, true);
            WriteLiteral("\" aria-expanded=\"true\"");
            EndContext();
            BeginWriteAttribute("aria-controls", " aria-controls=\"", 665, "\"", 691, 2);
            WriteAttributeValue("", 681, "col_", 681, 4, true);
#line 16 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
WriteAttributeValue("", 685, index, 685, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(692, 116, true);
            WriteLiteral(">\r\n                                <i class=\"material-icons\">calendar_today</i>\r\n                                <b>");
            EndContext();
            BeginContext(809, 13, false);
#line 18 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                              Write(e.Event.Title);

#line default
#line hidden
            EndContext();
            BeginContext(822, 24, true);
            WriteLiteral("</b> &nbsp;&nbsp;&nbsp; ");
            EndContext();
            BeginContext(847, 35, false);
#line 18 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                                                    Write(e.Event.When.ToString("d", culture));

#line default
#line hidden
            EndContext();
            BeginContext(883, 16, false);
#line 18 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                                                                                        Write(Html.Raw(" om "));

#line default
#line hidden
            EndContext();
            BeginContext(900, 35, false);
#line 18 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                                                                                                         Write(e.Event.When.ToString("t", culture));

#line default
#line hidden
            EndContext();
            BeginContext(935, 144, true);
            WriteLiteral("\r\n                            </button>\r\n                        </h2>\r\n                    </div>\r\n                </div>\r\n                <div");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 1079, "\"", 1094, 2);
            WriteAttributeValue("", 1084, "col_", 1084, 4, true);
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
WriteAttributeValue("", 1088, index, 1088, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1095, 17, true);
            WriteLiteral(" class=\"collapse\"");
            EndContext();
            BeginWriteAttribute("aria-labelledby", " aria-labelledby=\"", 1112, "\"", 1139, 2);
            WriteAttributeValue("", 1130, "hc_", 1130, 3, true);
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
WriteAttributeValue("", 1133, index, 1133, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1140, 122, true);
            WriteLiteral(" data-parent=\"#partiaccordion\">\r\n                    <div class=\"card-body\">\r\n                        <p>Omschrijving: <b>");
            EndContext();
            BeginContext(1263, 19, false);
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                       Write(e.Event.Description);

#line default
#line hidden
            EndContext();
            BeginContext(1282, 36, true);
            WriteLiteral("</b> &nbsp;-&nbsp; Kosten: <b>&euro;");
            EndContext();
            BeginContext(1319, 55, false);
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                                                                               Write(Math.Round((double)e.Event.Cost / 100, 2).ToString("F"));

#line default
#line hidden
            EndContext();
            BeginContext(1374, 428, true);
            WriteLiteral(@"</b></p>
                        <table class=""table text-white"">
                            <thead>
                                <tr>
                                    <th>Voornaam</th>
                                    <th>Achternaam</th>
                                    <th>Gefactureerd</th>
                                </tr>
                            </thead>
                            <tbody>
");
            EndContext();
#line 35 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                 for (int i = 0; i < e.Accounts.Length; i++)
                                {

#line default
#line hidden
            BeginContext(1915, 86, true);
            WriteLiteral("                                    <tr>\r\n                                        <td>");
            EndContext();
            BeginContext(2002, 23, false);
#line 38 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                       Write(e.Accounts[i].Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(2025, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(2077, 22, false);
#line 39 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                       Write(e.Accounts[i].Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(2099, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(2151, 107, false);
#line 40 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                       Write(Html.Raw((e.Factured[i]) ? "<span class='text-success'>Ja</span>" : "<span class='text-danger'>Nee</span>"));

#line default
#line hidden
            EndContext();
            BeginContext(2258, 50, true);
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
            EndContext();
#line 42 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
                                }

#line default
#line hidden
            BeginContext(2343, 144, true);
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 48 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Participators.cshtml"
        }

#line default
#line hidden
            BeginContext(2498, 173, true);
            WriteLiteral("    </div>\r\n</div>\r\n\r\n<script>\r\n    $(document).ready(function () {\r\n        //$(\'.collapsible\').collapsible();\r\n        $(\'.collapse\').collapse(\'hide\');\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.NoDB.AParticipator>> Html { get; private set; }
    }
}
#pragma warning restore 1591
