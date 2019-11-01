#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "846af61fc8487723c69b8d65b458ff0cf2f3dc73"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Parent_Index), @"mvc.1.0.view", @"/Views/Parent/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"846af61fc8487723c69b8d65b458ff0cf2f3dc73", @"/Views/Parent/Index.cshtml")]
    public class Views_Parent_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Family>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
  
    ViewBag.Title = "Mijn gezin";
    Layout = "_UserLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    <h3>Mijn gezin</h3>\r\n    <table class=\"responsive-table\">\r\n        <thead>\r\n            <tr>\r\n                <th>Naam</th>\r\n                <th>Acties</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 17 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
             foreach (var user in Model.Members)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 20 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                   Write(user.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n                        <a class=\"waves-effect waves-light btn-small red\"");
            BeginWriteAttribute("onclick", " onclick=\"", 559, "\"", 596, 3);
            WriteAttributeValue("", 569, "Parent.openRemove(", 569, 18, true);
#nullable restore
#line 22 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
WriteAttributeValue("", 587, user.Id, 587, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 595, ")", 595, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Verwijderen</a>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 25 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
             if (Model.Members.Count() == 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr><td colspan=\"2\" class=\"center-align\"><h5><b>Geen gezinsleden</b>.</h5> Klik op de + om iemand uit te nodigen</td></tr>\r\n");
#nullable restore
#line 29 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>
    <div class=""center-align"">
        <br />
        <a href=""/Parent/Billing"" class=""waves-effect waves-light btn blue-dark"">Betalingen en facturering</a>
    </div>
    <br />
</div>

<div class=""tap-target blue-dark"" id=""tt"" data-target=""fab"">
    <div class=""tap-target-content"">
        <h5>Gezinsleden toevoegen</h5>
        <p>Klik hier om gezinsleden toe te voegen</p>
    </div>
</div>

<div class=""fixed-action-btn"">
    <a id=""fab"" class=""btn-floating btn-large blue-dark waves-effect"" onclick=""Parent.openLink()"">
        <i class=""large material-icons"">add</i>
    </a>
</div>

<!--Modals-->
<div id=""invite"" class=""modal"">
    <div class=""modal-content"">
        <h4>Uitnodiging</h4>
        <h6>Via deze uitnodiging kan iemand zich voegen bij uw gezin</h6>
        <input id=""familyid"" type=""text"" placeholder=""familyid"" />
        <a class=""btn btn-small"" onclick=""Parent.copyLink()"">Link kopiëren</a>
    </div>
    <div class=""modal-footer"">
  ");
            WriteLiteral(@"      <a href=""#!"" class=""modal-close waves-effect waves-green btn-flat"">Sluiten</a>
    </div>
</div>

<div id=""areusure"" class=""modal"">
    <div class=""modal-content"">
        <h4>Weet u zeker dat u deze gebruiker wilt verwijderen?</h4>
        <h6>U kunt hem/haar altijd later weer opnieuw uitnodigen</h6>
    </div>
    <div class=""modal-footer"">
        <a href=""#!"" id=""btndelete"" class=""modal-close waves-effect waves-green btn red"">Ja, verwijderen</a>
        <a href=""#!"" class=""modal-close waves-effect waves-green btn green"">Nee, annuleren</a>
    </div>
</div>

<script>
    var familyCount = ");
#nullable restore
#line 77 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                 Write(Model.Members.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
    var Parent = {
        init: function() {
            $('#_family, #_mfamily').addClass('active');
            this.mapEvents();
            if (familyCount == 0) {
                M.TapTarget.getInstance($('#tt')).open();
            }
        },
        mapEvents: function() {
            $('#btndelete').click(function() {
                Parent.postRemove($(this).data('id'));
            });
        },
        openLink: function() {
            M.Modal.getInstance($('#invite')).open();
            var link = window.location.origin + '/Register/joinFamily/");
#nullable restore
#line 93 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                                                                 Write(Uri.EscapeDataString(Model.Token));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
            $('#familyid').val(link);
        },
        copyLink: function() {
            $('#familyid').select();
            document.execCommand(""copy"");
            M.toast({ html: 'Link gekopieërd!', classes: 'blue' });
        },
        postRemove: function(id) {
            $.get('/Parent/Remove/' + id, function(r) {
                if(r.status == 'success') {
                    M.toast({ html: 'Gebruiker verwijderd', classes: 'green' });
                    setTimeout(window.location.reload.bind(window.location), 500);
                }
                else
                    M.toast({ html: r.error, classes: 'red' });
            },'json');
        },
        openRemove: function(id) {
            $('#btndelete').data('id', id);
            M.Modal.getInstance($('#areusure')).open();
        }
    }

    $(document).ready(function () {
        $('.tap-target').tapTarget();
        Parent.init();
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Family> Html { get; private set; }
    }
}
#pragma warning restore 1591
