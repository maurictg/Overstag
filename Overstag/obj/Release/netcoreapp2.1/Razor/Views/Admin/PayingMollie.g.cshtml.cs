#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6d6145520df699d4985db4018bb07ff90c078a75"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_PayingMollie), @"mvc.1.0.view", @"/Views/Admin/PayingMollie.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/PayingMollie.cshtml", typeof(AspNetCore.Views_Admin_PayingMollie))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d6145520df699d4985db4018bb07ff90c078a75", @"/Views/Admin/PayingMollie.cshtml")]
    public class Views_Admin_PayingMollie : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Mollie.Api.Models.Payment.Response.PaymentResponse>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
  
    Layout = "~/Views/_AdminLayout.cshtml";
    ViewBag.Title = "Betalingen (Mollie)";

#line default
#line hidden
            BeginContext(161, 509, true);
            WriteLiteral(@"<br />
    <h3><a class=""badge badge-primary"" href=""/Admin/Paying"">Terug</a></h3>
<br />

<table class=""table text-white"">
    <thead>
        <tr>
            <th>Id</th>
            <th>? Gemaakt op</th>
            <th>Status</th>
            <th>? Betaald op</th>
            <th>? Geannuleerd op</th>
            <th>? Verloopt op</th>
            <th>? Verlopen op</th>
            <th>? Mislukt op</th>
            <th>? Geauthoriseerd op</th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 25 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
         foreach(var pr in Model)
        {

#line default
#line hidden
            BeginContext(716, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(755, 5, false);
#line 28 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.Id);

#line default
#line hidden
            EndContext();
            BeginContext(760, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(788, 12, false);
#line 29 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.CreatedAt);

#line default
#line hidden
            EndContext();
            BeginContext(800, 42, true);
            WriteLiteral("</td>\r\n                <td class=\"status\">");
            EndContext();
            BeginContext(843, 20, false);
#line 30 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
                              Write(pr.Status.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(863, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(891, 9, false);
#line 31 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.PaidAt);

#line default
#line hidden
            EndContext();
            BeginContext(900, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(928, 13, false);
#line 32 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.CanceledAt);

#line default
#line hidden
            EndContext();
            BeginContext(941, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(969, 12, false);
#line 33 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.ExpiresAt);

#line default
#line hidden
            EndContext();
            BeginContext(981, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1009, 12, false);
#line 34 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.ExpiredAt);

#line default
#line hidden
            EndContext();
            BeginContext(1021, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1049, 11, false);
#line 35 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.FailedAt);

#line default
#line hidden
            EndContext();
            BeginContext(1060, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1088, 15, false);
#line 36 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
               Write(pr.AuthorizedAt);

#line default
#line hidden
            EndContext();
            BeginContext(1103, 26, true);
            WriteLiteral("</td>\r\n            </tr>\r\n");
            EndContext();
#line 38 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Admin\PayingMollie.cshtml"
        }

#line default
#line hidden
            BeginContext(1140, 809, true);
            WriteLiteral(@"    </tbody>
</table>

<script>
    $(function () {
        $('#_paying').addClass('active');

        $.each($('.status'), function (i, e) {
            switch ($(e).text()) {
                case 'Paid':
                    $(e).addClass('text-success');
                    break;
                case 'Open':
                    $(e).addClass('text-primary');
                    break;
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Mollie.Api.Models.Payment.Response.PaymentResponse>> Html { get; private set; }
    }
}
#pragma warning restore 1591