#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Vote.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf94193754385fda6e38be07b2f1fb1eab7222a2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Vote), @"mvc.1.0.view", @"/Views/User/Vote.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf94193754385fda6e38be07b2f1fb1eab7222a2", @"/Views/User/Vote.cshtml")]
    public class Views_User_Vote : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Vote.cshtml"
  
    ViewBag.Title = "Vote";
    Layout = "_UserLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h2 class=""center-align blue-middle-text"">Ideeën voor activiteiten</h2>
<div class=""row"">
    <div class=""col m1 offset-m11 s2 offset-s10"">
        <button class=""btn transparent waves-effect"" onclick=""Vote.load();"">
            <i class=""material-icons blue-light-text"">refresh</i>
        </button>
    </div>
</div>
<div class=""row"" id=""ideas"">
    <h3 class=""center-align"">Laden...</h3>
</div>

<h3 class=""blue-middle-text center-align"">Idee toevoegen</h3>

<div class=""row"">
    <div class=""col s12 m3"">
        <div class=""input-field"">
            <input placeholder=""Titel"" id=""tbtitle"" type=""text"" class=""validate"">
            <span class=""helper-text blue-light-text"">Titel</span>
        </div>
    </div>
    <div class=""col s12 m6"">
        <div class=""input-field"">
            <input placeholder=""Omschrijving"" id=""tbdesc"" type=""text"" class=""validate"">
            <span class=""helper-text blue-light-text"">Omschrijving</span>
        </div>
    </div>
    <div class=""col s12 m3");
            WriteLiteral(@""">
        <div class=""input-field"">
            <i class=""material-icons prefix"">euro</i>
            <input data-regex=""[0-9]+(\,[0-9][0-9])"" placeholder=""10,00"" class=""validate"" id=""tbcost"" type=""text"">
            <span id=""spancost"" class=""helper-text blue-light-text"" data-error=""Vul een geldig bedrag in op deze manier: €10,00 Dus met 2 decimalen"">Geschatte kosten in € p.p. Als je het niet weet laat dan leeg</span>
        </div>
    </div>
    <div class=""col s12 center-align"">
        <button class=""waves-effect waves-light btn blue-dark"" id=""btnadd"" onclick=""Vote.addIdea();"">Toevoegen</button>
    </div>
    <br />
</div>

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
");
            WriteLiteral(@"        load: function () {
            $('#_progbar').show();
            $.get('/User/Ideas', function (r) {
                $('#ideas').html(r);
            }).done(function () {
                $('#_progbar').hide();
                Vote.mapEvents();
            });
        },
        mapEvents: function () {
            $('.likebtn').off().on('click', function () {
                Vote.saveLike($(this).data('id'), 1);
            });
            $('.dislikebtn').off().on('click', function () {
                Vote.saveLike($(this).data('id'), 0);
            });
        },
        addIdea: function () {
            if ($('#tbtitle').val() == '' || $('#tbdesc').val() == '') {
                M.toast({ html: 'Vul a.u.b. alle velden in', classes: 'orange' });
                return;
            }

            var regex = new RegExp($('#tbcost').data('regex'));
            if (!regex.test($('#tbcost').val()) && !($('#tbcost').val() == '' || $('#tbcost').val() == '€')) {
             ");
            WriteLiteral(@"   $('#tbcost').addClass(""invalid"");
                $('#tbcost').prop(""aria-invalid"", ""true"");
                $('#spancost').text('');
                return;
            } else {
                $('#spancost').text('Geschatte kosten in € p.p. Als je het niet weet laat dan leeg');
            }

            $('#btnadd').addClass('disabled');
            $('#_progbar').show();
            if ($('#tbcost').val() != '')
                $('#tbcost').val('€' + $('#tbcost').val());

            $.post('/User/Vote/postIdea', { Title: $('#tbtitle').val(), Description: $('#tbdesc').val(), Cost: $('#tbcost').val() }, function (r) {
                if (r.status == 'success') {
                    setTimeout(Vote.load, 300);
                    M.toast({ html: 'Idee toegevoegd!', classes: 'green' })
                    $('#tbtitle').val('');
                    $('#tbdesc').val('');
                    $('#tbcost').val('');
                    $('#_progbar').hide();
                } else {
      ");
            WriteLiteral(@"              M.toast({ html: r.error, classes: 'red' });
                }
            }, 'json').done(function () {
                $('#btnadd').removeClass('disabled');
            });
        },
        saveLike: function (id, like) {
            $.get('/User/Vote/Like/' + id + '/' + like, function (r) {
                if (r.status == 'success') {
                    if (like == 1) {
                        $('#like-' + id).removeClass('grey').addClass('green');
                        $('#dislike-' + id).removeClass('red').addClass('grey');
                    } else {
                        $('#dislike-' + id).removeClass('grey').addClass('red');
                        $('#like-' + id).removeClass('green').addClass('grey');
                    }
                } else {
                    M.toast({ html: r.error, classes: 'red' });
                    return false;
                }
            }, 'json');
        },
    };

    $(document).ready(function () {
        Vote.i");
            WriteLiteral("nit();\r\n    });\r\n</script>");
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
