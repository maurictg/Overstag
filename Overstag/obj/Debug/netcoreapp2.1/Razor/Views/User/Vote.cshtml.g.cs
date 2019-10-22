#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Vote.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83ef92e0b1023943c6e073f627d2c73bd7e52db1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Vote), @"mvc.1.0.view", @"/Views/User/Vote.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Vote.cshtml", typeof(AspNetCore.Views_User_Vote))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83ef92e0b1023943c6e073f627d2c73bd7e52db1", @"/Views/User/Vote.cshtml")]
    public class Views_User_Vote : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Vote.cshtml"
  
    ViewBag.Title = "Vote";
    Layout = "~/Views/_UserLayout.cshtml";

#line default
#line hidden
            BeginContext(80, 2693, true);
            WriteLiteral(@"
<h3>Ideeën voor activiteiten</h3>
<div id=""ideas"">
    <p>Laden...</p>
</div>
<table class=""responsive-table"">
    <tr><td colspan=""3"" class=""center-align""><h6 style=""font-weight: bold;"">Idee toevoegen</h6></td></tr>
    <tr>
        <td>
            <input placeholder=""Titel"" id=""tbtitle"" type=""text"" class=""validate"">
        </td>
        <td>
            <input placeholder=""Omschrijving"" id=""tbdesc"" type=""text"" class=""validate"">
        </td>
        <td>
            <a class=""waves-effect waves-light btn blue-dark"" onclick=""Vote.addIdea();"">Toevoegen</a>
        </td>
    </tr>
</table>



<div class=""fixed-action-btn"">
    <a class=""btn-floating btn-large blue-dark waves-effect"" href=""/User/Events"">
        <i class=""large material-icons"">arrow_back</i>
    </a>
</div>

<script>
    var Vote = {
        init: function () {
            $('#_aevent, #_maevent').addClass('active');
            //Load partial
            Vote.load();  
        },
        load: function () ");
            WriteLiteral(@"{
            $.get('/User/Ideas', function (r) {
                $('#ideas').html(r);
            }).done(function () {
                Vote.mapEvents();
            });
        },
        mapEvents: function () {
            $('.likebtn').off().on('click',function() {
                Vote.saveLike($(this).data('id'), 1);
            });
            $('.dislikebtn').off().on('click',function() {
                Vote.saveLike($(this).data('id'), 0);
            });
        },
        addIdea: function () {
            if ($('#tbtitle').val() == '' || $('#tbdesc').val() == '') {
                M.toast({ html: 'Vul a.u.b. alle velden in', classes: 'orange' });
            }
            else {
                $.post('/User/Vote/postIdea', { Title: $('#tbtitle').val(), Description: $('#tbdesc').val() }, function (r) {
                    if (r.status == 'success') {
                        Vote.load();
                        $('#tbtitle').val('');
                        $('#tbdesc').val");
            WriteLiteral(@"('');
                    } else {
                        M.toast({ html: r.error, classes: 'red' });
                    }
                },'json');
            }
        },
        saveLike: function (id, like) {
            $.get('/User/Vote/Like/' + id + '/' + like, function (r) {
                if (r.status == 'success') {
                    Vote.load();
                }else {
                    M.toast({ html: r.error, classes: 'red' });
                    return false;
                }
            }, 'json');
        },
    };

    $(document).ready(function () {
        Vote.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591