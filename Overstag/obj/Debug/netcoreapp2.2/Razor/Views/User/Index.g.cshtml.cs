#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ff1b7ad46ca2bb0b61dcb656ac3601cf6366af42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index), @"mvc.1.0.view", @"/Views/User/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Index.cshtml", typeof(AspNetCore.Views_User_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff1b7ad46ca2bb0b61dcb656ac3601cf6366af42", @"/Views/User/Index.cshtml")]
    public class Views_User_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
  
    Layout = "/Views/_UserLayout.cshtml";
    ViewBag.Title = "Mijn account";

#line default
#line hidden
            BeginContext(119, 134, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"center-align flow-text col s10 offset-s1\">\r\n        &nbsp;<h4 class=\"blue-middle-text\">Welkom <b>");
            EndContext();
            BeginContext(254, 15, false);
#line 9 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
                                                Write(Model.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(269, 1698, true);
            WriteLiteral(@"</b>!</h4>
        &nbsp;<h5>Navigeer via het <b>menu</b></h5>
        <p>
            Om jezelf in te schrijven voor een activiteit, navigeer je via het menu naar ""<b>Activiteiten</b>"".<br />
            Wanneer je de instellingen van je account wilt bekijken en wijzigen, bijvoorbeeld je wachtwoord veranderen, navigeer je naar ""<b>Instellingen</b>""
        </p>
        <br /><br />
        <h4 class=""blue-middle-text"">Huisregels tijdens activiteiten</h4>
        <p>De 10 geboden zijn ons uitgangspunt tijdens de chill-avonden en als we activiteiten gaan ondernemen.</p>
        <p>We willen met God en elkaar omgaan zoals dat verwoord is in Mattheus 22: &quot;<i>Gij zult de Heere, uw God, liefhebben met heel uw hart, met heel uw ziel en met heel uw verstand. Dit is het grote en eerste gebod. Het tweede, daaraan gelijk, is: Gij zult uw naaste liefhebben als uzelf.</i>&quot;</p>
        <p>In de kist is het niet toegestaan om alcohol te nuttigen, we serveren wel alcoholvrij bier of radler. We zijn ervan");
            WriteLiteral(@" overtuigd dat we het ook zonder alcohol gezellig kunnen hebben met elkaar. Roken mag uitsluitend buiten. Gebruik van wiet of andere drugs is niet toegestaan. Als je toch alcoholische drank drinkt /meeneemt of bepaalde middelen gebruikt krijg je een waarschuwing. Na een tweede keer ben je niet meer welkom in de kist. We vertrouwen er op dat dit uiteraard niet zal plaatsvinden.</p>
        <p>Wel vinden we het fijn als je op een fatsoenlijke manier gekleed bent en met je kleding geen aanstoot geeft.</p>
    </div>
</div>



<script>
    $(function () {
        $('#_ainfo').addClass('active');
        $('#_mainfo').addClass('active');
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Account> Html { get; private set; }
    }
}
#pragma warning restore 1591
