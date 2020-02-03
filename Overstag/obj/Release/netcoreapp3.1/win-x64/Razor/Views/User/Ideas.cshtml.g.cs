#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42c99845780c1826750e7b4e88185b450b4e2b5f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Ideas), @"mvc.1.0.view", @"/Views/User/Ideas.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42c99845780c1826750e7b4e88185b450b4e2b5f", @"/Views/User/Ideas.cshtml")]
    public class Views_User_Ideas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Idea>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<table>\r\n    <thead>\r\n        <tr>\r\n            <th>Titel</th>\r\n            <th>Omschrijving</th>\r\n            <th>~Kosten p.p.</th>\r\n            <th>Vote</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 15 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
         foreach (var idea in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    <b>");
#nullable restore
#line 19 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                  Write(idea.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("&nbsp;</b><br />\r\n                    <div class=\"right\">\r\n                        <span class=\"new badge green\" data-badge-caption=\"likes\"><b");
            BeginWriteAttribute("id", " id=\"", 530, "\"", 549, 2);
            WriteAttributeValue("", 535, "likes-", 535, 6, true);
#nullable restore
#line 21 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue("", 541, idea.Id, 541, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 21 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                                                                                                   Write(idea.Votes.Count(i => i.Upvote));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></span>\r\n                        \r\n                        <span class=\"new badge red\" data-badge-caption=\"dislikes\"><b");
            BeginWriteAttribute("id", " id=\"", 706, "\"", 728, 2);
            WriteAttributeValue("", 711, "dislikes-", 711, 9, true);
#nullable restore
#line 23 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue("", 720, idea.Id, 720, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 23 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                                                                                                       Write(idea.Votes.Count(i => !i.Upvote));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></span>\r\n                    </div>\r\n                </td>\r\n                <td>");
#nullable restore
#line 26 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
               Write(idea.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <th>");
#nullable restore
#line 27 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
               Write(Html.Raw((idea.Cost == null) ? "?" : idea.Cost));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <td>\r\n");
#nullable restore
#line 29 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                     if (idea.Votes.Any(i => i.UserID == ViewBag.UserID))
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("id", " id=\"", 1092, "\"", 1110, 2);
            WriteAttributeValue("", 1097, "like-", 1097, 5, true);
#nullable restore
#line 31 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue("", 1102, idea.Id, 1102, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 31 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                                                  Write(idea.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("class", " class=\"", 1130, "\"", 1268, 5);
            WriteAttributeValue("", 1138, "likebtn", 1138, 7, true);
            WriteAttributeValue(" ", 1145, "btn-floating", 1146, 13, true);
            WriteAttributeValue(" ", 1158, "waves-effect", 1159, 13, true);
            WriteAttributeValue(" ", 1171, "waves-green", 1172, 12, true);
#nullable restore
#line 31 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue(" ", 1183, Html.Raw((idea.Votes.First(i => i.UserID == ViewBag.UserID).Upvote)?"green":"grey"), 1184, 84, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"material-icons\">thumb_up</i></a>\r\n                        <a");
            BeginWriteAttribute("id", " id=\"", 1340, "\"", 1361, 2);
            WriteAttributeValue("", 1345, "dislike-", 1345, 8, true);
#nullable restore
#line 32 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue("", 1353, idea.Id, 1353, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 32 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                                                     Write(idea.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("class", " class=\"", 1381, "\"", 1519, 5);
            WriteAttributeValue("", 1389, "dislikebtn", 1389, 10, true);
            WriteAttributeValue(" ", 1399, "btn-floating", 1400, 13, true);
            WriteAttributeValue(" ", 1412, "waves-effect", 1413, 13, true);
            WriteAttributeValue(" ", 1425, "waves-red", 1426, 10, true);
#nullable restore
#line 32 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue(" ", 1435, Html.Raw(!(idea.Votes.First(i => i.UserID == ViewBag.UserID).Upvote)?"red":"grey"), 1436, 83, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"material-icons\">thumb_down</i></a>\r\n");
#nullable restore
#line 33 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("id", " id=\"", 1665, "\"", 1683, 2);
            WriteAttributeValue("", 1670, "like-", 1670, 5, true);
#nullable restore
#line 36 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue("", 1675, idea.Id, 1675, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 36 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                                                  Write(idea.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"likebtn btn-floating waves-effect waves-green grey\"><i class=\"material-icons\">thumb_up</i></a>\r\n                        <a");
            BeginWriteAttribute("id", " id=\"", 1833, "\"", 1854, 2);
            WriteAttributeValue("", 1838, "dislike-", 1838, 8, true);
#nullable restore
#line 37 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
WriteAttributeValue("", 1846, idea.Id, 1846, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 37 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                                                     Write(idea.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"dislikebtn btn-floating waves-effect waves-red grey\"><i class=\"material-icons\">thumb_down</i></a>\r\n");
#nullable restore
#line 38 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 41 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Ideas.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n<br />");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Idea>> Html { get; private set; }
    }
}
#pragma warning restore 1591
