#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1404436b80316426a8762415c05a32476c670acf"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1404436b80316426a8762415c05a32476c670acf", @"/Views/User/Settings.cshtml")]
    public class Views_User_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
  
    Layout = "/Views/_UserLayout.cshtml";
    ViewBag.Title = "Instellingen";

#line default
#line hidden
            BeginContext(119, 809, true);
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
            BeginWriteAttribute("value", " value=\"", 928, "\"", 952, 1);
#line 20 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 936, Model.Firstname, 936, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(953, 382, true);
            WriteLiteral(@" />
                                        <label for=""firstname"">Voornaam</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <input type=""text"" class=""checkj"" id=""lastname"" placeholder=""Achternaam""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1335, "\"", 1358, 1);
#line 26 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1343, Model.Lastname, 1343, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1359, 464, true);
            WriteLiteral(@" />
                                        <label for=""lastname"">Achternaam</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <i class=""material-icons prefix"">email</i>
                                        <input type=""text"" class=""checkj"" id=""email"" placeholder=""Emailadres""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1823, "\"", 1843, 1);
#line 33 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 1831, Model.Email, 1831, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1844, 538, true);
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
            BeginWriteAttribute("value", " value=\"", 2382, "\"", 2403, 1);
#line 42 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2390, Model.Adress, 2390, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2404, 389, true);
            WriteLiteral(@" />
                                        <label for=""adress"">Adres + Huisnummer</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <input type=""text"" class=""checkj"" id=""postalcode"" placeholder=""Postcode""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2793, "\"", 2818, 1);
#line 48 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 2801, Model.Postalcode, 2801, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2819, 384, true);
            WriteLiteral(@" />
                                        <label for=""postalcode"">Postcode</label>
                                    </div>
                                </td>
                                <td>
                                    <div class=""input-field"">
                                        <input type=""text"" class=""checkj"" id=""residence"" placeholder=""Woonplaats""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3203, "\"", 3227, 1);
#line 54 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
WriteAttributeValue("", 3211, Model.Residence, 3211, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3228, 9032, true);
            WriteLiteral(@" />
                                        <label for=""residence"">Woonplaats</label>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    
                    <br />
                    <a class=""waves-effect waves-light btn orange center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(0);"">Annuleren</a>
                    <a class=""waves-effect waves-light btn green center"" id=""savechanges"">Wijzigingen opslaan</a>
                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">lock</i>Wachtwoord wijzigen</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Wachtwoord wijzigen</h5>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <inpu");
            WriteLiteral(@"t type=""password"" class=""checki"" id=""tbpw1"" placeholder=""Huidig wachtwoord"" />
                        <label class=""active"" for=""tbpw1"">Huidig wachtword</label>
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
    ");
            WriteLiteral(@"                <br /><br />
                    <a class=""waves-effect waves-light btn orange center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(1); $('.checki').val('');"">Annuleren</a>
                    <a class=""waves-effect waves-light btn green center"" id=""savepass"">Opslaan</a>

                </div>
            </li>
            <li>
                <div class=""collapsible-header""><i class=""material-icons"">person</i>Account</div>
                <div class=""collapsible-body col s12 m6 center-align"">
                    <h5>Account</h5>
                    <h6>Account sluiten</h6>
                    <p>Wanneer u besluit uw account te sluiten, is het volgende van toepassing: </p>
                    <p>- 1. Al uw persoonlijke gegevens worden uit onze database verwijderd<br />
                    - 2. U kunt op deze website <b>NIET meer inloggen</b><br />
                    - 3. U kunt uzelf <b>NIET meer inschrijven</b> voor een avond<br />
                    - Uiteraa");
            WriteLiteral(@"rd kunt u opnieuw uzelf registreren als u er spijt van krijgt</p><br />
                    <a class=""waves-effect waves-light btn green center"" onclick=""M.Collapsible.getInstance($('.collapsible')).close(2);"">Annuleren</a>
                    <a class=""waves-effect waves-light btn red center modal-trigger"" data-target=""delaccount"">Account verwijderen</a>
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
                    - 3. U kunt uzelf <b>NIET meer inschrijven</b> voo");
            WriteLiteral(@"r een avond<br />
                    - Uiteraard kunt u opnieuw uzelf registreren als u er spijt van krijgt
                </p><br />
            </div>
            <div class=""modal-footer"">
                <a class=""modal-close waves-effect btn green white-text"">Nee natuurlijk niet</a>
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

                    User.mapEvents();
                },
                mapEvents: function () {
                    $('#savechanges').click(function () {
                        var validate = true;
                        $('.checkj').each(functio");
            WriteLiteral(@"n () {
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
                    $('#savepass').click(function () {
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
        ");
            WriteLiteral(@"                } else {
                           M.toast({html: 'Vul a.u.b. alle velden correct in', classes: 'orange'})
                        }
                    });

                    //Detect & check if passwords matches
                    $('#tbpw2').on('keyup',function () {
                        if ($('#tbpw2').val() != $('#tbpw3').val()) {
                            $('#pwhelper').css('visibility', 'visible');
                        } else { $('#pwhelper').css('visibility', 'hidden'); }
                    });

                    $('#tbpw3').on('keyup',function () {
                        if ($('#tbpw3').val() != $('#tbpw2').val()) {
                            $('#pwhelper').css('visibility', 'visible');
                        } else { $('#pwhelper').css('visibility', 'hidden'); }
                    });
                },
                //Gaat fout, login gaat ook fout
                savechanges: function () {
                     $.post(""/User/postInfoChange"", {");
            WriteLiteral(@" Firstname: $('#firstname').val(),Lastname: $('#lastname').val(),Email: $('#email').val(),Adress: $('#adress').val(),Postalcode: $('#postalcode').val(),Residence: $('#residence').val() }, function (response) {
                        if (response.status == 'success') {
                            M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                            $('.checki').val('');
                            M.Collapsible.getInstance($('.collapsible')).close(0);
                        }
                        else {
                            //status = error
                            M.toast({ html: response.error, classes: 'red' });
                            $('.checki').val('');
                        }
                    }, 'json');
                },
                savepass: function () {
                    $.post(""/User/postPasswordChange"", { Oldpass: $('#tbpw1').val(), Newpass: $('#tbpw2').val() }, function (response) {
                        if (re");
            WriteLiteral(@"sponse.status == 'success') {
                            M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                            $('.checki').val('');
                            M.Collapsible.getInstance($('.collapsible')).close(1);
                        }
                        else {
                            //status = error
                            M.toast({ html: response.error, classes: 'red' });
                            $('.checki').val('');
                        }
                    }, 'json');

                },
                delaccount: function () {
                    var pass = prompt('Typ je wachtwoord in om door te gaan');
                    if (confirm('Account verwijderen? Zeker weten?')) {
                        $.post(""/User/postDeleteAccount"", { Token: '");
            EndContext();
            BeginContext(12261, 11, false);
#line 215 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\User\Settings.cshtml"
                                                               Write(Model.Token);

#line default
#line hidden
            EndContext();
            BeginContext(12272, 683, true);
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
                }

            }

            User.init();
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
