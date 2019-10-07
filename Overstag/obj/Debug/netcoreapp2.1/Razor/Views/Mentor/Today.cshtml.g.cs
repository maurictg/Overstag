#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ebd38aa60da4789ded536791c02279f253f6f74"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Today), @"mvc.1.0.view", @"/Views/Mentor/Today.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Mentor/Today.cshtml", typeof(AspNetCore.Views_Mentor_Today))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ebd38aa60da4789ded536791c02279f253f6f74", @"/Views/Mentor/Today.cshtml")]
    public class Views_Mentor_Today : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.NoDB.UserEvent>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
  
    ViewBag.Title = "Deze avond";
    Layout = "~/Views/_MentorLayout.cshtml";
    int totald = 0;
    Model.Participators.RemoveAll(f => f.account.Type != 0);

#line default
#line hidden
            BeginContext(210, 13, true);
            WriteLiteral("\r\n    <div>\r\n");
            EndContext();
#line 10 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
         if (Model.Event == null)
        {

#line default
#line hidden
            BeginContext(269, 231, true);
            WriteLiteral("            <div class=\"center-align\">\r\n                <h3 class=\"red-text\"><b>Geen activiteit vandaag</b></h3>\r\n                <p>Aanwezigheid melden etc. kan natuurlijk alleen als er aanwezigen zijn ;)</p>\r\n            </div>\r\n");
            EndContext();
#line 16 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
        }
        else
        {

#line default
#line hidden
            BeginContext(536, 331, true);
            WriteLiteral(@"            <h3 class=""green-text text-darken-4"">Statistieken</h3>
            <div class=""row"">
                <div class=""col s6 m3"">
                    <div class=""card-panel center-align"">
                        <span class=""flow-text"">Aantal mannen</span>
                        <h3><b class=""blue-text"" id=""aantalm"">");
            EndContext();
            BeginContext(868, 50, false);
#line 24 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                         Write(Model.Participators.Count(f => f.account.Sex == 0));

#line default
#line hidden
            EndContext();
            BeginContext(918, 295, true);
            WriteLiteral(@"</b></h3>
                    </div>
                </div>
                <div class=""col s6 m3"">
                    <div class=""card-panel center-align"">
                        <span class=""flow-text"">Aantal vrouwen</span>
                        <h3><b class=""red-text"" id=""aantalv"">");
            EndContext();
            BeginContext(1214, 50, false);
#line 30 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                        Write(Model.Participators.Count(f => f.account.Sex == 1));

#line default
#line hidden
            EndContext();
            BeginContext(1264, 284, true);
            WriteLiteral(@"</b></h3>
                    </div>
                </div>
                <div class=""col s6 m3"">
                    <div class=""card-panel center-align"">
                        <span class=""flow-text"">Totaal present/absent</span>
                        <h3><b id=""aantal"">");
            EndContext();
            BeginContext(1549, 27, false);
#line 36 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                      Write(Model.Participators.Count());

#line default
#line hidden
            EndContext();
            BeginContext(1576, 93, true);
            WriteLiteral("</b> / <span id=\"aantala\">0</span></h3>\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 39 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                   
                    Model.Participators.ForEach(f => totald += f.part.ConsumptionCount);
                

#line default
#line hidden
            BeginContext(1799, 242, true);
            WriteLiteral("                <div class=\"col s6 m3\">\r\n                    <div class=\"card-panel center-align\">\r\n                        <span class=\"flow-text\">Totaal aantal drankjes</span>\r\n                        <h3><b class=\"green-text\" id=\"aantald\">");
            EndContext();
            BeginContext(2042, 6, false);
#line 45 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                          Write(totald);

#line default
#line hidden
            EndContext();
            BeginContext(2048, 83, true);
            WriteLiteral("</b></h3>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
            EndContext();
            BeginContext(2145, 237, true);
            WriteLiteral("            <br />\r\n            <h3 class=\"green-text text-darken-4\">Aanwezigheid</h3>\r\n            <p class=\"flow-text\">\r\n                &nbsp;Vink de mensen uit die er niet zijn en druk op &quot;Opslaan&quot;<br />\r\n            </p>\r\n");
            EndContext();
            BeginContext(2386, 37, true);
            WriteLiteral("            <ul class=\"collection\">\r\n");
            EndContext();
#line 58 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                 foreach (var p in Model.Participators.OrderBy(f => f.account.Firstname).OrderBy(g => g.account.Lastname))
                {

#line default
#line hidden
            BeginContext(2566, 223, true);
            WriteLiteral("                    <li class=\"collection-item\">\r\n                        <p>\r\n                            <label>\r\n                                <input type=\"checkbox\" class=\"filled-in checki\" checked=\"checked\" data-id=\"");
            EndContext();
            BeginContext(2790, 12, false);
#line 63 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                                                                      Write(p.account.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2802, 11, true);
            WriteLiteral("\" data-sx=\"");
            EndContext();
            BeginContext(2814, 13, false);
#line 63 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                                                                                              Write(p.account.Sex);

#line default
#line hidden
            EndContext();
            BeginContext(2827, 73, true);
            WriteLiteral("\" />\r\n                                <span style=\"font-size: large;\"><b>");
            EndContext();
            BeginContext(2901, 19, false);
#line 64 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                              Write(p.account.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(2920, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(2922, 18, false);
#line 64 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                                                   Write(p.account.Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(2940, 6, true);
            WriteLiteral("</b> (");
            EndContext();
            BeginContext(2947, 95, false);
#line 64 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                                                                            Write(Html.Raw((p.account.Sex == 0) ? "<b class=\"blue-text\">m</b>" : "<b class=\"red-text\">v</b>"));

#line default
#line hidden
            EndContext();
            BeginContext(3042, 105, true);
            WriteLiteral(")</span>\r\n                            </label>\r\n                        </p>\r\n                    </li>\r\n");
            EndContext();
#line 68 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                }

#line default
#line hidden
            BeginContext(3166, 19, true);
            WriteLiteral("            </ul>\r\n");
            EndContext();
            BeginContext(3198, 18, false);
#line 70 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
       Write(Html.Raw("&nbsp;"));

#line default
#line hidden
            EndContext();
            BeginContext(3216, 267, true);
            WriteLiteral(@"<a id=""btnasave"" class=""btn btn-large green waves-effect waves-light modal-trigger"" href=""#areusure""><b>Opslaan</b></a>
            <br />
            <small>&nbsp;Staat er iemand niet tussen? Vraag hem/haar zich alsnog in te schrijven</small>
            <br />
");
            EndContext();
            BeginContext(3485, 476, true);
            WriteLiteral(@"            <h3 class=""green-text text-darken-4"">Drankjes</h3>
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
            EndContext();
#line 87 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                         foreach (var user in Model.Participators)
                        {

#line default
#line hidden
            BeginContext(4056, 70, true);
            WriteLiteral("                            <tr>\r\n                                <td>");
            EndContext();
            BeginContext(4127, 22, false);
#line 90 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                               Write(user.account.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(4149, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(4193, 21, false);
#line 91 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                               Write(user.account.Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(4214, 43, true);
            WriteLiteral("</td>\r\n                                <td>");
            EndContext();
            BeginContext(4258, 80, false);
#line 92 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                               Write(Html.Raw((user.part.FriendCount>0)?"Ja, <b>"+user.part.FriendCount+"</b>":"Nee"));

#line default
#line hidden
            EndContext();
            BeginContext(4338, 222, true);
            WriteLiteral("</td>\r\n                                <td>\r\n                                    <div class=\"row\">\r\n                                        <div class=\"col s6 center-align\">\r\n                                            <h5");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 4560, "\"", 4584, 2);
#line 96 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
WriteAttributeValue("", 4565, user.account.Id, 4565, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 4581, "-cc", 4581, 3, true);
            EndWriteAttribute();
            BeginContext(4585, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(4587, 26, false);
#line 96 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                                                                    Write(user.part.ConsumptionCount);

#line default
#line hidden
            EndContext();
            BeginContext(4613, 174, true);
            WriteLiteral("</h5>\r\n                                        </div>\r\n                                        <div class=\"col s6 left-align\">\r\n                                            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 4787, "\"", 4836, 3);
            WriteAttributeValue("", 4797, "Presence.setDrink(\'", 4797, 19, true);
#line 99 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
WriteAttributeValue("", 4816, user.account.Id, 4816, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 4832, "\',1)", 4832, 4, true);
            EndWriteAttribute();
            BeginContext(4837, 144, true);
            WriteLiteral(" class=\"btn btn-flat green waves-effect white-text\"><i class=\"material-icons\">add_circle</i></a>\r\n                                            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 4981, "\"", 5031, 3);
            WriteAttributeValue("", 4991, "Presence.setDrink(\'", 4991, 19, true);
#line 100 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
WriteAttributeValue("", 5010, user.account.Id, 5010, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 5026, "\',-1)", 5026, 5, true);
            EndWriteAttribute();
            BeginContext(5032, 265, true);
            WriteLiteral(@" class=""btn btn-flat red waves-effect white-text""><i class=""material-icons"">remove_circle</i></a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
");
            EndContext();
#line 105 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
                        }

#line default
#line hidden
            BeginContext(5324, 80, true);
            WriteLiteral("\r\n\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n");
            EndContext();
#line 111 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Today.cshtml"
        }

#line default
#line hidden
            BeginContext(5415, 4333, true);
            WriteLiteral(@"    </div>

    <div id=""areusure"" class=""modal"">
        <div class=""modal-content"">
            <h4>Zeker weten??</h4>
            <p><b>Controleer of de gegevens juist zijn.</b> <br />Als u iemand absent meldt die er toch is, dan wordt dit niet verrekend. Als u iemand present meldt die er niet is, krijgt diegene alsnog een factuur.<br />
            Zorg er daarom voor dat de gegevens kloppen voor u op opslaan klikt.</p>
        </div>
        <div class=""modal-footer"">
            <button class=""btn green darken-4 modal-close"">Annuleren</button>
            <button id=""btnsave"" class=""btn green modal-close"">Opslaan</button>
        </div>
    </div>
<br />

<script>
    var afwezigen = [];

    var Presence = {
        init: function () {
            $('#_pres, #_apres').addClass('active');

            $('.checki').off().on('change', function () {
                Presence.calculateAbsence();
            });

            $('#btnsave').click(Presence.saveAbsence);

            $");
            WriteLiteral(@"('#btndrink').click(function () {
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
        },
        calculateAbsence: function () {
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
     ");
            WriteLiteral(@"       });

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
            $.post('/Mentor/postPresence', { absentids: afw }, function (r) {
                if (r.status == 'success') {
                    M.toast({ html: 'Opgeslagen!', classes: 'green' });
                    $('#btnasave').addClass('disabled');
                    $.each($(""input:checkbox""), function (i, e) {
                        $(e).attr('disabled', 'disabled');
                    });
                } else if (r.status == 'warning') {
                    M.toast({ html: r.warning, classes: 'orange' });
    ");
            WriteLiteral(@"            } else {
                    M.toast({ html: 'Er is iets fout gegaan.', classes: 'red' });
                }
            },'json');
        },
        resetbtn: function(el) {
            $(el).removeClass('red').addClass('green').text('Opslaan');
            $(el).data('confirm', '');
            $(el).fadeIn();
        },
        setDrink: function(id, add) {
            var count = Number($('#' + id + '-cc').text()) + add;
            if (count >= 0 && id > 0 && count <= 15) {
                $.get('/Mentor/setDrink/' + id + '/' + count, function (r) {
                    if (r.status == 'success') {
                        M.toast({ html: 'Drankje ' + (add > 0 ? 'toegevoegd!' : 'verwijderd!'), classes: (add > 0 ? 'green' : 'yellow darken-3') });
                        $('#' + id + '-cc').text(count);
                        $('#aantald').text(Number($('#aantald').text()) + add);
                    } else {
                        M.toast({ html: r.error, classes: 'red' });");
            WriteLiteral("\r\n                    }\r\n                }, \'json\');\r\n            }\r\n        }\r\n    }\r\n\r\n    $(function () {\r\n        Presence.init();\r\n    });\r\n\r\n    $(document).ready(function () {\r\n        $(\'select\').formSelect();\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.NoDB.UserEvent> Html { get; private set; }
    }
}
#pragma warning restore 1591
