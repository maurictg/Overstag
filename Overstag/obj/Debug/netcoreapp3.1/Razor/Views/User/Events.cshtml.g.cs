#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Events.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e0da2a06f12bc3c7fb0e56b3f1c7ea29fdd4e6db"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Events), @"mvc.1.0.view", @"/Views/User/Events.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0da2a06f12bc3c7fb0e56b3f1c7ea29fdd4e6db", @"/Views/User/Events.cshtml")]
    public class Views_User_Events : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Events.cshtml"
  
    Layout = "_UserLayout";
    ViewBag.Title = "Evenementen";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div>
        <h3 class=""center-align"">Agenda &amp; inschrijvingen</h3>
        <h5 class=""blue-middle-text center-align"">&nbsp;Komende evenementen, onder het voorbehoud van <a href=""https://herzienestatenvertaling.nl/teksten/jakobus/4#15"" target=""_blank"">Jakobus 4:15</a></h5>
        <div id=""partial""></div>
    </div>


<br />
<div class=""row"">
    <div class=""col s10 offset-s1"">
        <h4 class=""blue-middle-text center-align"">Voten</h4>
        <p class=""flow-text"">
            Wat zijn leuke activiteiten om te doen? Daar hebben jullie ook een stem in.<br />
            Via <a href=""/User/Vote"">deze link</a> of het knopje met het stemicoontje kan je zelf ook ideeën toevoegen. Breng anderen op de hoogte en vraag hen hun stem uit te brengen.
        </p>
        <br />
        <h4 class=""blue-middle-text center-align"">Huisregels</h4>
        <p class=""flow-text"">Tijdens onze activiteiten gelden bepaalde huisregels. Deze kun je <a href=""/Home/About#huisregels"">hier</a> vinden.</p>
    ");
            WriteLiteral(@"</div>
</div>


<div class=""fixed-action-btn"">
    <a class=""btn-floating btn-large blue-dark waves-effect"" href=""/User/Vote"">
        <i class=""large material-icons"">how_to_vote</i>
    </a>
</div>

<div id=""uitschrijven"" class=""modal"">
    <div class=""modal-content"">
        <h4>Weet je zeker dat je jezelf wilt afmelden voor deze avond?</h4>
        <p>Natuurlijk kun je je altijd weer opnieuw inschrijven, voor het geval je er spijt van krijgt.</p>
    </div>
    <div class=""modal-footer"">
        <a class=""modal-close waves-effect btn green white-text"">Annuleren</a>
        <a onclick=""Events.Unsubscribe();"" id=""btnunsub"" class=""modal-close waves-effect btn red white-text"">Uitschrijven</a>
    </div>
</div>

<div id=""ssetting"" class=""modal"">
    <div class=""modal-content"">
        <h4>Inschrijving bewerken</h4>
        <table>
            <tr>
                <td>
                    <h5>Vriend(inn)en meenemen</h5>
                    <p>Hier kun je instellen of je iemand mee wil");
            WriteLiteral(@"t nemen naar deze activiteit. De gemaakte kosten worden dan voor jou in rekening gebracht.</p>
                </td>
                <td>
                    <h5><span id=""fcount"">0</span> </h5>
                </td>
                <td>
                    <h6>
                        <a onclick=""Events.AddFriend(true);"" class=""btn btn-flat green-text transparent waves-effect""><i class=""material-icons"">add_circle</i></a>
                        <a onclick=""Events.AddFriend(false);"" class=""btn btn-flat transparent red-text waves-effect""><i class=""material-icons"">remove_circle</i></a>
                    </h6>
                </td>
            </tr>
        </table>
    </div>
    <div class=""modal-footer"">
        <a class=""waves-effect btn green white-text"" onclick=""Events.SaveSetting();"">Opslaan</a>
        <a class=""modal-close waves-effect btn grey white-text"">Sluiten</a>
    </div>
</div>


<script>
    let eventtoremove;
    var User = {
        init: function () {
            $");
            WriteLiteral(@"('#_aevent, #_maevent').addClass('active');
            User.loadcontent();
        },
        loadcontent: function () {
            $('#partial').html('<h3>&nbsp;Laden...</h3>');
            $('#partial').load('/User/Subscriptions');
        }
    }

    var Events = {
        MapEvents: function () {
            $('.btnsetting').off().on('click', function () {
                Events.OpenSetting(this);
            })
        },
        DoUnsubscribe: function (id) {
            $('#btnunsub').data('id', id);
            M.Modal.getInstance($('#uitschrijven')).open();
        },
        Subscribe: function (id) {
            $('#_progbar').show();
            //Inschrijven
            $.post(""/User/postSubscribeEvent"", { EventId: id }, function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Inschrijving gelukt! Leuk dat je komt!', classes: 'green' });
                    User.loadcontent();
                }
                els");
            WriteLiteral(@"e {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                }
            }, 'json').done(function () {
                $('#_progbar').hide();
            });
        },
        Unsubscribe: function (id) {
            if (!(id > 0))
                id = $('#btnunsub').data('id');
            //Uitschrijven
            $('#_progbar').show();
            $.post(""/User/postDeleteEvent"", { EventId: id }, function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Successvol uitgeschreven', classes: 'orange' });
                    User.loadcontent();
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                }
            }, 'json').done(function () {
                $('#_progbar').hide();
            });
        },
        OpenSetting: function(sender) {
         ");
            WriteLiteral(@"   $('#ssetting').data('sender', sender);
            M.Modal.getInstance($('#ssetting')).open();
            $('#fcount').text($(sender).data('fc'));
        },
        AddFriend: function (add) {
            var nv = parseInt($('#fcount').text()) + ((add) ? 1 : -1);
            if (nv >= 0 && nv <= 5) {
                $('#fcount').text(nv);
            }
        },
        SaveSetting: function () {
            var sender = $('#ssetting').data('sender');
            $(sender).data('fc',$('#fcount').text());
            $.post('/User/SubscribeFriends', { id: $(sender).data('id'), amount: $(sender).data('fc') }, function (r) {
                if (r.status == 'success') {
                    M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                    M.Modal.getInstance($('#ssetting')).close();
                } else {
                    M.toast({ html: 'Opslaan mislukt', classes: 'red' });
                }
            });
        },
        GetSubs: function () ");
            WriteLiteral(@"{
            $.getJSON('/User/getSubscribers/' + $($('#ssetting').data('sender')).data('id'), function (r) {
                console.log(r);
            });            
        }
    }

    $(document).ready(function () {
        $('.modal').modal();
        User.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
