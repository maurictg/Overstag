#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d223239ec0b8fcf9592ad46ba295bf2cc1fdff0b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Settings), @"mvc.1.0.view", @"/Views/User/Settings.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Settings.cshtml", typeof(AspNetCore.Views_User_Settings))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d223239ec0b8fcf9592ad46ba295bf2cc1fdff0b", @"/Views/User/Settings.cshtml")]
    public class Views_User_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
  
    Layout = "/Views/_UserLayout.cshtml";
    ViewBag.Title = "Instellingen";
    string[] type = { "Lid", "Ouder", "Mentor", "Administrator" };

#line default
#line hidden
            BeginContext(187, 809, true);
            WriteLiteral(@"<div>
    <h3 class=""center-align"">Accountinstellingen</h3><br />
    <div>
        <ul class=""collapsible"">
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">info</i>Persoonlijke informatie</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Persoonlijke informatie</h5>
                    <table class=""center-align responsive-table"">
                        <tbody>
                            <tr>
                                <td>
                                    <div class=""input-field"">
                                        <i class=""material-icons prefix"">account_circle</i>
                                        <input type=""text"" class=""checkj"" id=""firstname"" placeholder=""Voornaam""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 996, "\"", 1020, 1);
#line 21 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1004, Model.Firstname, 1004, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1021, 382, true);
            WriteLiteral(@" />
                                        <label for=""firstname"">Voornaam</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <input type=""text"" class=""checkj"" id=""lastname"" placeholder=""Achternaam""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1403, "\"", 1426, 1);
#line 27 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1411, Model.Lastname, 1411, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1427, 464, true);
            WriteLiteral(@" />
                                        <label for=""lastname"">Achternaam</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <i class=""material-icons prefix"">email</i>
                                        <input type=""text"" class=""checkj"" id=""email"" placeholder=""Emailadres""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1891, "\"", 1911, 1);
#line 34 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1899, Model.Email, 1899, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1912, 538, true);
            WriteLiteral(@" />
                                        <label for=""email"">Emailadres</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class=""input-field"">
                                        <i class=""material-icons prefix"">home</i>
                                        <input type=""text"" class=""checkj"" id=""adress"" placeholder=""Adres + Huisnummer""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2450, "\"", 2471, 1);
#line 43 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2458, Model.Adress, 2458, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2472, 389, true);
            WriteLiteral(@" />
                                        <label for=""adress"">Adres + Huisnummer</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <input type=""text"" class=""checkj"" id=""postalcode"" placeholder=""Postcode""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2861, "\"", 2886, 1);
#line 49 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2869, Model.Postalcode, 2869, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2887, 384, true);
            WriteLiteral(@" />
                                        <label for=""postalcode"">Postcode</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <input type=""text"" class=""checkj"" id=""residence"" placeholder=""Woonplaats""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3271, "\"", 3295, 1);
#line 55 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 3279, Model.Residence, 3279, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3296, 410, true);
            WriteLiteral(@" />
                                        <label for=""residence"">Woonplaats</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan=""3"">
                                    <h6>Aanvullende informatie: </h6>
                                    <p>Geslacht: ");
            EndContext();
            BeginContext(3707, 92, false);
#line 63 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                            Write(Html.Raw(Model.Sex == 0 ? "<b class='blue-text'>Man</b>" : "<b class='pink-text'>Vrouw</b>"));

#line default
#line hidden
            EndContext();
            BeginContext(3799, 54, true);
            WriteLiteral("</p>\r\n                                    <p>Type: <b>");
            EndContext();
            BeginContext(3854, 16, false);
#line 64 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                           Write(type[Model.Type]);

#line default
#line hidden
            EndContext();
            BeginContext(3870, 67, true);
            WriteLiteral("</b></p>\r\n                                    <p>Geboortedatum: <b>");
            EndContext();
            BeginContext(3938, 38, false);
#line 65 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                                    Write(Model.Birthdate.ToString("dd-MM-yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(3976, 62, true);
            WriteLiteral("</b></p>\r\n                                    <p>Leeftijd: <b>");
            EndContext();
            BeginContext(4039, 45, false);
#line 66 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                               Write(Overstag.Core.General.getAge(Model.Birthdate));

#line default
#line hidden
            EndContext();
            BeginContext(4084, 3848, true);
            WriteLiteral(@"</b></p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <a class=""waves-effect waves-light btn orange center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(0);"">Sluiten</a>
                    <a class=""waves-effect waves-light btn green center"" id=""savechanges"">Wijzigingen opslaan</a>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">lock</i>Wachtwoord wijzigen</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Wachtwoord wijzigen</h5>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""tbpw1"" placeholder=""Huidig wachtwoord"" />
                        <label class=""active"" for=""tbpw1"">Huidig w");
            WriteLiteral(@"achtword</label>
                    </div>
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
                    <a class=""waves-effect waves-light btn orange center"" onclick=""M.Collapsible.getInstance($('.col");
            WriteLiteral(@"lapsible')).close(1); $('.checki').val('');"">Annuleren</a>
                    <a class=""waves-effect waves-light btn green center"" id=""savepass"">Opslaan</a>

                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">security</i>Two-Factor Authenticatie (2FA)</div>
                <div class=""collapsible-body"">
                    <h6>Schakel 2FA in of uit</h6>
                    <div class=""switch"">
                        <label>
                            Uit
                            <input type=""checkbox"" id=""cb2fa"" onchange=""User.toggle2fa()"">
                            <span class=""lever""></span>
                            Aan
                        </label>
                    </div>
                    <br />
                    <a class=""btn waves-effect btn-small blue-dark"" id=""btngetqr"" onclick=""User.getQR()"">Code ophalen</a>
                    <div id=""qr"">
                        <h6>Scan deze c");
            WriteLiteral(@"ode met uw 2FA app (zoals Authy)</h6>
                        <img class=""materialboxed"" src="""" id=""qrimg"" width=""350"" height=""350""/>
                        <h5 id=""qrcode""></h5>
                    </div>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">person</i>Account</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Account</h5>
                    <h6>Account sluiten</h6>
                    <p>Wanneer u uw account wilt verwijderen, klik dan op &quot;Account verwijderen&quot;.</p>
                    <a class=""waves-effect waves-light btn red center modal-trigger"" data-target=""delaccount"">Account verwijderen</a>

");
            EndContext();
#line 132 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                     if (Model.Family != null)
                    {

#line default
#line hidden
            BeginContext(8003, 314, true);
            WriteLiteral(@"                        <h5>Gezin</h5>
                        <p>Uw account is gekoppeld aan een gezin. Wanneer u het gezin wilt verlaten, klik dan op &quot;Gezin verlaten&quot;.</p>
                        <a class=""waves-effect waves-light btn orange center"" onclick=""User.leaveFamily();"">Gezin verlaten</a>
");
            EndContext();
#line 137 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                    }

#line default
#line hidden
            BeginContext(8340, 1635, true);
            WriteLiteral(@"                    <a class=""waves-effect waves-light btn green center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(3);"">Annuleren</a>

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
            <div class=""modal-footer"">
       ");
            WriteLiteral(@"         <a class=""modal-close waves-effect btn green white-text"">Nee natuurlijk niet</a>
                <a class=""modal-close waves-effect btn orange white-text"">Ik wou alleen ff kijken</a>
                <a onclick=""User.delaccount()"" class=""modal-close waves-effect btn red white-text"">Ja, toch verwijderen</a>
            </div>
        </div>

    </div>
    <script>
            var User = {
                init: function () {
                    $('#_asetting').addClass('active');
                    $('#_masetting').addClass('active');
                    $('#cb2fa').attr('checked', ('");
            EndContext();
            BeginContext(9977, 38, false);
#line 169 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                              Write(!string.IsNullOrEmpty(Model.TwoFactor));

#line default
#line hidden
            EndContext();
            BeginContext(10016, 4491, true);
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
                //Gaat fout, login gaat ook fout
                savechanges: function () {
                     $.post(""/User/postInfoChange"", { Firstname: $('#firstname').val(),Lastname: $('#lastname').val(),Email: $('#email').val(),Adress: $('#adress').val(),Postalcode: $('#postalcode').val(),Residence: $('#residence').val() }, function (response) {
                        if (response.status == 'success') {
                            M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                            $('.checki').val('');
                            M.Collapsible.getInstance($('.collapsible')).close(0);
                        }
         ");
            WriteLiteral(@"               else {
                            //status = error
                            M.toast({ html: response.error, classes: 'red' });
                            $('.checki').val('');
                        }
                    }, 'json');
                },
                savepass: function () {
                    $.post(""/User/postPasswordChange"", { Oldpass: $('#tbpw1').val(), Newpass: $('#tbpw2').val() }, function (response) {
                        if (response.status == 'success') {
                            M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                            $('.checki').val('');
                            M.Collapsible.getInstance($('.collapsible')).close(1);
                        }
                        else {
                            //status = error
                            M.toast({ html: response.error, classes: 'red' });
                            $('.checki').val('');
                        }
              ");
            WriteLiteral(@"      }, 'json');

                },
                delaccount: function () {
                    var pass = prompt('Typ je wachtwoord in om door te gaan');

                    if (pass == null || pass == '')
                        return;

                    if (confirm('Account verwijderen? Zeker weten?')) {
                        $.post(""/User/postDeleteAccount"", { Token: '");
            EndContext();
            BeginContext(14508, 33, false);
#line 260 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                                               Write(Uri.EscapeDataString(Model.Token));

#line default
#line hidden
            EndContext();
            BeginContext(14541, 1375, true);
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
                                M.toast({ html: '2FA ingeschakeld', classes: 'green' });
                                $('#qrimg').attr('src', '/Photo/GetQR/'+encodeURIComponent('otpauth://totp/Overstag:");
            EndContext();
            BeginContext(15917, 14, false);
#line 284 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                                                                                               Write(Model.Username);

#line default
#line hidden
            EndContext();
            BeginContext(15931, 825, true);
            WriteLiteral(@"?secret=' + response.secret + '&issuer=Overstag'));
                                $('#qrcode').text(response.secret);
                                $('#qr').show();
                                $('#btngetqr').hide();
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
                           $('#qrimg').attr('src', '/Photo/GetQR/'+encodeURIComponent('otpauth://totp/Overstag:");
            EndContext();
            BeginContext(16757, 14, false);
#line 299 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\User\Settings.cshtml"
                                                                                                          Write(Model.Username);

#line default
#line hidden
            EndContext();
            BeginContext(16771, 1322, true);
            WriteLiteral(@"?secret=' + response.secret + '&issuer=Overstag'));
                           $('#qrcode').text(response.secret);
                            $('#qr').show();
                            $('#btngetqr').hide();
                        }
                        else {
                            M.toast({ html: response.error, classes: 'red' });
                        }
                    });
                },
                leaveFamily: function () {
                    if (confirm(""Weet u zeker dat u het gezin wilt verlaten? Deze actie kan niet ongedaan worden gemaakt."")) {
                        $.get('/User/LeaveFamily', function (r) {
                            if (r.status == 'success') {
                                M.toast({ html: 'Gezin successvol verlaten', classes: 'green' });
                                setTimeout(window.location.reload.bind(window.location), 750);
                            } else {
                                M.toast({ html: 'Er is iets fout geg");
            WriteLiteral(@"aan', classes: 'red' });
                            }
                        }, 'json');
                    }
                }

            }

            User.init();

    $(document).ready(function () {
        $('.materialboxed').materialbox();
    });
    </script>
</div>

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