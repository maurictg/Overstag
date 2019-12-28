#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Register\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e4546e4c5bc0946e2cea3e3ca9f118ae74b4e242"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Register_Register), @"mvc.1.0.view", @"/Views/Register/Register.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4546e4c5bc0946e2cea3e3ca9f118ae74b4e242", @"/Views/Register/Register.cshtml")]
    public class Views_Register_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Register\Register.cshtml"
  
    ViewBag.Title = "Account aanmaken bij Overstag";
    ViewBag.Description = "Maak hier jouw Overstag account aan. Met een account kun je je inschrijven voor activiteiten en stemmen op ideeën";
    ViewBag.Keywords = "overstag, overstag rilland, overstag reimerswaal, stichting overstag, overstag account, registreren, account aanmaken";
    ViewBag.Canonical = "https://stoverstag.nl/aanmelden";

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e4546e4c5bc0946e2cea3e3ca9f118ae74b4e2423155", async() => {
                WriteLiteral(@"
        <div class=""row"">
            <div class=""col s12 m8 offset-m2"">
                <h4 class=""blue-light-text center-align""><b id=""rtitle"">Registreren</b></h4><br />
                <div class=""card-panel"" style=""display: block; margin-left: auto; margin-right: auto"">
                    <a href=""/Register/Login""><b>Al een account? Log hier in</b></a>
                    <br /><br />
                    <form class=""login-form"" id=""frmRegister"">
                        <div class=""row"">
                            <div class=""input-field col s12 m6"">
                                <i class=""material-icons prefix"">account_circle</i>
                                <input type=""text"" class=""validate checki"" id=""Firstname"" placeholder=""Voornaam"" />
                                <label for=""Firstname"">Voornaam</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. je voornaam in"" ></span>
                            </div>
                            <div");
                WriteLiteral(@" class=""input-field col s12 m6"">
                                <input type=""text"" class=""validate checki"" id=""Lastname"" placeholder=""Achternaam"" />
                                <label for=""Lastname"">Achternaam</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. je achternaam in""></span>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""input-field col s12 m6"">
                                <i class=""material-icons prefix"">home</i>
                                <input type=""text"" data-regex=""\w+ \w+"" class=""validate checki"" id=""Adress"" placeholder=""Adres + Huisnummer"" />
                                <label for=""Adress"">Adres + Huisnummer</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. een geldig adres in: Straat, spatie, huisnummer""></span>

                            </div>
                            <div cla");
                WriteLiteral(@"ss=""input-field col s12 m2"">
                                <input type=""text"" data-regex=""^[1-9][0-9]{3} ?(?!sa|sd|ss|SA|SD|SS)[A-Za-z]{2}$"" class=""validate checki"" maxlength=""7"" id=""Postalcode"" placeholder=""Postcode"" />
                                <label for=""Postalcode"">Postcode</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. een geldige postcode in""></span>
                            </div>
                            <div class=""input-field col s12 m4"">
                                <input type=""text"" class=""validate checki"" id=""Residence"" placeholder=""Woonplaats"" />
                                <label for=""Residence"">Woonplaats</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. je woonplaats in""></span>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""input-field col s12 m6"">
                             ");
                WriteLiteral(@"   <i class=""material-icons prefix"">account_circle</i>
                                <input type=""text"" data-regex=""^[a-zA-Z0-9_-]{3,20}$"" class=""validate checki"" id=""Username"" placeholder=""Gebruikersnaam"" />
                                <label class=""active"" for=""Username"">Gebruikersnaam</label>
                                <span class=""helper-text"" data-error=""Gebruikersnaam mag geen spaties of speciale tekens bevatten met uitzondering van _ 3-20 tekens.""></span>
                            </div>
                            <div class=""input-field col s12 m3"">
                                <input type=""date"" class=""validate checki"" id=""Birthdate"" placeholder=""Geboortedatum"">
                                <label for=""Birthdate"">Geboortedatum</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. je geboortedatum in""></span>
                            </div>
                            <div class=""input-field col s12 m3"">
                             ");
                WriteLiteral(@"   <p>
                                    Geslacht:&nbsp;
                                    <label>
                                        <input value=""0"" class=""with-gap"" name=""sex"" type=""radio"" checked />
                                        <span>M</span>&nbsp;
                                    </label>
                                    <label>
                                        <input value=""1"" class=""with-gap"" name=""sex"" type=""radio"" />
                                        <span>V</span>
                                    </label>
                                </p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""input-field col s12 m7"">
                                <i class=""material-icons prefix"">email</i>
                                <input type=""text"" data-regex=""^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+");
                WriteLiteral(@"@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"" class=""validate checki"" id=""Email"" placeholder=""Email"" />
                                <label for=""Email"">Email</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. een geldig email adres in"" data-success=""""></span>
                            </div>
                            <div class=""input-field col s12 m5"">
                                <i class=""material-icons prefix"">phone</i>
                                <input type=""text"" maxlength=""12"" id=""Phone"" placeholder=""Telefoonnummer"" />
                                <label for=""Phone"">Telefoonnummer</label>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""input-field col s12 m6"">
                                <i class=""material-icons prefix"">lock</i>
                                <input type=""password"" class=""validate checki"" id=""Password"" placeholder=""Wachtwoo");
                WriteLiteral(@"rd"" />
                                <label for=""Password"">Wachtwoord</label>
                                <span class=""helper-text"" data-error=""Vul a.u.b. een wachtwoord in""></span>
                            </div>
                            <div class=""input-field col s12 m6"">
                                <i class=""material-icons prefix"">lock</i>
                                <input type=""password"" placeholder=""Wachtwoord herhalen"" id=""Passwordrep"" class=""validate checki"" />
                                <label for=""Passwordrep"">Wachtwoord herhalen</label>
                                <span class=""helper-text"" data-error=""Herhaal a.u.b. je wachtwoord""></span>
                            </div>
                            <span id=""pwhelper"" style=""color: red; visibility: hidden;"">Wachtwoorden komen niet overeen</span>
                        </div>
                        <div class=""row"">
                            <p>Type gebruiker: </p>
                            <p>
  ");
                WriteLiteral(@"                              <label>
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

        <div class=""fixed-action-btn"">
            <a class=""btn-floating btn-large blue-light waves-effect"" href=""/Register/Login"">
                <i class=""large material-icons"">arrow_back</i>
            </a>
        </div>
    ");
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
            WriteLiteral(@"
<script>
    Register = {
        init: function () {
            $(""#_reg, #_mreg"").addClass(""active"");
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
            var isValid = true;
            $('.checki').ea");
            WriteLiteral(@"ch(function() {
                var valid = true;
                if ($.trim($(this).val()) == '') {
                    valid = false;
                }

                if ($(this).data('regex') != undefined) {
                    var regex = new RegExp($(this).data('regex'))
                    if (!regex.test($(this).val()))
                        valid = false;
                }

                if (!valid) {
                    $(this).addClass(""invalid"");
                    $(this).prop(""aria-invalid"", ""true"");
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
            $('#register').addClass('disabled');
            var");
            WriteLiteral(@" data = {
                Username: $('#Username').val(),
                Password: $('#Password').val(),
                Email: $('#Email').val(),
                Firstname: $('#Firstname').val(),
                Lastname: $('#Lastname').val(),
                Adress: $('#Adress').val(),
                Postalcode: $('#Postalcode').val(),
                Residence: $('#Residence').val(),
                Sex: $('input[name=sex]:checked').val(),
                Type: $('input[name=type]:checked').val(),
                Birthdate: $('#Birthdate').val(),
                Phone: $('#Phone').val()
            };
            $('#_progbar').show();
            $('#rtitle').text('Registeren...');
            $('.checki').attr('disabled',true);
            $(window).scrollTop(0);
            $.post(""/Register/postRegister"",data , function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Registratie successvol!!', classes: 'green' });
            ");
            WriteLiteral(@"        if ($('#autologin').prop('checked')) {
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
                $('#_progbar').hide();
                $('#register').removeClass('disabled');
                $('#rtitle').text('Registeren');
                $('.checki').removeAttr('disabled');
            });
        },

        postlogin: function () {
            $('#_progbar').show();
            $.post(""/Register/postLogin"", { Username: $(");
            WriteLiteral(@"'#Username').val(), Password: $('#Password').val() }, function (response) {
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
                $('#_progbar').hide();
            });
        }
    }

    $(function () {
        Register.init();
    });
    //init
    
</script>");
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