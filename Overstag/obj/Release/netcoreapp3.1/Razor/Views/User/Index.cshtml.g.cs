#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c8e6831035e9856eb73995017c3fd62e8fa0e06d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index), @"mvc.1.0.view", @"/Views/User/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8e6831035e9856eb73995017c3fd62e8fa0e06d", @"/Views/User/Index.cshtml")]
    public class Views_User_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
  
    Layout = "_UserLayout";
    ViewBag.Title = "Mijn account";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"center-align flow-text col s10 offset-s1\">\r\n        &nbsp;<h3 class=\"blue-middle-text\">Welkom <b>");
#nullable restore
#line 9 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
                                                Write(Model.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b>!</h3>
        &nbsp;<h5>Navigeer via het <b>menu</b></h5>
        <p>
            Om jezelf in te schrijven voor een activiteit, navigeer je via het menu naar &quot;<a href=""/User/Events""><b>Activiteiten</b></a>&quot;.<br />
            Wanneer je de instellingen van je account wilt bekijken en wijzigen, bijvoorbeeld je wachtwoord veranderen, navigeer je naar &quot;<a href=""/User/Settings""><b>Instellingen</b></a>&quot;
        </p>

        <h4>Snel naar: </h4>

");
#nullable restore
#line 18 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
         if (Model.Type != 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <a class=\"btn btn-large orange darken-4 waves-effect waves-light\" href=\"/User/Events\"><b>Inschrijven</b></a>\r\n            <a class=\"btn btn-large blue-light waves-effect waves-light\" href=\"/User/Vote\"><b>Voten</b></a>\r\n");
#nullable restore
#line 22 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 24 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
         if (Model.Type == 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <a class=\"btn btn-large purple darken-4 waves-effect waves-light\" href=\"/Parent\"><b>Mijn gezin</b></a>\r\n");
#nullable restore
#line 27 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 29 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
         if (Model.Type >= 2)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <a class=\"btn btn-large green darken-4 waves-effect waves-light\" href=\"/Mentor\"><b>Overstag Bestuur</b></a>\r\n");
#nullable restore
#line 32 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 34 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
         if (Model.Type >= 3)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <a class=\"btn btn-large blue darken-4 waves-effect waves-light\" href=\"/Admin\"><b>Overstag Admin</b></a>\r\n");
#nullable restore
#line 37 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\User\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
</div>

<script>
    var User = {
        init: function () {
            $('#_ainfo, #_mainfo').addClass('active');
        },
    }

    $(function () {
        User.init();
    });

    $(document).ready(function(){
        $('.modal').modal();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Account> Html { get; private set; }
    }
}
#pragma warning restore 1591
