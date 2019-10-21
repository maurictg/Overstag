#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "118774c9344c127bdc06e266cf55e119d91e76ca"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Paying), @"mvc.1.0.view", @"/Views/Admin/Paying.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"118774c9344c127bdc06e266cf55e119d91e76ca", @"/Views/Admin/Paying.cshtml")]
    public class Views_Admin_Paying : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Payment[]>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
  
    ViewBag.Title = "Betalingen";
    Layout = "~/Views/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<br />
<h3><a class=""badge badge-primary"" href=""/Admin/PayingMollie"">Mollie betalingen</a></h3>

<br />
<table class=""table text-white"">
    <thead>
        <tr>
            <th>Id</th>
            <th>InvoiceID</th>
            <th>Status</th>
            <th>UserID</th>
            <th>Geplaatst op</th>
            <th>Mollie ID</th>
            <th>? Betaald op</th>
            <th>Acties</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 24 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
         foreach(var i in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 27 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
               Write(i.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 28 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
               Write(i.InvoiceID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"status\">");
#nullable restore
#line 29 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
                              Write(i.Status.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 30 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
               Write(i.UserID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
               Write(i.PlacedAt);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 32 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
               Write(i.PaymentID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 33 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
               Write(i.PayedAt);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td><a class=\"btn btn-sm bg-secondary text-white\" href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 998, "\"", 1030, 3);
            WriteAttributeValue("", 1008, "Update(\'", 1008, 8, true);
#nullable restore
#line 34 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
WriteAttributeValue("", 1016, i.PaymentID, 1016, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1028, "\')", 1028, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Bijwerken</a></td>\r\n            </tr>\r\n");
#nullable restore
#line 36 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Paying.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>

<script>
    function Update(id) {
        $.get('/Pay/UpdatePayment/' + id, function (r) {
            console.log(r);
            if (r.status == 'success') {
                $.bootstrapGrowl('Gevonden! Pagina ververst nu...', { type: 'success' });
                setTimeout(window.location.reload.bind(window.location), 1000);
            }
            else if (r.status == 'warning') {
                $.bootstrapGrowl(r.warning, { type: 'warning' });
                console.log(r.ps);
            }
            else {
                $.bootstrapGrowl(r.error, { type: 'danger' });
            }
        });
    }

    $(function () {
        $('#_paying').addClass('active');

        $.each($('.status'), function (i, e) {
            switch ($(e).text()) {
                case 'Paid':
                    $(e).addClass('text-success');
                    break;
                case 'Open':
                    $(e).addClass('text-primary');
                 ");
            WriteLiteral(@"   break;
                case 'Failed':
                    $(e).addClass('text-danger');
                    break;
                case 'Expired':
                    $(e).addClass('text-warning');
                    break;
                case 'Pending':
                    $(e).addClass('text-secondary');
                    break;
            }
                
        });
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Payment[]> Html { get; private set; }
    }
}
#pragma warning restore 1591
