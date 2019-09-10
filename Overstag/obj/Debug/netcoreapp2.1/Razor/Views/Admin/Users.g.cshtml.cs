#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5eb7386c6bed3230ca02c17c3728bbd31b57594b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Users), @"mvc.1.0.view", @"/Views/Admin/Users.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Users.cshtml", typeof(AspNetCore.Views_Admin_Users))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5eb7386c6bed3230ca02c17c3728bbd31b57594b", @"/Views/Admin/Users.cshtml")]
    public class Views_Admin_Users : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Account>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
  
    Layout = "/Views/_AdminLayout.cshtml";
    ViewBag.Title = "Gebruikers";
    string[] types = { "Lid", "Ouder", "Mentor", "Administrator" };

#line default
#line hidden
            BeginContext(193, 527, true);
            WriteLiteral(@"<br />
<div class=""center-align white-text"">
    <h3>Gebruikersoverzicht</h3>
    <br />
    <table class=""table text-white"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Gebruikersnaam</th>
                <th>Emailadres</th>
                <th>Voornaam</th>
                <th>Achternaam</th>
                <th>Adres</th>
                <th>Token</th>
                <th>Type</th>
                <th>Acties</th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 26 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
             foreach(var user in Model)
            {

#line default
#line hidden
            BeginContext(776, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(823, 7, false);
#line 29 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                   Write(user.Id);

#line default
#line hidden
            EndContext();
            BeginContext(830, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(862, 13, false);
#line 30 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                   Write(user.Username);

#line default
#line hidden
            EndContext();
            BeginContext(875, 33, true);
            WriteLiteral("</td>\r\n                    <td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 908, "\"", 933, 2);
            WriteAttributeValue("", 915, "mailto:", 915, 7, true);
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
WriteAttributeValue("", 922, user.Email, 922, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(934, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(936, 10, false);
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                                                Write(user.Email);

#line default
#line hidden
            EndContext();
            BeginContext(946, 38, true);
            WriteLiteral("</a></td>\r\n                    <td><b>");
            EndContext();
            BeginContext(985, 14, false);
#line 32 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                      Write(user.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(999, 38, true);
            WriteLiteral("</b></td>\r\n                    <td><b>");
            EndContext();
            BeginContext(1038, 13, false);
#line 33 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                      Write(user.Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(1051, 35, true);
            WriteLiteral("</b></td>\r\n                    <td>");
            EndContext();
            BeginContext(1087, 11, false);
#line 34 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                   Write(user.Adress);

#line default
#line hidden
            EndContext();
            BeginContext(1098, 9, true);
            WriteLiteral(" ,&nbsp; ");
            EndContext();
            BeginContext(1108, 15, false);
#line 34 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                                        Write(user.Postalcode);

#line default
#line hidden
            EndContext();
            BeginContext(1123, 8, true);
            WriteLiteral(" &nbsp; ");
            EndContext();
            BeginContext(1132, 14, false);
#line 34 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                                                                Write(user.Residence);

#line default
#line hidden
            EndContext();
            BeginContext(1146, 34, true);
            WriteLiteral("</td>\r\n                    <td><i>");
            EndContext();
            BeginContext(1181, 10, false);
#line 35 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                      Write(user.Token);

#line default
#line hidden
            EndContext();
            BeginContext(1191, 61, true);
            WriteLiteral("</i></td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1253, 16, false);
#line 37 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
                   Write(types[user.Type]);

#line default
#line hidden
            EndContext();
            BeginContext(1269, 53, true);
            WriteLiteral("<br />\r\n                        <a href=\"javascript:\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1322, "\"", 1357, 3);
            WriteAttributeValue("", 1332, "Admin.grade(0,\'", 1332, 15, true);
#line 38 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
WriteAttributeValue("", 1347, user.Id, 1347, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 1355, "\')", 1355, 2, true);
            EndWriteAttribute();
            BeginContext(1358, 33, true);
            WriteLiteral(">- down</a>|<a href=\"javascript:\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1391, "\"", 1426, 3);
            WriteAttributeValue("", 1401, "Admin.grade(1,\'", 1401, 15, true);
#line 38 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
WriteAttributeValue("", 1416, user.Id, 1416, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 1424, "\')", 1424, 2, true);
            EndWriteAttribute();
            BeginContext(1427, 64, true);
            WriteLiteral(">+ up</a>\r\n                    </td>\r\n                    <td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1491, "\"", 1551, 2);
            WriteAttributeValue("", 1498, "/Register/Passreset/", 1498, 20, true);
#line 40 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
WriteAttributeValue("", 1518, Uri.EscapeDataString(user.Token), 1518, 33, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1552, 18, true);
            WriteLiteral(">Reset</a><br /><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1570, "\"", 1625, 2);
            WriteAttributeValue("", 1577, "/Admin/Loginas/", 1577, 15, true);
#line 40 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
WriteAttributeValue("", 1592, Uri.EscapeDataString(user.Token), 1592, 33, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1626, 40, true);
            WriteLiteral(">Login</a></td>\r\n                </tr>\r\n");
            EndContext();
#line 42 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Users.cshtml"
            }

#line default
#line hidden
            BeginContext(1681, 311, true);
            WriteLiteral(@"        </tbody>
    </table>
</div>
<script>
    $('#_users').addClass('active');

    var Admin = {
        init: function () {

        },
        grade: function (up, id) {
            $.get('/Admin/upgrade/' + up + '/' + id, function (r) {

            }, 'json');
        }
    }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Account>> Html { get; private set; }
    }
}
#pragma warning restore 1591
