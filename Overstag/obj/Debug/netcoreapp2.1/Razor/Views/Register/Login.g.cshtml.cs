#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4057680494ffed421eabbb9a92df6d9b826b309d"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4057680494ffed421eabbb9a92df6d9b826b309d", @"/Views/Register/Login.cshtml")]
    public class Views_Register_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 1 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml"
  
    ViewBag.Title = "Inloggen";

#line default
#line hidden
            BeginContext(40, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(44, 5225, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ac6c6532df7145d9ab6b5277e36a90a4", async() => {
                BeginContext(50, 5212, true);
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
                            <a href=""#memailreset"" class=""modal-trigger"" id=""forgotpass"">Wachtwoord vergeten?</a>
                        </div>
                        <a href=""/Register/Register""><b>Geen account? Maak er hier een</b></a>
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
                <h4>Wachtwoord reset aanvragen</h4>
                <p>Voer hieronder het");
                WriteLiteral(@" emailadres in dat gekoppeld is aan uw account</p><br />
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

        <!-- Modal Structure -->
        <div id=""tfa"" class=""modal"">
            <div");
                WriteLiteral(@" class=""modal-content"">
                <div class=""row"">
                    <div class=""center-align"">
                        <h4>Tweetrapsvertificatie (2FA)</h4>
                        <h5>Open uw 2FA app, selecteer het account en type de code in:</h5>
                        <div class=""row"">
                            <div class=""input-field col m4 s12 offset-m4"">
                                <input type=""number"" style=""font-size: xx-large"" maxlength=""6"" data-length=""6"" id=""2facode"" placeholder=""000000"" />
                            </div>
                        </div>
                        <a href=""#!"" class=""modal-close waves-effect orange btn modal-trigger"" data-target=""tfarestore"" onclick=""Login.cancel2fa();"">Code vergeten</a>
                        <a href=""#!"" class=""modal-close waves-effect red btn"" onclick=""Login.cancel2fa();"">Annuleren</a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Structure -->
        <di");
                WriteLiteral(@"v id=""tfarestore"" class=""modal"">
            <div class=""modal-content"">
                <div class=""row"">
                    <div class=""center-align"">
                        <h4>2FA code herstellen</h4>
                        <h5>Type hier een van uw backupcodes in:</h5>
                        <div class=""row"">
                            <div class=""input-field col s4 offset-s4"">
                                <input type=""text"" placeholder=""Herstelcode"" id=""tfarest"" />
                            </div>
                            <div id=""secretdiv"" style=""display: none;"">
                                <br />
                                <h6>Uw secret is: </h6>
                                <h5 id=""secret""></h5>
                            </div>
                        </div>
                        <a href=""#!"" class=""modal-close waves-effect red btn"">Sluiten</a>
                        <a href=""#!"" class=""waves-effect btn green"" onclick=""Login.restore2fa()"">Verzenden</a>
");
                WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    ");
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
            BeginContext(5269, 81, true);
            WriteLiteral("\r\n<script>\r\n\r\n    let wrong2fa = 0;\r\n    var token = \"\";\r\n    let redirecturl = \'");
            EndContext();
            BeginContext(5351, 5, false);
#line 100 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml"
                  Write(Model);

#line default
#line hidden
            EndContext();
            BeginContext(5356, 6723, true);
            WriteLiteral(@"';

    Login = {
        init: function () {
            $(""#_reg, #_mreg"").addClass(""active"");
            $('#progbar, #progbar2, #mailhelper').hide();
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

            $('#tbrmail').on('keyup', function () { $('#mailhelper').hide(); });

            $('#sendmailreset').click(function () {
                if ($('#tbrmail').val() != '') {
                    $('#mailhelper').hide();
                    Login.sendmail();
                } else {
                    $('#mailhelper').show();
                }
            });

            $('#2facode').on('keyup', function () {
                if ($('#2facode').val().length >= 6)
                    Login.valida");
            WriteLiteral(@"te2fa();
            });

        },

        sendmail: function () {
            $('#progbar2').show();
            $.post(""/Register/postMailreset"", { Email: $('#tbrmail').val() }, function (response) {
                if (response.status == 'success') {
                    $('#tbrmail').val('')
                    M.toast({ html: 'Mail verzonden. Check je email a.u.b. <br><b>Controleer ook je spam map</b>', classes: 'green ' });
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red ' });
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
                    is");
            WriteLiteral(@"Valid = false;
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

        post: function () {
            $('#progbar').show();
            $.post(""/Register/postLogin"", { Username: $('#Username').val(), Password: $('#Password').val() }, function (response) {
                if (response.status == 'success') {
                    if (response.type == 3)
                        localStorage.setItem('redirect', '/Admin');

                    if (response.type == 2)
                        localStorage.setItem('redirect', '/Mentor');

                    if (response.twofactor == 'yes') {
                        Login.open2fa();
                        $('#2facode').focus();
                        token = response.token;
                    }
                    els");
            WriteLiteral(@"e
                        Login.redirect();
                }
                else {
                    //status = error
                    M.toast({ html: response.error, classes: 'red ' });
                    $('.checki').val('');
                    $('#Username').focus();
                }
            }, 'json'
            ).done(function () {
                $('#progbar').hide();
            });
        },

        redirect: function () {
            //Redirect to user page
            localStorage.setItem(""login"", true);
            if (redirecturl != '') {
                $(location).attr('href', redirecturl);
                return;
            }

            if (localStorage.getItem(""redirect"")) {
                $(location).attr('href', localStorage.getItem(""redirect""));
                localStorage.removeItem(""redirect"");
            }
            else {
                $(location).attr('href', '/User');
            }
        },

        open2fa: function () {
  ");
            WriteLiteral(@"          M.Modal.getInstance($('#tfa')).open();
            $('#2facode').focus();
        },

        validate2fa: function() {
            if ($('#2facode').val().length != 6 || isNaN($('#2facode').val())) {
                M.toast({ html: 'Type aub een geldige code in', classes: 'orange' });
                $('#2facode').val('');
            }
            else {
                $.post('/Register/Validate2FA' , { token: token, code: $('#2facode').val() }, function (response) {
                    if (response.status == 'success') {
                        Login.redirect();
                        M.Modal.getInstance($('#tfa')).close();
                        $('#2facode').val('');
                    } else {
                        M.toast({ html: 'Validatie mislukt.', classes: 'red' });
                        $('#2facode').val('');
                        wrong2fa++;
                        if (wrong2fa > 5) {
                            M.toast({ html: 'Te veel pogingen. Probeer he");
            WriteLiteral(@"t later opnieuw', classes: 'orange ' });
                            Login.cancel2fa();
                        }
                    }
                },'json').fail(function () {
                    M.toast({ html: 'Er is iets fout gegaan', classes: 'red' });
                });
            }
        },

        cancel2fa: function () {
            $('#Username').val('');
            $('#Password').val('');
            $('#2facode').val('');
            M.Modal.getInstance($('#tfa')).close();
        },

        restore2fa: function () {
            if ($('#tfarest').val() == '')
                M.toast({ html: 'Vul a.u.b. alle velden in!', classes: 'orange' });
            else {
                $.get('/Register/Restore2FA/' + token + '/' + encodeURIComponent($('#tfarest').val()), function (r) {
                    if (r.status == 'success') {
                        $('#secret').text(unescape(r.secret));
                        $('#secretdiv').show();
                        M.toa");
            WriteLiteral(@"st({ html: 'Code successvol hersteld', classes: 'green' });
                    } else {
                        M.toast({ html: 'Code onjuist', classes: 'red' });
                        $('#tfarest').val('');
                    }
                }, 'json').fail(function () {
                    M.toast({ html: 'Dit is geen geldige code', classes: 'red' });
                });
            }
        }
    }

    //call init function
    Login.init();

    $(document).ready(function () {
        $('input#2facode').characterCounter();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
