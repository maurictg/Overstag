#pragma checksum "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee8902a271e07371ba32eb82805bee1cf3ace83c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Register_Login), @"mvc.1.0.view", @"/Views/Register/Login.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Register/Login.cshtml", typeof(AspNetCore.Views_Register_Login))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee8902a271e07371ba32eb82805bee1cf3ace83c", @"/Views/Register/Login.cshtml")]
    public class Views_Register_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Login>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml"
  
    ViewBag.Title = "Inloggen";

#line default
#line hidden
            BeginContext(70, 1677, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ee8902a271e07371ba32eb82805bee1cf3ace83c2820", async() => {
                BeginContext(76, 1664, true);
                WriteLiteral(@"
    <div class=""row"">
        <div class=""col s12 m6 offset-m3"">
            <h4 class=""blue-light-text center-align""><b>Inloggen</b></h4><br />
            <div class=""card-panel"" style=""display: block; margin-left: auto; margin-right: auto"">
                <form method=""post"" action=""/Register/postLogin"" class=""login-form"" id=""frmlogin"">
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">account_circle</i>
                        <input type=""text"" class=""checki"" id=""Username"" placeholder=""Gebruikersnaam of email"" />
                        <label class=""active"" for=""Username"">Gebruikersnaam of email</label>
                    </div>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""Password"" placeholder=""Wachtwoord"" />
                        <label for=""Password"">Wachtwoord</label>
                    </div>
 ");
                WriteLiteral(@"                   <a>
                        <a href=""/Register/Register"">Geen account? Maak er hier een</a>
                    </a>
                    <div class=""card-action center-align"">
                        <br />
                        <div class=""progress"" id=""progbar""><div class=""indeterminate""></div></div>
                        <a class=""btn blue-light waves-effect"" id=""btnlogin"">Inloggen</a><br />
                        <span id=""tbhelper"" style=""color: red; visibility: hidden;"">Vul alle velden in</span>
                    </div>
                </form>
            </div>
        </div>
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1747, 2138, true);
            WriteLiteral(@"
<script>

    Login = {
        init: function () {
            $(""#_reg"").addClass(""active"");
            $(""#_mreg"").addClass(""active"");
            $('#progbar').hide();
            Login.mapEvents();
        },

        mapEvents: function () {
            $('#Password').keydown(function (e) {
                if (e.which == 13) {
                    Login.login();
                }
            });

            $('#btnlogin').click(function () {
                Login.login();
            });
        },

        login: function () {
            var isValid = true;
            $('.checki').each(function () {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                }
            });

            if (isValid) {
                Login.post();
                $('#tbhelper').css('visibility', 'hidden');
            }
            else {
                $('#tbhelper').css('visibility', 'visible');
            }
        },

      ");
            WriteLiteral(@"  post: function () {
            $('#progbar').show();
            $.post(""/Register/postLogin"", { Username: $('#Username').val(), Password: $('#Password').val() }, function (response) {
                if (response.status == 'success') {
                    Login.redirect();
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                    $('.checki').val('');
                }
                }, 'json'
            ).done(function(){
                $('#progbar').hide();
            });
        },

        redirect: function () {
            //Redirect to user page
            localStorage.setItem(""login"", true);
            if (localStorage.getItem(""redirect"")) {
                $(location).attr('href', localStorage.getItem(""redirect""));
                localStorage.removeItem(""redirect"");
            }
            else {
                $(location).attr('href', '/User');
      ");
            WriteLiteral("      }\r\n        } \r\n\r\n    }\r\n\r\n    //call init function\r\n    Login.init();\r\n</script>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Login> Html { get; private set; }
    }
}
#pragma warning restore 1591
