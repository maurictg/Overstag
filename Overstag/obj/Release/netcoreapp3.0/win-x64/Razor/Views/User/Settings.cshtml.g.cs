#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "292d693d9abcc07534b19b9b85bdf57621cfab17"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Settings), @"mvc.1.0.view", @"/Views/User/Settings.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"292d693d9abcc07534b19b9b85bdf57621cfab17", @"/Views/User/Settings.cshtml")]
    public class Views_User_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/moment.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
  
    Layout = "/Views/_UserLayout.cshtml";
    ViewBag.Title = "Instellingen";
    string[] type = { "Lid", "Ouder", "Bestuurder", "Administrator" };

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div>
    <h3 class=""center-align"">Accountinstellingen</h3><br />
    <div>
        <ul class=""collapsible"">
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">info</i>Persoonlijke informatie</div>
                <div class=""collapsible-body col s12 m6"">
                    <h5 class=""center-align"">Persoonlijke informatie</h5>
                    <div class=""row"">
                        <div class=""input-field col s12 m3"">
                            <i class=""material-icons prefix"">account_circle</i>
                            <input type=""text"" class=""checkj"" id=""firstname"" placeholder=""Voornaam""");
            BeginWriteAttribute("value", " value=\"", 850, "\"", 874, 1);
#nullable restore
#line 18 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 858, Model.Firstname, 858, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""firstname"">Voornaam</label>
                        </div>
                        <div class=""input-field col s12 m3"">
                            <input type=""text"" class=""checkj"" id=""lastname"" placeholder=""Achternaam""");
            BeginWriteAttribute("value", " value=\"", 1143, "\"", 1166, 1);
#nullable restore
#line 22 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1151, Model.Lastname, 1151, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""lastname"">Achternaam</label>
                        </div>
                        <div class=""input-field col s12 m4"">
                            <i class=""material-icons prefix"">email</i>
                            <input type=""text"" class=""checkj"" id=""email"" placeholder=""Emailadres""");
            BeginWriteAttribute("value", " value=\"", 1505, "\"", 1525, 1);
#nullable restore
#line 27 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1513, Model.Email, 1513, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""email"">Emailadres</label>
                        </div>
                        <div class=""input-field col s12 m2"">
                            <input type=""text"" id=""phone"" maxlength=""12"" placeholder=""Telefoonnummer""");
            BeginWriteAttribute("value", " value=\"", 1793, "\"", 1813, 1);
#nullable restore
#line 31 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1801, Model.Phone, 1801, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""phone"">Telefoonnummer</label>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""input-field col s12 m4"">
                            <i class=""material-icons prefix"">home</i>
                            <input type=""text"" class=""checkj"" id=""adress"" placeholder=""Adres + Huisnummer""");
            BeginWriteAttribute("value", " value=\"", 2228, "\"", 2249, 1);
#nullable restore
#line 38 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2236, Model.Adress, 2236, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""adress"">Adres + Huisnummer</label>
                        </div>
                        <div class=""input-field col s4 m2"">
                            <input type=""text"" class=""checkj"" maxlength=""6"" id=""postalcode"" placeholder=""Postcode""");
            BeginWriteAttribute("value", " value=\"", 2538, "\"", 2563, 1);
#nullable restore
#line 42 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2546, Model.Postalcode, 2546, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""postalcode"">Postcode</label>
                        </div>
                        <div class=""input-field col s8 m3"">
                            <input type=""text"" class=""checkj"" id=""residence"" placeholder=""Woonplaats""");
            BeginWriteAttribute("value", " value=\"", 2833, "\"", 2857, 1);
#nullable restore
#line 46 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2841, Model.Residence, 2841, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""residence"">Woonplaats</label>
                        </div>
                        <div class=""input-field col s12 m3"">
                            <input type=""text"" class=""checkj"" id=""username"" placeholder=""Gebruikersnaam""");
            BeginWriteAttribute("value", " value=\"", 3132, "\"", 3155, 1);
#nullable restore
#line 50 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 3140, Model.Username, 3140, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                            <label for=""username"">Gebruikersnaam</label>
                        </div>
                    </div>
                    <div class=""row"">
                        <h6>Aanvullende informatie: </h6>
                        <p>Geslacht: ");
#nullable restore
#line 56 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                Write(Html.Raw(Model.Sex == 0 ? "<b class='blue-text'>Man</b>" : "<b class='pink-text'>Vrouw</b>"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p>Type: <b>");
#nullable restore
#line 57 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                               Write(type[Model.Type]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                        <p>Geboortedatum: <b>");
#nullable restore
#line 58 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                        Write(Model.Birthdate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                        <p>Leeftijd: <b>");
#nullable restore
#line 59 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                   Write(Overstag.Core.General.getAge(Model.Birthdate));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></p>
                    </div>

                    <br />
                    <div class=""center-align"">
                        <a class=""waves-effect waves-light btn orange center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(0);"">Sluiten</a>
                        <a class=""waves-effect waves-light btn green center"" id=""savechanges"">Wijzigingen opslaan</a>
                    </div>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">lock</i>Wachtwoord wijzigen</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Wachtwoord wijzigen</h5>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""tbpw1"" placeholder=""Huidig wachtwoord"" />
                        <label class=""active"" for=""tbpw1"">Huidig wachtword</label>
      ");
            WriteLiteral(@"              </div>
                    <br />
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""tbpw2"" placeholder=""Nieuw wachtwoord"" />
                        <label class=""active"" for=""tbpw2"">Nieuw wachtwoord</label>
                    </div>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""tbpw3"" placeholder=""Nieuw wachtwoord herhalen"" />
                        <label class=""active"" for=""tbpw3"">Wachtwoord herhalen</label>
                    </div>
                    <span id=""pwhelper"" style=""color: red; visibility: hidden;"">Wachtwoorden komen niet overeen</span>
                    <br /><br />
                    <a class=""waves-effect waves-light btn orange center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(1); $(");
            WriteLiteral(@"'.checki').val('');"">Annuleren</a>
                    <a class=""waves-effect waves-light btn green center"" id=""savepass"">Opslaan</a>

                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">security</i>Two-Factor Authenticatie (2FA)</div>
                <div class=""collapsible-body"">
                    <h5>Schakel 2FA in of uit</h5>
                    <p>Met 2FA (Tweetrapsauthenticatie) kun je met behulp van een ander apparaat in 2 stappen inloggen. Dit is veiliger omdat zowel je wachtwoord als de 2fa code nodig is om in te loggen.</p>
                    <div class=""switch"">
                        <label>
                            Uit
                            <input type=""checkbox"" id=""cb2fa"" onchange=""User.toggle2fa()"">
                            <span class=""lever""></span>
                            Aan
                        </label>
                    </div>
                    <br />
            ");
            WriteLiteral(@"        <a class=""btn waves-effect btn-small blue-dark"" id=""btngetqr"" onclick=""User.getQR()"">Code ophalen</a>
                    <div class=""row"" id=""qr"">
                        <div class=""col s12 card-panel"">
                            <div class=""row"">
                                <div class=""col s12 m6"">
                                    <h6><b>Stap 1: </b> Scan deze code met uw 2FA app (zoals <a target=""_blank"" href=""https://play.google.com/store/apps/details?id=com.authy.authy&gl=NL"">Authy</a> of bijv <a target=""_blank"" href=""https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&gl=NL"">Google Authenticator</a>)</h6>
                                    <img class=""materialboxed""");
            BeginWriteAttribute("src", " src=\"", 7581, "\"", 7587, 0);
            EndWriteAttribute();
            WriteLiteral(@" id=""qrimg"" width=""300"" height=""300"" />
                                    <h5 id=""qrcode""></h5>
                                </div>
                                <div class=""col s12 m6"">
                                    <h6><b>Stap 2: </b> Bewaar jouw herstelcodes voor het geval je je secret kwijt bent</h6>
                                    <div class=""row"" id=""tfacodes"">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">person</i>Account</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Account</h5>
                    <h6>Account sluiten</h6>
                    <p>Wanneer u uw account wilt verwijderen, klik dan op &quot;Account verwijderen&quot;.</p>
                    <");
            WriteLiteral("a class=\"waves-effect waves-light btn red center modal-trigger\" data-target=\"delaccount\">Account verwijderen</a>\r\n\r\n");
#nullable restore
#line 138 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                     if (Model.Family != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <h5>Gezin</h5>
                        <p>Uw account is gekoppeld aan een gezin. Wanneer u het gezin wilt verlaten, klik dan op &quot;Gezin verlaten&quot;.</p>
                        <a class=""waves-effect waves-light btn orange center"" onclick=""User.leaveFamily();"">Gezin verlaten</a>
");
#nullable restore
#line 143 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    <br /><br />
                    <a class=""waves-effect waves-light btn green center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(3);"">Annuleren</a>

                </div>
            </li>
        </ul>

        <!--Modal-->
        <div id=""delaccount"" class=""modal"">
            <div class=""modal-content"">
                <h4>Weet je zeker dat je je account wilt verwijderen? <b>Deze actie kan niet ongedaan worden gemaakt!!</b></h4>
                <p>Wanneer u besluit uw account te sluiten, is het volgende van toepassing: </p>
                <p>
                    - 1. Al uw persoonlijke gegevens worden uit onze database verwijderd<br />
                    - 2. U kunt op deze website <b>NIET meer inloggen</b><br />
                    - 3. U kunt uzelf <b>NIET meer inschrijven</b> voor een avond<br />
                    - Uiteraard kunt u opnieuw uzelf registreren als u er spijt van krijgt
                </p><br />
            </div>
           ");
            WriteLiteral(@" <div class=""modal-footer"">
                <a class=""modal-close waves-effect btn green white-text"">Nee natuurlijk niet</a>
                <a class=""modal-close waves-effect btn orange white-text"">Ik wou alleen ff kijken</a>
                <a onclick=""User.delaccount()"" class=""modal-close waves-effect btn red white-text"">Ja, toch verwijderen</a>
            </div>
        </div>

    </div>
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "292d693d9abcc07534b19b9b85bdf57621cfab1718295", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <script>\r\n            var User = {\r\n                init: function () {\r\n                    $(\'#_asetting, #_masetting\').addClass(\'active\');\r\n                    $(\'#cb2fa\').attr(\'checked\', (\'");
#nullable restore
#line 177 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                              Write(!string.IsNullOrEmpty(Model.TwoFactor));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"' == 'True'));

                    if ($('#cb2fa').prop('checked'))
                        $('#btngetqr').show();
                    else
                        $('#btngetqr').hide();

                    $('#qr').hide();
                    User.mapEvents();
                },
                mapEvents: function () {
                    $('#savechanges').click(function () {
                        var validate = true;
                        $('.checkj').each(function () {
                            if ($(this).val() == '') {
                                validate = false;
                                return;
                            }
                        });
                        if (validate) {
                            User.savechanges();
                        } else {
                           M.toast({html: 'Vul a.u.b. alle velden in', classes: 'orange'})
                        }
                    });
                    $('#savepass').click(function (");
            WriteLiteral(@") {
                        var validate = true;
                        validate = ($('#tbpw2').val() == $('#tbpw3').val());
                        $('.checki').each(function () {
                            if ($(this).val() == '') {
                                validate = false;
                                return;
                            }
                        });
                        if (validate) {
                            User.savepass();
                        } else {
                           M.toast({html: 'Vul a.u.b. alle velden correct in', classes: 'orange'})
                        }
                    });

                    //Detect & check if passwords matches
                    $('#tbpw2').on('keyup',function () {
                        if ($('#tbpw2').val() != $('#tbpw3').val()) {
                            $('#pwhelper').css('visibility', 'visible');
                        } else { $('#pwhelper').css('visibility', 'hidden'); }
             ");
            WriteLiteral(@"       });

                    $('#tbpw3').on('keyup',function () {
                        if ($('#tbpw3').val() != $('#tbpw2').val()) {
                            $('#pwhelper').css('visibility', 'visible');
                        } else { $('#pwhelper').css('visibility', 'hidden'); }
                    });
                },
                savechanges: function () {
                    $('#savechanges').addClass('disabled');
                    $.post(""/User/postInfoChange"", { Firstname: $('#firstname').val(), Lastname: $('#lastname').val(), Email: $('#email').val(), Adress: $('#adress').val(), Postalcode: $('#postalcode').val(), Residence: $('#residence').val(), Username: $('#username').val(), Phone: $('#phone').val() }, function (response) {
                        if (response.status == 'success') {
                            M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                            $('.checki').val('');
                            M.Collapsible.getIn");
            WriteLiteral(@"stance($('.collapsible')).close(0);
                        }
                        else {
                            //status = error
                            M.toast({ html: response.error, classes: 'red' });
                            $('.checki').val('');
                        }
                        $('#savechanges').removeClass('disabled');
                    }, 'json');
                },
                savepass: function () {
                    $('#_progbar').show();
                    $('#savepass').addClass('disabled');
                    $.post(""/User/postPasswordChange"", { Oldpass: $('#tbpw1').val(), Newpass: $('#tbpw2').val() }, function (response) {
                        if (response.status == 'success') {
                            M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                            $('.checki').val('');
                            M.Collapsible.getInstance($('.collapsible')).close(1);
                        }
       ");
            WriteLiteral(@"                 else if (response.status == 'warning') {
                            M.toast({ html: response.error, classes: 'orange' });
                        }
                        else {
                            //status = error
                            M.toast({ html: response.error, classes: 'red' });
                            $('.checki').val('');
                        }
                        $('#savepass').removeClass('disabled');
                    }, 'json').done(function () {
                        $('#_progbar').hide();
                    });

                },
                delaccount: function () {
                    var pass = prompt('Typ je wachtwoord in om door te gaan');

                    if (pass == null || pass == '')
                        return;

                    if (confirm('Account verwijderen? Zeker weten?')) {
                        $.post(""/User/postDeleteAccount"", { Token: '");
#nullable restore
#line 277 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                                               Write(Uri.EscapeDataString(Model.Token));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', Password: pass }, function (response) {
                            if (response.status == 'success') {
                                M.toast({ html: 'Account verwijderd', classes: 'green' });
                                localStorage.setItem(""logout"", true);
                                $(location).attr('href', '/Register/Logoff')
                            }
                            else {
                                M.toast({ html: response.error, classes: 'red' });
                            }
                        }, 'json');

                    }
                },
                toggle2fa: function () {
                    var use = $('#cb2fa').prop('checked');
                    $.getJSON('/User/Toggle2FA', function (response) {
                        if (response.status == 'success') {
                            if (response.secret == '') {
                               M.toast({ html: '2FA uitgeschakeld', classes: 'blue' });
                            ");
            WriteLiteral(@"    $('#qr').hide();
                                $('#btngetqr').hide();
                            }
                            else {
                                User.getQR();
                            }
                        }
                        else {
                            M.toast({ html: 'Er is een fout opgetreden', classes: 'red' });
                            $('#cb2fa').attr('checked',!use);
                        }
                    });
                },
                getQR: function () {
                    $.getJSON('/User/Get2FA', function (response) {
                        if (response.status == 'success') {
                            $('#qrimg').attr('src', '/Photo/GetQR/'+encodeURIComponent(response.secret)+'/");
#nullable restore
#line 312 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                                                                                     Write(Model.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
                            $('#qrcode').text(response.secret);
                            $('#qr').show();
                            $('#btngetqr').hide();
                        }
                        else {
                            M.toast({ html: response.error, classes: 'red' });
                        }
                    });

                    $.getJSON('/User/Get2FACodes', function (r) {
                        if (r.status == 'success') {
                            var html = '';
                            $.each(r.data, function(i, e) {
                                html += `<h5 class=""col s6"">${e}</h5>`;
                            });
                            $('#tfacodes').html(html);
                        }
                    });
                },
                leaveFamily: function () {
                    if (confirm(""Weet u zeker dat u het gezin wilt verlaten? Deze actie kan niet ongedaan worden gemaakt."")) {
                        $.get('");
            WriteLiteral(@"/User/LeaveFamily', function (r) {
                            if (r.status == 'success') {
                                M.toast({ html: 'Gezin successvol verlaten', classes: 'green' });
                                setTimeout(window.location.reload.bind(window.location), 750);
                            } else {
                                M.toast({ html: 'Er is iets fout gegaan', classes: 'red' });
                            }
                        }, 'json');
                    }
                },
                
            }

            User.init();

        $(document).ready(function () {
            $('.materialboxed').materialbox();
        });
    </script>
</div>

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