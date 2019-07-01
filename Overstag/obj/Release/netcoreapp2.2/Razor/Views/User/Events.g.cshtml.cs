#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Events.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e0d1d99cdd8ea21d4c5477320d0c6093dcea3287"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Events), @"mvc.1.0.view", @"/Views/User/Events.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Events.cshtml", typeof(AspNetCore.Views_User_Events))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0d1d99cdd8ea21d4c5477320d0c6093dcea3287", @"/Views/User/Events.cshtml")]
    public class Views_User_Events : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Events.cshtml"
  
    Layout = "/Views/_UserLayout.cshtml";
    var context = new Overstag.Models.OverstagContext();
    var model = context.Events.Where(e=>e.When>DateTime.Now).OrderBy(e => e.When).ToArray();

#line default
#line hidden
            BeginContext(202, 53, true);
            WriteLiteral("<div>\r\n    <h3 class=\"center-align\">Agenda</h3>\r\n    ");
            EndContext();
            BeginContext(256, 60, false);
#line 8 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Events.cshtml"
Write(await Html.PartialAsync("~/Views/Home/Events.cshtml", model));

#line default
#line hidden
            EndContext();
            BeginContext(316, 1533, true);
            WriteLiteral(@"
</div>
<div>
    <h3 class=""center-align"">Inschrijvingen</h3>
    <div id=""partial"">

    </div>
</div>
<div id=""uitschrijven"" class=""modal"">
    <div class=""modal-content"">
        <h4>Weet je zeker dat je jezelf wilt afmelden voor deze avond?</h4>
        <p>Natuurlijk kun je je altijd weer opnieuw inschrijven, voor het geval je er spijt van krijgt.</p>
    </div>
    <div class=""modal-footer"">
        <a class=""modal-close waves-effect btn green white-text"">Annuleren</a>
        <a onclick=""User.remove()"" class=""modal-close waves-effect btn red white-text"">Toch uitschrijven</a>
    </div>
</div>
<script>
    let eventtoremove;
    var User = {
        init: function () {
            $('#_aevent').addClass('active');
            $('#_maevent').addClass('active');
            User.loadcontent();
        },
        remove: function() {
            //Uitschrijven

            $.post(""/User/postDeleteEvent"", { EventId: eventtoremove }, function (response) {
                if (res");
            WriteLiteral(@"ponse.status == 'success') {
                    M.toast({ html: 'Successvol uitgeschreven', classes: 'orange' });
                    User.loadcontent();
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                }
            }, 'json');
        },
        loadcontent: function(){
           $('#partial').load('/User/Subscriptions');
        }
    }

    User.init();

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
