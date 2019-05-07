#pragma checksum "C:\Users\mauri\Desktop\Projects\ASP\maurictsite\Overstag\Overstag\Views\Register\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "82f3319a03f2d46fb47622e88a9b3d9e47e190d7"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82f3319a03f2d46fb47622e88a9b3d9e47e190d7", @"/Views/Register/Login.cshtml")]
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
#line 2 "C:\Users\mauri\Desktop\Projects\ASP\maurictsite\Overstag\Overstag\Views\Register\Login.cshtml"
  
    ViewBag.Title = "Inloggen";

#line default
#line hidden
            BeginContext(70, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(74, 3057, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "82f3319a03f2d46fb47622e88a9b3d9e47e190d72950", async() => {
                BeginContext(80, 3044, true);
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
                            <label for=""Pas");
                WriteLiteral(@"sword"">Wachtwoord</label>
                            <a href=""#memailreset"" class=""modal-trigger"" id=""forgotpass"" \>Wachtwoord vergeten?</a>
                        </div>
                        <b>
                            <a href=""/Register/Register"">Geen account? Maak er hier een</a>
                        </b>
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

        <!-- Modal Structure -->
        <div id=""memailreset"" class=""modal"">
            <div class=""modal-content"">
                <h4>Wachtwoord r");
                WriteLiteral(@"eset aanvragen</h4>
                <p>Voer hieronder het emailadres in dat gekoppeld is aan uw account</p><br />
                <div class=""input-field"">
                    <i class=""material-icons prefix"">email</i>
                    <input type=""email"" class=""validate"" id=""tbrmail"" placeholder=""Emailadres"" />
                    <label for=""tbrmail"">Emailadres</label>
                    <span class=""helper-text"" data-error=""Vul a.u.b. een geldig emailadres in""></span>
                    <p class=""red-text"" id=""mailhelper"">Vul a.u.b. alle velden in</p>
                    <div class=""progress"" id=""progbar2""><div class=""indeterminate""></div></div>
                </div>
            </div>
            <div class=""modal-footer"">
                <a href=""#!"" class=""modal-close waves-effect waves-red btn-flat"">Annuleren</a>
                <a href=""#!"" id=""sendmailreset"" class=""waves-effect waves-green btn-flat"">Verzenden</a>
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
            BeginContext(3131, 3352, true);
            WriteLiteral(@"
<script>

    Login = {
        init: function () {
            $(""#_reg"").addClass(""active"");
            $(""#_mreg"").addClass(""active"");
            $('#progbar').hide();
            $('#progbar2').hide();
            $('#mailhelper').hide();
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

            $('#tbrmail').on('keyup', function () { $('#mailhelper').hide();});

            $('#sendmailreset').click(function () {
                if ($('#tbrmail').val() != '') {
                    $('#mailhelper').hide();
                    Login.sendmail();
                } else {
                    $('#mailhelper').show();
                }
            });
        },

        sendmail: function () {
");
            WriteLiteral(@"            $('#progbar2').show();
            $.post(""/Register/postMailreset"", { Email: $('#tbrmail').val() }, function (response) {
                if (response.status == 'success') {
                    $('#tbrmail').val('')
                    M.toast({ html: 'Mail verzonden. Check je email a.u.b.', classes: 'green' });
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                    $('.checki').val('');
                }
            }, 'json').done(function () {
                $('#progbar2').hide();
                M.Modal.getInstance($('#memailreset')).close();
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
   ");
            WriteLiteral(@"             $('#tbhelper').css('visibility', 'hidden');
            }
            else {
                $('#tbhelper').css('visibility', 'visible');
            }
        },

        post: function () {
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
                $(location).attr('");
            WriteLiteral(@"href', localStorage.getItem(""redirect""));
                localStorage.removeItem(""redirect"");
            }
            else {
                $(location).attr('href', '/User');
            }
        } 

    }

    //call init function
    Login.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Login> Html { get; private set; }
    }
}
#pragma warning restore 1591
