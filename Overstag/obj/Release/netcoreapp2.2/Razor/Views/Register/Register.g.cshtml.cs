#pragma checksum "C:\Users\mauri\Desktop\Projects\ASP\maurictsite\Overstag\Overstag\Views\Register\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48c5ab4de140e1b7b202a70c768a85e622391296"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Register_Register), @"mvc.1.0.view", @"/Views/Register/Register.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Register/Register.cshtml", typeof(AspNetCore.Views_Register_Register))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48c5ab4de140e1b7b202a70c768a85e622391296", @"/Views/Register/Register.cshtml")]
    public class Views_Register_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
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
#line 2 "C:\Users\mauri\Desktop\Projects\ASP\maurictsite\Overstag\Overstag\Views\Register\Register.cshtml"
  
    ViewBag.Title = "Registreren";

#line default
#line hidden
            BeginContext(75, 3564, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "48c5ab4de140e1b7b202a70c768a85e6223912962877", async() => {
                BeginContext(81, 3551, true);
                WriteLiteral(@"
    <div class=""row"">
        <div class=""col s12 m6 offset-m3"">
            <h4 class=""blue-light-text center-align""><b>Registreren</b></h4><br />
            <div class=""card-panel"" style=""display: block; margin-left: auto; margin-right: auto"">
                <form class=""login-form"" id=""frmRegister"">
                    <div class=""row"">
                        <div class=""input-field col s12 m6"">
                            <i class=""material-icons prefix"">account_circle</i>
                            <input type=""text"" class=""validate checki"" id=""Firstname"" placeholder=""Voornaam"" />
                            <label for=""Firstname"">Voornaam</label>
                        </div>
                        <div class=""input-field col s12 m6"">
                            <input type=""text"" class=""validate checki"" id=""Lastname"" placeholder=""Achternaam"" />
                            <label for=""Lastname"">Achternaam</label>
                        </div>
                    </div>
         ");
                WriteLiteral(@"           <div class=""input-field"">
                        <i class=""material-icons prefix"">account_circle</i>
                        <input type=""text"" class=""validate checki"" id=""Username"" placeholder=""Gebruikersnaam"" />
                        <label class=""active"" for=""Username"">Gebruikersnaam</label>
                    </div>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">email</i>
                        <input type=""email"" class=""validate checki"" id=""Email"" placeholder=""Email"" />
                        <label for=""Email"">Email</label>
                        <span class=""helper-text"" data-error=""Vul een geldig email adres in"" data-success=""""></span>
                    </div>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""validate checki"" id=""Password"" placeholder=""Wachtwoord"" />
                        <label f");
                WriteLiteral(@"or=""Password"">Wachtwoord</label>
                    </div>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" placeholder=""Wachtwoord herhalen"" id=""Passwordrep"" class=""checki"" />
                        <label for=""Passwordrep"">Wachtwoord herhalen</label>
                        <span id=""pwhelper"" style=""color: red; visibility: hidden;"">Wachtwoorden komen niet overeen</span>
                    </div>
                    <a>
                        <a href=""/Register/Login"">Al een account? Log hier in</a>
                    </a>
                    <div class=""card-action center-align"">
                        <br />
                        <div class=""progress"" id=""progbar""><div class=""indeterminate""></div></div>
                        <p>
                            <label>
                                <input type=""checkbox"" class=""filled-in"" checked=""checked"" id=""autologin""/>
");
                WriteLiteral(@"                                <span>Automatisch inloggen</span>
                            </label>
                        </p>
                        <a class=""btn btn-large blue-light waves-effect"" id=""register"">Registreren</a><br />
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
            BeginContext(3639, 3602, true);
            WriteLiteral(@"
<script>
    Register = {
        init: function () {
            $(""#_reg"").addClass(""active"");
            $(""#_mreg"").addClass(""active"");
            $('#progbar').hide();
            Register.mapEvents();
        },

        mapEvents: function () {

            //Detect & check if passwords matches
            $('#Password').on('keyup',function () {
                if ($('#Password').val() != $('#Passwordrep').val()) {
                    $('#pwhelper').css('visibility', 'visible');
                } else { $('#pwhelper').css('visibility', 'hidden'); }
            });

            $('#Passwordrep').on('keyup',function () {
                if ($('#Password').val() != $('#Passwordrep').val()) {
                    $('#pwhelper').css('visibility', 'visible');
                } else { $('#pwhelper').css('visibility', 'hidden'); }
            });

            $('#register').click(function () {
                Register.register();
            });
        },

        register: fun");
            WriteLiteral(@"ction () {
            var isValid = true;
            $('.checki').each(function () {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                }
            });

            if (isValid) {
                Register.post();
                $('#tbhelper').css('visibility', 'hidden');
            }
            else {
                $('#tbhelper').css('visibility', 'visible');
            }
        },

        post: function () {
            var data = {
                Username: $('#Username').val(),
                Password: $('#Password').val(),
                Email: $('#Email').val(),
                Firstname: $('#Firstname').val(),
                Lastname: $('#Lastname').val()
            };
            $('#progbar').show();
            $.post(""/Register/postRegister"",data , function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Registratie successvol!!', classes: 'green' });");
            WriteLiteral(@"
                    if ($('#autologin').prop('checked')) {
                        Register.postlogin();
                    } else {
                        $('.checki').val('');
                    }
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red' });
                    if (response.code == 0) {
                        $('#Username').val('');
                    }
                    else if (response.code == 1) {
                        $('#Email').val('');
                    }
                }
                }, 'json'
            ).done(function(){
                $('#progbar').hide();
            });
        },

        postlogin: function () {
            $('#progbar').show();
            $.post(""/Register/postLogin"", { Username: $('#Username').val(), Password: $('#Password').val() }, function (response) {
                if (response.status == 'success') {
                   ");
            WriteLiteral(@" localStorage.setItem(""login"",true);
                    $(location).attr('href', '/User');
                    $('.checki').val('');
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
        }
    }

    //init
    Register.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Account> Html { get; private set; }
    }
}
#pragma warning restore 1591