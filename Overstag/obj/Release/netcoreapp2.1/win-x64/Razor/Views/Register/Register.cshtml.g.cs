#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "77a76ae5e5cec6ff06ffb279f65e18f6aa58e8bd"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"77a76ae5e5cec6ff06ffb279f65e18f6aa58e8bd", @"/Views/Register/Register.cshtml")]
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
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Register.cshtml"
  
    ViewBag.Title = "Account aanmaken bij Overstag";
    ViewBag.Description = "Maak hier jouw Overstag account aan. Met een account kun je je inschrijven voor activiteiten en stemmen op ideeën";
    ViewBag.Keywords = "overstag, overstag rilland, overstag reimerswaal, stichting overstag, overstag account, registreren, account aanmaken";

#line default
#line hidden
            BeginContext(382, 6424, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "52e827db6be8467982c70a8e67994043", async() => {
                BeginContext(388, 6411, true);
                WriteLiteral(@"
    <div class=""row"">
        <div class=""col s12 m8 offset-m2"">
            <h4 class=""blue-light-text center-align""><b>Registreren</b></h4><br />
            <div class=""card-panel"" style=""display: block; margin-left: auto; margin-right: auto"">
                <a href=""/Register/Login""><b>Al een account? Log hier in</b></a>
                <br /><br />
                <form class=""login-form"" id=""frmRegister"">
                    <div class=""row"">
                        <div class=""input-field col s12 m6"">
                            <i class=""material-icons prefix"">account_circle</i>
                            <input type=""text"" class=""validate checki"" id=""Firstname"" placeholder=""Voornaam"" />
                            <label for=""Firstname"">Voornaam</label>
                        </div>
                        <div class=""input-field col s12 m6"">
                            <input type=""text"" class=""validate checki"" id=""Lastname"" placeholder=""Achternaam"" />
                           ");
                WriteLiteral(@" <label for=""Lastname"">Achternaam</label>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""input-field col s12 m6"">
                            <i class=""material-icons prefix"">home</i>
                            <input type=""text"" class=""validate checki"" id=""Adress"" placeholder=""Adres + Huisnummer"" />
                            <label for=""Adress"">Adres + Huisnummer</label>
                        </div>
                        <div class=""input-field col s12 m2"">
                            <input type=""text"" class=""validate checki"" maxlength=""6"" id=""Postalcode"" placeholder=""Postcode"" />
                            <label for=""Postalcode"">Postcode</label>
                        </div>
                        <div class=""input-field col s12 m4"">
                            <input type=""text"" class=""validate checki"" id=""Residence"" placeholder=""Woonplaats"" />
                            <label for=""Residence"">W");
                WriteLiteral(@"oonplaats</label>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""input-field col s12 m8"">
                            <i class=""material-icons prefix"">account_circle</i>
                            <input type=""text"" class=""validate checki"" id=""Username"" placeholder=""Gebruikersnaam"" />
                            <label class=""active"" for=""Username"">Gebruikersnaam</label>
                        </div>
                        <div class=""input-field col s12 m4"">
                            <p>
                                Geslacht:&nbsp;
                                <label>
                                    <input value=""0"" class=""with-gap"" name=""sex"" type=""radio"" checked />
                                    <span>M</span>&nbsp;
                                </label>
                                <label>
                                    <input value=""1"" class=""with-gap"" name=""sex"" type=""radio");
                WriteLiteral(@""" />
                                    <span>V</span>
                                </label>
                            </p>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""input-field col s12 m9"">
                            <i class=""material-icons prefix"">email</i>
                            <input type=""email"" class=""validate checki"" id=""Email"" placeholder=""Email"" />
                            <label for=""Email"">Email</label>
                            <span class=""helper-text"" data-error=""Vul een geldig email adres in"" data-success=""""></span>
                        </div>
                        <div class=""input-field col s12 m3"">
                            <input type=""date"" class=""checki"" id=""Birthdate"" placeholder=""Geboortedatum"">
                            <label for=""Birthdate"">Geboortedatum</label>
                        </div>
                    </div>
                    <div class=");
                WriteLiteral(@"""row"">
                        <div class=""input-field col s12 m6"">
                            <i class=""material-icons prefix"">lock</i>
                            <input type=""password"" class=""validate checki"" id=""Password"" placeholder=""Wachtwoord"" />
                            <label for=""Password"">Wachtwoord</label>
                        </div>
                        <div class=""input-field col s12 m6"">
                            <i class=""material-icons prefix"">lock</i>
                            <input type=""password"" placeholder=""Wachtwoord herhalen"" id=""Passwordrep"" class=""checki"" />
                            <label for=""Passwordrep"">Wachtwoord herhalen</label>
                        </div>
                        <span id=""pwhelper"" style=""color: red; visibility: hidden;"">Wachtwoorden komen niet overeen</span>
                    </div>
                    <div class=""row"">
                        <p>Type gebruiker: </p>
                        <p>
                          ");
                WriteLiteral(@"  <label>
                                <input value=""0"" class=""with-gap"" name=""type"" type=""radio"" checked />
                                <span>Normale gebruiker</span>&nbsp;
                            </label>
                        </p>
                        <p>
                            <label>
                                <input value=""1"" class=""with-gap"" name=""type"" type=""radio"" />
                                <span>Ouder (gezinsaccount)</span>
                            </label>
                        </p>
                    </div>
                    <div class=""card-action center-align"">
                        <br />
                        <div class=""progress"" id=""progbar""><div class=""indeterminate""></div></div>
                        <p>
                            <label>
                                <input type=""checkbox"" class=""filled-in"" checked=""checked"" id=""autologin"" />
                                <span>Automatisch inloggen</span>
            ");
                WriteLiteral(@"                </label>
                        </p>
                        <a class=""btn btn-large blue-light waves-effect"" id=""register"">Registreren</a><br />
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
            BeginContext(6806, 3987, true);
            WriteLiteral(@"
<script>
    Register = {
        init: function () {
            $(""#_reg, #_mreg"").addClass(""active"");
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

        register: function () {
            var isValid =");
            WriteLiteral(@" true;
            $('.checki').each(function () {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                }
            });

            var passMatch = ($('#Password').val() == $('#Passwordrep').val());

            if (isValid && passMatch) {
                Register.post();
            }
            else {
                M.toast({ html: 'Vul a.u.b. alle velden correct in', classes: 'orange' });
            }
        },

        post: function () {
            var data = {
                Username: $('#Username').val(),
                Password: $('#Password').val(),
                Email: $('#Email').val(),
                Firstname: $('#Firstname').val(),
                Lastname: $('#Lastname').val(),
                Adress: $('#Adress').val(),
                Postalcode: $('#Postalcode').val(),
                Residence: $('#Residence').val(),
                Sex: $('input[name=sex]:checked').val(),
                Type: $('in");
            WriteLiteral(@"put[name=type]:checked').val(),
                Birthdate: $('#Birthdate').val()
            };
            $('#progbar').show();
            $.post(""/Register/postRegister"",data , function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Registratie successvol!!', classes: 'green' });
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
            ).done(function(){");
            WriteLiteral(@"
                $('#progbar').hide();
            });
        },

        postlogin: function () {
            $('#progbar').show();
            $.post(""/Register/postLogin"", { Username: $('#Username').val(), Password: $('#Password').val() }, function (response) {
                if (response.status == 'success') {
                    localStorage.setItem(""login"",true);
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

    $(function () {
        Register.init();
    });
    //init
    
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
