#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "69945e69036742eb35a74c9faf8db17b4b68f570"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"69945e69036742eb35a74c9faf8db17b4b68f570", @"/Views/Admin/Events.cshtml")]
    public class Views_Admin_Events : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.Event>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
  
    ViewData["Title"] = "Activiteiten";
    Layout = "/Views/_AdminLayout.cshtml";
    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("nl-NL");

#line default
#line hidden
            BeginContext(223, 1639, true);
            WriteLiteral(@"
<br />
<h3 id=""events"" class=""white-text"">Activiteiten toevoegen/bewerken</h3>
<form class="""">
    <div class=""input-group"">
        <div class=""input-group-append"">
            &nbsp;<input type=""text"" class=""checki form-control"" id=""ttitle"" placeholder=""Titel"" />
            &nbsp;<input type=""text"" class=""checki form-control"" id=""description"" placeholder=""Omschrijving"" />
            &nbsp;<input type=""text"" class=""checki form-control datepicker"" id=""date"" placeholder=""Datum (dd-mm-jjjj)"">
            &nbsp;<input type=""text"" class=""checki form-control timepicker"" id=""time"" placeholder=""Tijd (uu:mm)"">
            &nbsp;<input type=""text"" class=""checki form-control"" id=""cost"" placeholder=""Kosten in eurocent (€)"" />
            &nbsp;
        </div>
        <div class=""input-group-append"">
            <input class=""btn btn-primary"" type=""button"" value=""Toevoegen"" id=""btnadd"">
            <input class=""btn btn-secondary"" type=""button"" value=""Annuleren"" id=""btncancel"" style=""display: none;"">
 ");
            WriteLiteral(@"           <input class=""btn btn-primary"" type=""button"" value=""Opslaan"" id=""btnsave"" style=""display: none;"">
        </div>
    </div>
</form>
<br />

<div id=""eventsdiv"" class=""center-align"">
    <h3 class=""white-text"">Activiteiten</h3>
    <br />
    <table class=""table text-white"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Titel</th>
                <th>Omschrijving</th>
                <th>Datum</th>
                <th>Tijd</th>
                <th>Kosten</th>
                <th>Actie</th>
            </tr>
        </thead>
        <tbody>

");
            EndContext();
#line 46 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
             foreach (var e in Model)
            {
                

#line default
#line hidden
#line 48 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                 if (!Overstag.Core.General.DateIsPassed(e.When))
                {
                    double c = (double)e.Cost;

#line default
#line hidden
            BeginContext(2050, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(2105, 4, false);
#line 52 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2109, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2145, 7, false);
#line 53 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.Title);

#line default
#line hidden
            EndContext();
            BeginContext(2152, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2188, 13, false);
#line 54 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.Description);

#line default
#line hidden
            EndContext();
            BeginContext(2201, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2237, 29, false);
#line 55 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.When.ToString("d", culture));

#line default
#line hidden
            EndContext();
            BeginContext(2266, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2302, 29, false);
#line 56 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                       Write(e.When.ToString("t", culture));

#line default
#line hidden
            EndContext();
            BeginContext(2331, 41, true);
            WriteLiteral("</td>\r\n                        <td>&euro;");
            EndContext();
            BeginContext(2373, 36, false);
#line 57 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                             Write(Math.Round(c / 100, 2).ToString("F"));

#line default
#line hidden
            EndContext();
            BeginContext(2409, 138, true);
            WriteLiteral("</td>\r\n                        <td>\r\n                            <a class=\"text-warning\" data-target=\"#verwijdermodal\" data-toggle=\"modal\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2547, "\"", 2576, 3);
            WriteAttributeValue("", 2557, "eventtoedit", 2557, 11, true);
            WriteAttributeValue(" ", 2568, "=", 2569, 2, true);
#line 59 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
WriteAttributeValue(" ", 2570, e.Id, 2571, 5, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2577, 85, true);
            WriteLiteral(">Delete</a>\r\n                            <br /><a class=\"text-primary\" href=\"#events\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2662, "\"", 2690, 3);
            WriteAttributeValue("", 2672, "Events.edit(", 2672, 12, true);
#line 60 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
WriteAttributeValue("", 2684, e.Id, 2684, 5, false);

#line default
#line hidden
            WriteAttributeValue("", 2689, ")", 2689, 1, true);
            EndWriteAttribute();
            BeginContext(2691, 69, true);
            WriteLiteral(">Edit</a>\r\n                        </td>\r\n                    </tr>\r\n");
            EndContext();
#line 63 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                }

#line default
#line hidden
#line 63 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Admin\Events.cshtml"
                 
            }

#line default
#line hidden
            BeginContext(2794, 5190, true);
            WriteLiteral(@"        </tbody>
    </table>

    <br />
    <h3 class=""white-text"">Ingeschreven deelnemers</h3>
    <br />
    <div id=""participators"" class=""white-text"">
        <!--Render deelnemers hier-->
    </div>

    <div class=""modal fade"" id=""verwijdermodal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""DeleteEvent""
         aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content bg-dark"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"">Event verwijderen?</h5>
                    <button type=""button"" class=""close btn btn-warning"" data-dismiss=""modal"" aria-label=""Annuleren"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    <h4>Weet u zeker dat u deze activiteit wilt verwijderen?</h4>
                    <p>Dit kan niet ongedaan worden gemaakt!</p>
                </div>
   ");
            WriteLiteral(@"             <div class=""modal-footer"">
                    <button class=""btn btn-primary"" data-dismiss=""modal"">Annuleren</button>
                    <button class=""btn btn-danger"" onclick=""Events.delete(eventtoedit)"">Verwijderen</button>
                </div>
            </div>
        </div>
    </div>

</div>
<script>
    let eventtoedit = 0;

    Events = {
        init: function () {
            $('#_events').addClass('active');
            $('#progbar').hide();
            Events.mapEvents();
            $('#participators').load('/Admin/Participators');
        },
        mapEvents: function () {
            $('#btnadd').click(function () {
                Events.post();
            });
            $('#btncancel').click(function () {
                $('#ttitle').val('');
                $('#description').val('');
                $('#date').val('');
                $('#time').val('');
                $('#cost').val('');
                $('#btncancel').hide();
            ");
            WriteLiteral(@"    $('#btnsave').hide();
                $('#btnadd').show();
            });
            $('#btnsave').click(function () {
                var data = {
                    Title: $('#ttitle').val(),
                    Description: $('#description').val(),
                    When: $('#date').val() + ' ' + $('#time').val(),
                    Cost: $('#cost').val(), Id: eventtoedit
                };
                $.post('/Admin/UpdateEvent', data, function (r) {
                    if (r.status == 'success') {
                        $.bootstrapGrowl('Wijzigingen opgeslagen!', { type: 'success' });
                        location.reload();
                    }
                    else {
                        $.bootstrapGrowl(r.error, { type: 'danger' });
                    }

                }, 'json');

            });
        },
        edit: function (id) {
            eventtoedit = id;
            moment.locale('nl');
            $.getJSON('/Admin/getEvent/' + id, func");
            WriteLiteral(@"tion (r) {
                if (r.status == 'success') {
                    var data = r.data;
                    var date = new Date(Date.parse(data.when));
                    date = moment(date);
                    console.log(date);
                    $('#ttitle').val(data.title);
                    $('#description').val(data.description);
                    $('#date').val(date.format('DD-MM-YYYY'));
                    $('#time').val(date.format('HH:mm'));
                    $('#cost').val(data.cost);
                    $('#btncancel').show();
                    $('#btnsave').show();
                    $('#btnadd').hide();
                }
                else
                    $.bootstrapGrowl(r.error, { type: 'danger' })

            });
        },
        post: function () {
            var data = {
                Title: $('#ttitle').val(),
                Description: $('#description').val(),
                When: $('#date').val() + ' ' + $('#time').val(),
      ");
            WriteLiteral(@"          Cost: $('#cost').val()
            };
            $('#progbar').show();
            $.post(""/Admin/postEvent"", data, function (response) {
                if (response.status == 'success') {
                    location.reload();
                    $('.checki').val('');
                }
                else {
                    //status = error
                    $.bootstrapGrowl(response.error, { type: 'danger' })
                }
            }, 'json'
            ).done(function () {
                $('#progbar').hide();
            });
        },
        delete: function (id) {
            //Post delete
            $.post(""/Admin/deleteEvent/"" + id, function (response) {
                if (response.status == 'success') {
                    location.reload();
                }
                else {
                    //status = error
                    $.bootstrapGrowl(response.error, { type: 'danger' })
                }
            }, 'json').done(function ()");
            WriteLiteral(" {\r\n            });\r\n        }\r\n    }\r\n\r\n    Events.init();\r\n</script>");
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