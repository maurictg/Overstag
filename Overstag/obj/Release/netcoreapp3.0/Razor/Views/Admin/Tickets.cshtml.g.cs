#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36920fef0651287123a6b6e0e8bded728b49d1da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Tickets), @"mvc.1.0.view", @"/Views/Admin/Tickets.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36920fef0651287123a6b6e0e8bded728b49d1da", @"/Views/Admin/Tickets.cshtml")]
    public class Views_Admin_Tickets : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Ticket>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
  
    Layout = "~/Views/_AdminLayout.cshtml";
    ViewBag.Title = "Tickets";
    string[] type = { "Lid", "Ouder", "Bestuurder", "Administrator" };

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<br />
<h3>Ticketoverzicht</h3>
<br />
<table class=""table text-white"">
    <thead>
        <tr>
            <th>Id</th>
            <th>Geplaatst op</th>
            <th>Titel</th>
            <th>Omschrijving</th>
            <th>Doelgroep</th>
            <th>Blokkeren</th>
            <th>Acties</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 23 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
         foreach (var t in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 26 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
           Write(t.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 27 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
           Write(t.Timestamp.ToString("dd-MM-yyyy HH:mm:ss"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td><b>");
#nullable restore
#line 28 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
              Write(t.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n            <td>");
#nullable restore
#line 29 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
           Write(t.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td><b>");
#nullable restore
#line 30 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
              Write(type[t.Type]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n            <td><button");
            BeginWriteAttribute("onclick", " onclick=\"", 846, "\"", 880, 3);
            WriteAttributeValue("", 856, "blockSender(\'", 856, 13, true);
#nullable restore
#line 31 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
WriteAttributeValue("", 869, t.UserID, 869, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 878, "\')", 878, 2, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning\">(de)blokkeer afzender</button></td>\r\n            <td><button");
            BeginWriteAttribute("onclick", " onclick=\"", 966, "\"", 997, 3);
            WriteAttributeValue("", 976, "deleteTicket(\'", 976, 14, true);
#nullable restore
#line 32 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
WriteAttributeValue("", 990, t.Id, 990, 5, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 995, "\')", 995, 2, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\">Verwijderen</button></td>\r\n        </tr>\r\n");
#nullable restore
#line 34 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\Tickets.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>


<script>
    $(function () {
        $('#_tickets').addClass('active');
    });

    function deleteTicket(id) {
        if (confirm(""Weet je zeker dat je dit ticket wilt verwijderen?"")) {
            $.post('/Admin/deleteTicket/' + id, function (r) {
                if (r.status == 'success') {
                    window.location.reload();
                } else {
                    $.bootstrapGrowl('Verwijderen mislukt', { type: 'danger' });
                }
            }, 'json');
        }
    }

    function blockSender(id) {
        if (confirm(""Zeker weten?"")) {
            var block = 0;
            if (confirm(""Klik op OK of YES om deze persoon te blokkeren, klik op NO of CANCEL om te deblokkeren"")) {
                block = 1;
            }
            $.post('/Admin/blockTicketSender/' + id + '/' + block, function (r) {
                if (r.status == 'success') {
                    if (block == 0)
                        $.bootstrapGrowl('Pe");
            WriteLiteral(@"rsoon gedeblokkeerd', { type: 'success' });
                    else
                        $.bootstrapGrowl('Persoon geblokkeerd', { type: 'success' });
                } else {
                    $.bootstrapGrowl('Updaten mislukt', { type: 'danger' });
                }
            }, 'json');
        }
    }
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Ticket>> Html { get; private set; }
    }
}
#pragma warning restore 1591
