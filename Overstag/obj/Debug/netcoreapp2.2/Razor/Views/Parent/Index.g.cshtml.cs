#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7a89451a0638438e962c19ffde94266a465b87e6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Parent_Index), @"mvc.1.0.view", @"/Views/Parent/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Parent/Index.cshtml", typeof(AspNetCore.Views_Parent_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a89451a0638438e962c19ffde94266a465b87e6", @"/Views/Parent/Index.cshtml")]
    public class Views_Parent_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Family>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
  
    ViewBag.Title = "Mijn gezin";
    Layout = "~/Views/_UserLayout.cshtml";

#line default
#line hidden
            BeginContext(117, 265, true);
            WriteLiteral(@"
    <div>
        <h3>Mijn gezin</h3>
        <table class=""responsive-table"">
            <thead>
                <tr>
                    <th>Naam</th>
                    <th>Acties</th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 17 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                 foreach (var user in Model.Members)
                {

#line default
#line hidden
            BeginContext(455, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(510, 14, false);
#line 20 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                       Write(user.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(524, 114, true);
            WriteLiteral("</td>\r\n                        <td>\r\n                            <a class=\"waves-effect waves-light btn-small red\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 638, "\"", 675, 3);
            WriteAttributeValue("", 648, "Parent.openRemove(", 648, 18, true);
#line 22 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
WriteAttributeValue("", 666, user.Id, 666, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 674, ")", 674, 1, true);
            EndWriteAttribute();
            BeginContext(676, 76, true);
            WriteLiteral(">Verwijderen</a>\r\n                        </td>\r\n                    </tr>\r\n");
            EndContext();
#line 25 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(771, 16, true);
            WriteLiteral("                ");
            EndContext();
#line 26 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                 if (Model.Members.Count() == 0)
                {

#line default
#line hidden
            BeginContext(840, 144, true);
            WriteLiteral("                    <tr><td colspan=\"2\" class=\"center-align\"><h5><b>Geen gezinsleden</b>.</h5> Klik op de + om iemand uit te nodigen</td></tr>\r\n");
            EndContext();
#line 29 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1003, 1924, true);
            WriteLiteral(@"            </tbody>
        </table>
        <div class=""center-align"">
            <br />
            <a href=""/Parent/Billing"" class=""waves-effect waves-light btn blue-dark"">Betalingen en facturering</a>
        </div>
        <br />
    </div>

<div class=""fixed-action-btn"">
    <a class=""btn-floating btn-large blue-dark"" onclick=""Parent.openLink()"">
        <i class=""large material-icons"">add</i>
    </a>
</div>

<!--Modals-->
<div id=""invite"" class=""modal"">
    <div class=""modal-content"">
        <h4>Uitnodiging</h4>
        <h6>Via deze uitnodiging kan iemand zich voegen bij uw gezin</h6>
        <input id=""familyid"" type=""text"" placeholder=""familyid""/>
        <a class=""btn btn-small"" onclick=""Parent.copyLink()"">Link kopiëren</a>
    </div>
    <div class=""modal-footer"">
        <a href=""#!"" class=""modal-close waves-effect waves-green btn-flat"">Sluiten</a>
    </div>
</div>

<div id=""areusure"" class=""modal"">
    <div class=""modal-content"">
        <h4>Weet u zeker dat u d");
            WriteLiteral(@"eze gebruiker wilt verwijderen?</h4>
        <h6>U kunt hem/haar altijd later weer opnieuw uitnodigen</h6>
    </div>
    <div class=""modal-footer"">
        <a href=""#!"" id=""btndelete"" class=""modal-close waves-effect waves-green btn red"">Ja, verwijderen</a>
        <a href=""#!"" class=""modal-close waves-effect waves-green btn green"">Nee, annuleren</a>
    </div>
</div>

<script>
    var Parent = {
        init: function() {
            $('#_family').addClass('active');
            $('#_mfamily').addClass('active');
            this.mapEvents();
        },
        mapEvents: function() {
            $('#btndelete').click(function() {
                Parent.postRemove($(this).data('id'));
            });
        },
        openLink: function() {
            M.Modal.getInstance($('#invite')).open();
            var link = window.location.origin + '/Register/joinFamily/");
            EndContext();
            BeginContext(2928, 33, false);
#line 83 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Parent\Index.cshtml"
                                                                 Write(Uri.EscapeDataString(Model.Token));

#line default
#line hidden
            EndContext();
            BeginContext(2961, 933, true);
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

    $(document).ready(function() {
        Parent.init();
    });
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Family> Html { get; private set; }
    }
}
#pragma warning restore 1591
