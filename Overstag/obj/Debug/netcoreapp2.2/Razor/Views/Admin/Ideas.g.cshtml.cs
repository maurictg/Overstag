#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e23887e09ef2ff7eea6a98232e8ae3184e91429"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Ideas), @"mvc.1.0.view", @"/Views/Admin/Ideas.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Ideas.cshtml", typeof(AspNetCore.Views_Admin_Ideas))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e23887e09ef2ff7eea6a98232e8ae3184e91429", @"/Views/Admin/Ideas.cshtml")]
    public class Views_Admin_Ideas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Idea>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
  
    ViewBag.Title = "Ideeen";
    Layout = "~/Views/_AdminLayout.cshtml";

#line default
#line hidden
            BeginContext(118, 333, true);
            WriteLiteral(@"
    <div>
        <br />
        <h2>Ideeën voor avonden</h2>
        <table class=""table text-white"">
            <thead>
                <tr>
                    <th>Titel</th>
                    <th>Omschrijving</th>
                    <th>Actie</th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 19 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                 foreach (var idea in Model)
                {

#line default
#line hidden
            BeginContext(516, 87, true);
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            <b>");
            EndContext();
            BeginContext(604, 10, false);
#line 23 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                          Write(idea.Title);

#line default
#line hidden
            EndContext();
            BeginContext(614, 132, true);
            WriteLiteral("</b>\r\n                            <div class=\"right\">\r\n                                <span class=\"badge badge-pill badge-success\">");
            EndContext();
            BeginContext(747, 36, false);
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                                                                        Write(idea.Votes.Count(i => i.Upvote == 1));

#line default
#line hidden
            EndContext();
            BeginContext(783, 91, true);
            WriteLiteral(" likes</span>\r\n                                <span class=\"badge badge-pill badge-danger\">");
            EndContext();
            BeginContext(875, 36, false);
#line 26 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                                                                       Write(idea.Votes.Count(i => i.Upvote == 0));

#line default
#line hidden
            EndContext();
            BeginContext(911, 113, true);
            WriteLiteral(" dislikes</span>\r\n                            </div>\r\n                        </td>\r\n                        <td>");
            EndContext();
            BeginContext(1025, 16, false);
#line 29 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                       Write(idea.Description);

#line default
#line hidden
            EndContext();
            BeginContext(1041, 129, true);
            WriteLiteral("</td>\r\n                        <td>\r\n                            <button type=\"button\" class=\"btn btn-danger btndelete\" data-id=\"");
            EndContext();
            BeginContext(1171, 7, false);
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                                                                                       Write(idea.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1178, 82, true);
            WriteLiteral("\">Verwijderen</button>\r\n                        </td>\r\n                    </tr>\r\n");
            EndContext();
#line 34 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Ideas.cshtml"
                }

#line default
#line hidden
            BeginContext(1279, 794, true);
            WriteLiteral(@"            </tbody>
        </table>
    </div>

<script>
    var Ideas = {
        init: function () {
            $('#_ideas').addClass('active');

            $('.btndelete').click(function () {
                if (confirm(""Weet u zeker dat u dit idee wilt verwijderen? Dit kan niet ongedaan worden gemaakt!!!"")) {
                    $.get('/Admin/Ideas/Delete/' + $(this).data('id'), function (r) {
                        if (r.status == ""success"")
                            window.location.reload();
                        else
                            $.bootstrapGrowl('Interne serverfout.', { type: 'danger' });
                    },'json');
                }
            });
        }
    }

    $(function () {
        Ideas.init();
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Idea>> Html { get; private set; }
    }
}
#pragma warning restore 1591
