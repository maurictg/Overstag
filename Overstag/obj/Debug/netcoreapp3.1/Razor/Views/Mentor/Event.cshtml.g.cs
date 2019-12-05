#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f231676734bfa56d890e4eeac9e98f8671af222f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Event), @"mvc.1.0.view", @"/Views/Mentor/Event.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f231676734bfa56d890e4eeac9e98f8671af222f", @"/Views/Mentor/Event.cshtml")]
    public class Views_Mentor_Event : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.NoDB.UserEvent>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
  
    ViewBag.Title = "Deze avond";
    Layout = "_MentorLayout";
    int totald = 0;
    Model.Participators.RemoveAll(f => f.account.Type != 0);
    List<Overstag.Models.Account> users = new Overstag.Models.OverstagContext().Accounts.Where(f => f.Type == 0).OrderBy(f => f.Firstname).ToList();

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n");
#nullable restore
#line 11 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
     if (Model.Event == null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""center-align"">
            <h3 class=""red-text""><b>Activiteit niet gevonden</b></h3>
            <p>Aanwezigheid melden etc. kan natuurlijk alleen als er aanwezigen zijn ;). Misschien is er geen activiteit vandaag</p>
        </div>
");
#nullable restore
#line 17 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h4 class=\"center-align\"><i>&quot;");
#nullable restore
#line 20 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                     Write(Model.Event.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("&quot; - ");
#nullable restore
#line 20 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                Write(Model.Event.When.ToString("dd-MM-yyyy HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</i></h4>
        <h3 class=""green-text text-darken-4"">Statistieken</h3>
        <div class=""row"">
            <div class=""col s6 m3"">
                <div class=""card-panel center-align"">
                    <span class=""flow-text"">Aantal mannen</span>
                    <h3><b class=""blue-text"" id=""aantalm"">");
#nullable restore
#line 26 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                     Write(Model.Participators.Count(f => f.account.Sex == 0));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></h3>
                </div>
            </div>
            <div class=""col s6 m3"">
                <div class=""card-panel center-align"">
                    <span class=""flow-text"">Aantal vrouwen</span>
                    <h3><b class=""red-text"" id=""aantalv"">");
#nullable restore
#line 32 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                    Write(Model.Participators.Count(f => f.account.Sex == 1));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></h3>
                </div>
            </div>
            <div class=""col s6 m3"">
                <div class=""card-panel center-align"">
                    <span class=""flow-text"">Totaal present/absent</span>
                    <h3><b id=""aantal"">");
#nullable restore
#line 38 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                  Write(Model.Participators.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> / <span id=\"aantala\">0</span></h3>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 41 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
              
                Model.Participators.ForEach(f => totald += f.part.ConsumptionCount);
            

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col s6 m3\">\r\n                <div class=\"card-panel center-align\">\r\n                    <span class=\"flow-text\">Totaal aantal drankjes</span>\r\n                    <h3><b class=\"green-text\" id=\"aantald\">");
#nullable restore
#line 47 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                      Write(totald);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h3>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
            WriteLiteral("        <br />\r\n        <h3 class=\"green-text text-darken-4\">Aanwezigheid</h3>\r\n        <p class=\"flow-text\">\r\n            &nbsp;Vink de mensen uit die er niet zijn en druk op &quot;Opslaan&quot;<br />\r\n        </p>\r\n");
            WriteLiteral("        <ul class=\"collection\">\r\n");
#nullable restore
#line 60 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
             foreach (var p in Model.Participators.OrderBy(f => f.account.Firstname).OrderBy(g => g.account.Lastname))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"collection-item\">\r\n                    <p>\r\n                        <label>\r\n                            <input type=\"checkbox\" class=\"filled-in checki\" checked=\"checked\" data-id=\"");
#nullable restore
#line 65 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                                                  Write(p.account.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-sx=\"");
#nullable restore
#line 65 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                                                                          Write(p.account.Sex);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" />\r\n                            <span style=\"font-size: large;\"><b>");
#nullable restore
#line 66 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                          Write(p.account.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 66 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                               Write(p.account.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> (");
#nullable restore
#line 66 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                                                        Write(Html.Raw((p.account.Sex == 0) ? "<b class=\"blue-text\">m</b>" : "<b class=\"red-text\">v</b>"));

#line default
#line hidden
#nullable disable
            WriteLiteral(")</span>\r\n                        </label>\r\n                    </p>\r\n                </li>\r\n");
#nullable restore
#line 70 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n");
#nullable restore
#line 72 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
   Write(Html.Raw("&nbsp;"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<a id=""btnasave"" class=""btn btn-large green waves-effect waves-light modal-trigger"" href=""#areusure""><b>Opslaan</b></a>
        <br />
        <small>&nbsp;Staat er iemand niet tussen? <a href=""#"" class=""modal-trigger"" data-target=""addperson"">Voeg iemand toe</a></small>
        <br />
");
            WriteLiteral(@"        <h3 class=""green-text text-darken-4"">Drankjes</h3>
        <div class=""row"">
            <table>
                <thead>
                    <tr>
                        <th>Voornaam</th>
                        <th>Achternaam</th>
                        <th>Vriend(inn)en mee?</th>
                        <th>Aantal drankjes</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 89 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                     foreach (var user in Model.Participators)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 92 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                           Write(user.account.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 93 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                           Write(user.account.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 94 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                           Write(Html.Raw((user.part.FriendCount > 0) ? "Ja, <b>" + user.part.FriendCount + "</b>" : "Nee"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>\r\n                                <div class=\"row\">\r\n                                    <div class=\"col s6 center-align\">\r\n                                        <h5");
            BeginWriteAttribute("id", " id=\"", 4559, "\"", 4583, 2);
#nullable restore
#line 98 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
WriteAttributeValue("", 4564, user.account.Id, 4564, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4580, "-cc", 4580, 3, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 98 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                Write(user.part.ConsumptionCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                    </div>\r\n                                    <div class=\"col s6 left-align\">\r\n                                        <a");
            BeginWriteAttribute("onclick", " onclick=\"", 4774, "\"", 4823, 3);
            WriteAttributeValue("", 4784, "Presence.setDrink(\'", 4784, 19, true);
#nullable restore
#line 101 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
WriteAttributeValue("", 4803, user.account.Id, 4803, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4819, "\',1)", 4819, 4, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-flat green waves-effect white-text\"><i class=\"material-icons\">add_circle</i></a>\r\n                                        <a");
            BeginWriteAttribute("onclick", " onclick=\"", 4964, "\"", 5014, 3);
            WriteAttributeValue("", 4974, "Presence.setDrink(\'", 4974, 19, true);
#nullable restore
#line 102 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
WriteAttributeValue("", 4993, user.account.Id, 4993, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5009, "\',-1)", 5009, 5, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-flat red waves-effect white-text\"><i class=\"material-icons\">remove_circle</i></a>\r\n                                    </div>\r\n                                </div>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 107 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                </tbody>\r\n            </table>\r\n        </div>\r\n");
#nullable restore
#line 113 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>

<div id=""areusure"" class=""modal"">
    <div class=""modal-content"">
        <h4>Zeker weten??</h4>
        <p>
            <b>Controleer of de gegevens juist zijn.</b> <br />Als u iemand absent meldt die er toch is, dan wordt dit niet verrekend. Als u iemand present meldt die er niet is, krijgt diegene alsnog een factuur.<br />
            Zorg er daarom voor dat de gegevens kloppen voor u op opslaan klikt.
        </p>
    </div>
    <div class=""modal-footer"">
        <button class=""btn green darken-4 modal-close"">Annuleren</button>
        <button id=""btnsave"" class=""btn green modal-close"">Opslaan</button>
    </div>
</div>
<div id=""addperson"" class=""modal"">
    <div class=""modal-content"">
        <h4>Iemand toevoegen</h4>
        <div class=""input-field col s12"">
            <select id=""seluser"">
");
#nullable restore
#line 134 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                 foreach (var user in users)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <option");
            BeginWriteAttribute("value", " value=\"", 6291, "\"", 6307, 1);
#nullable restore
#line 136 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
WriteAttributeValue("", 6299, user.Id, 6299, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 136 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                        Write(user.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 136 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                        Write(user.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</option>\r\n");
#nullable restore
#line 137 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </select>
            <label>Voeg iemand toe</label>
        </div>
    </div>
    <div class=""modal-footer"">
        <button class=""btn green darken-4 modal-close waves-effect"">Annuleren</button>
        <button onclick=""Presence.addUser();"" class=""btn green modal-close waves-effect"">Toevoegen</button>
    </div>
</div>
<br />


");
#nullable restore
#line 150 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
 if (Model.Event != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script>
     var afwezigen = [];

     var Presence = {
        init: function () {
            $('#_pres, #_apres').addClass('active');

            $('.checki').off().on('change', function () {
                Presence.calculateAbsence();
            });

            $('#btnsave').click(Presence.saveAbsence);

            $('#btndrink').click(function () {
                if ($(this).data('confirm') == 'yes') {
                    $(this).removeClass('red').addClass('green disabled').text('Opslaan...');
                    $(this).data('confirm', '');
                    Presence.addDrink();
                } else {
                    $(this).removeClass('green').addClass('red').text('Zeker weten?');
                    $(this).data('confirm', 'yes');
                    $(this).fadeOut(4000);
                    setTimeout(Presence.resetbtn, 4000, $(this));
                }
            });

            $('select').formSelect();
        },
        calculateAbsence: function ()");
            WriteLiteral(@" {
            var totaal = 0;
            var totaala = 0;
            var mannen = 0;
            var vrouwen = 0;
            afwezigen = [];

            $.each($(""input:checkbox:checked""), function (i, e) {
                totaal++;
                if ($(e).data('sx') == '0')
                    mannen++;
                else
                    vrouwen++;
            });

            $.each($(""input:checkbox:not(:checked)""), function (i, e) {
                totaala++;
                afwezigen.push($(e).data('id'));
            });

            $('#aantalm').text(mannen);
            $('#aantalv').text(vrouwen);
            $('#aantal').text(totaal);
            $('#aantala').text(totaala);
        },
        saveAbsence: function () {
            Presence.calculateAbsence();
            var afw = JSON.stringify(afwezigen);
            $.post('/Mentor/postPresence', { absentids: afw, eventID: ");
#nullable restore
#line 208 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                                                 Write(Model.Event.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" }, function (r) {
                if (r.status == 'success') {
                    M.toast({ html: 'Opgeslagen!', classes: 'green' });
                    $('#btnasave').addClass('disabled');
                    $.each($(""input:checkbox""), function (i, e) {
                        $(e).attr('disabled', 'disabled');
                    });
                } else if (r.status == 'warning') {
                    M.toast({ html: r.warning, classes: 'orange' });
                } else {
                    M.toast({ html: 'Er is iets fout gegaan.', classes: 'red' });
                }
            },'json');
        },
         addUser: function () {
           $('#_progbar').show();
           $.getJSON('/Mentor/addUser/");
#nullable restore
#line 224 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                 Write(Model.Event.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/' + $('#seluser').val(), function(r) {
               if (r.status == 'success') {
                   M.toast({ html: 'Gebruiker toegevoegd!', classes: 'green' });
                   Core.doReload(500);
               } else {
                   M.toast({ html: r.error, classes: 'red' });
                   $('#_progbar').hide();
               }
           });
        },
        resetbtn: function(el) {
            $(el).removeClass('red').addClass('green').text('Opslaan');
            $(el).data('confirm', '');
            $(el).fadeIn();
        },
        setDrink: function(id, add) {
            var count = Number($('#' + id + '-cc').text()) + add;
            if (count >= 0 && id > 0 && count <= 15) {
                $.get('/Mentor/setDrink/");
#nullable restore
#line 242 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
                                   Write(Model.Event.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/' + id + '/' + count, function (r) {
                    if (r.status == 'success') {
                        M.toast({ html: 'Drankje ' + (add > 0 ? 'toegevoegd!' : 'verwijderd!'), classes: (add > 0 ? 'green' : 'yellow darken-3') });
                        $('#' + id + '-cc').text(count);
                        $('#aantald').text(Number($('#aantald').text()) + add);
                    } else {
                        M.toast({ html: r.error, classes: 'red' });
                    }
                }, 'json');
            }
        }
    }

    $(function () {
        Presence.init();
    });

    $(document).ready(function () {
        $('select').formSelect();
    });
</script>
");
#nullable restore
#line 263 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Event.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.NoDB.UserEvent> Html { get; private set; }
    }
}
#pragma warning restore 1591
