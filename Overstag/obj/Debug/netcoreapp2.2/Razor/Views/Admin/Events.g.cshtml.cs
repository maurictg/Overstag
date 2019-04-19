#pragma checksum "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2e567cf61235456741e003219f1e4791c022c893"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Events), @"mvc.1.0.view", @"/Views/Admin/Events.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Events.cshtml", typeof(AspNetCore.Views_Admin_Events))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e567cf61235456741e003219f1e4791c022c893", @"/Views/Admin/Events.cshtml")]
    public class Views_Admin_Events : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Event>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
  
    ViewData["Title"] = "Activiteiten";
    Layout = "/Views/_AdminLayout.cshtml";

#line default
#line hidden
            BeginContext(128, 328, true);
            WriteLiteral(@"
<br />
<div id=""eventsdiv"">
    <h3>Activiteiten</h3>
    <table>
        <thead>
            <tr>
                <th>Titel</th>
                <th>Omschrijving</th>
                <th>Datum</th>
                <th>Tijd</th>
                <th>Kosten</th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 21 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                 foreach (var e in Model)
                {

#line default
#line hidden
            BeginContext(518, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(573, 7, false);
#line 24 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.Title);

#line default
#line hidden
            EndContext();
            BeginContext(580, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(616, 13, false);
#line 25 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.Description);

#line default
#line hidden
            EndContext();
            BeginContext(629, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(665, 6, false);
#line 26 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.Date);

#line default
#line hidden
            EndContext();
            BeginContext(671, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(707, 34, false);
#line 27 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(Math.Round((double)(e.Cost / 100)));

#line default
#line hidden
            EndContext();
            BeginContext(741, 35, true);
            WriteLiteral(";</td>\r\n                    </tr>\r\n");
            EndContext();
#line 29 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                }

#line default
#line hidden
            BeginContext(795, 1993, true);
            WriteLiteral(@"        </tbody>
    </table>
    <h3>Activiteiten toevoegen</h3>
    <form class=""center-align"">
        <div class=""row"">
            <div class=""col m8"">
                <input type=""text"" id=""title"" placeholder=""Titel"" />
                <input type=""text"" id=""description"" placeholder=""Omschrijving"" />
                <input type=""text"" id=""date"" class=""datepicker"" placeholder=""Datum"">
                <input type=""text"" id=""time"" class=""timepicker"" placeholder=""Tijd"">
                <input type=""text"" id=""cost"" placeholder=""Kosten"" />
                <div class=""progress"" id=""progbar""><div class=""indeterminate""></div></div>
                <a class=""waves-effect waves-light btn"" id=""btnadd"">Toevoegen</a>
            </div>
        </div>
    </form>
</div>
<script>
    Events = {
        init: function () {
            $('#_events').addClass('active');
            $('#progbar').hide();
            Events.mapEvents();
        },
        mapEvents: function () {
             $('#bt");
            WriteLiteral(@"nadd').click(function () {
                 Events.post();
            });
        },
        post: function () {
            var data = {
                Title = $('#title').val(),
                Description = $('#description').val(),
                Date = $('#date').val(),
                Time = $('#time').val(),
                Cost = $('#cost').val()
            };
            $('#progbar').show();
            $.post(""/Admin/postEvent"",data , function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Event successvol toegevoegd!!', classes: 'green' });
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                }
                }, 'json'
            ).done(function(){
                $('#progbar').hide();
            });
        }
    }

    Events.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.Event>> Html { get; private set; }
    }
}
#pragma warning restore 1591
