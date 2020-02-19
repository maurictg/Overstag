#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cb1fa3d260773bfe953284b2226aaecee030bbc3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Accountancy_Index), @"mvc.1.0.view", @"/Views/Mentor/Accountancy/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cb1fa3d260773bfe953284b2226aaecee030bbc3", @"/Views/Mentor/Accountancy/Index.cshtml")]
    public class Views_Mentor_Accountancy_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.NoDB._Transactions>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
  
    Layout = "_AccountancyLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <div class=""row"">
        <!--Boekhouding overzicht-->
        <div class=""row"">
            <ul class=""tabs tabs-transparent tabs-fixed-width red darken-4"">
                <li class=""tab""><a class=""active"" href=""#transactions"">Transactieoverzicht</a></li>
                <li class=""tab""><a href=""#stats"">Statistieken</a></li>
                <li class=""tab disabled""><a href=""#accountancy"">Boekhouding</a></li>
            </ul>
        </div>

        <h5 class=""red-text hide-on-med-and-up""><i class=""material-icons"">warning</i>&nbsp;Deze pagina werkt beter op een computer of een ander groot scherm.</h5>

        <div id=""transactions"">
            <div class=""col s12"">
                <div class=""col s12"">
                    Toon nieuwste:
                    &nbsp;
                    <div class=""input-field inline"">
                        <input id=""limit"" type=""number"" max=""1000"" style=""width: 5em""");
            BeginWriteAttribute("value", " value=\"", 1026, "\"", 1085, 1);
#nullable restore
#line 24 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 1034, Html.Raw((Model.Limit == -1) ? 1000 : Model.Limit), 1034, 51, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>&nbsp;
                    <button class=""btn inline red darken-4 waves-effect"" onclick=""Transactions.filter()"">Go</button>
                    &nbsp;
                    <button class=""btn red darken-4 waves-effect modal-trigger"" onclick=""Transactions.updateDD()"" data-target=""add"">Transactie toevoegen</button>
                    <br />
                </div>
            </div>
            <table>
                <tr>
                    <td class=""grey darken-1 white-text center-align row"">
                        <div class=""col s5"">
                            Geld in de kas: <b id=""cash"">&euro;&nbsp;");
#nullable restore
#line 36 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                                                Write(Math.Round((double)Model.Balance / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b>
                        </div>
                        <div class=""col s7"">
                            <div class=""switch"">
                                <label class=""white-text"">
                                    Alleen definitief
                                    <input type=""checkbox"" id=""useDC"">
                                    <span class=""lever""></span>
                                    Ook debit/credit
                                </label>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <table class=""responsive-table"">
                <thead>
                    <tr>
                        <th>Bedrag</th>
                        <th>Datum</th>
                        <th>Omschrijving</th>
                        <th>Type</th>
                        <th>Acties</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 62 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                     foreach (var t in Model.Transactions)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr");
            BeginWriteAttribute("class", " class=\"", 2910, "\"", 2973, 1);
#nullable restore
#line 64 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 2918, Html.Raw(t.Payed?"":"grey lighten-1 debitcredit hide"), 2918, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 2974, "\"", 2986, 2);
            WriteAttributeValue("", 2979, "t-", 2979, 2, true);
#nullable restore
#line 64 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 2981, t.Id, 2981, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <td>");
#nullable restore
#line 65 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                           Write(Html.Raw((t.Amount > 0) ? $"<b class=\"green-text text-accent-4\">+ &euro;{Math.Round((double)t.Amount / 100, 2).ToString("F")}</b>" : $"<b class=\"red-text\">- &euro;{Math.Round((double)t.Amount / 100, 2).ToString("F").Replace("-", "")}</b>"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 66 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                           Write(t.When.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 67 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                           Write(t.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 68 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                           Write(Html.Raw((t.Type != null) ? Overstag.Accountancy.Transactions.Types[Convert.ToInt32(t.Type)] : "-"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>\r\n                                <button class=\"btn-small red waves-effect modal-trigger\" data-target=\"areusure\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3679, "\"", 3723, 3);
            WriteAttributeValue("", 3689, "$(\'#btndelete\').data(\'id\',\'", 3689, 27, true);
#nullable restore
#line 70 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 3716, t.Id, 3716, 5, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3721, "\')", 3721, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Verwijderen</button>\r\n");
#nullable restore
#line 71 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                 if (!t.Payed)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <button class=\"btn-small blue waves-effect modal-trigger\"");
            BeginWriteAttribute("id", " id=\"", 3923, "\"", 3939, 2);
            WriteAttributeValue("", 3928, "btnsp-", 3928, 6, true);
#nullable restore
#line 73 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 3934, t.Id, 3934, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-target=\"areusure2\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3964, "\"", 4010, 3);
            WriteAttributeValue("", 3974, "$(\'#btnsetpayed\').data(\'id\',\'", 3974, 29, true);
#nullable restore
#line 73 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 4003, t.Id, 4003, 5, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4008, "\')", 4008, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Definitief maken</button>\r\n");
#nullable restore
#line 74 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                 if(t.Type == 29)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <a class=\"btn-small green waves-effect\"");
            BeginWriteAttribute("href", " href=\"", 4235, "\"", 4272, 2);
            WriteAttributeValue("", 4242, "/Accountancy/Declaration/", 4242, 25, true);
#nullable restore
#line 77 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 4267, t.Id, 4267, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Open declaratie</a>\r\n");
#nullable restore
#line 78 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 81 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </tbody>
            </table>
        </div>

        <div id=""stats"">
            <div class=""row"">
                <div class=""col s12 m4"">
                    <canvas id=""pie"" style=""height: 1em; width: 1em""></canvas>
                </div>
                <div class=""col s12 m4"">
                    <canvas id=""pie2a"" style=""height: 1em; width: 1em""></canvas>
                </div>
                <div class=""col s12 m4"">
                    <canvas id=""pie2b"" style=""height: 1em; width: 1em""></canvas>
                </div>
            </div>
            <br />
            <div class=""row"">
                <div class=""col s12 m8 offset-m2"">
                    <canvas id=""stat"" style=""height: 1em; width: 1em""></canvas>
                </div>
            </div>
        </div>
    </div>

    <!-- In aanbouw -->
    <div id=""accountancy"">

    </div>

    <div id=""add"" class=""modal fullscreen-modal"">
        <div class=""modal-content"">
            <h4>Transact");
            WriteLiteral(@"ie toevoegen</h4>
            <br />
            <div class=""row"">
                <div class=""row"">
                    <div class=""input-field col s6 m4"">
                        <input id=""amount"" type=""text"" class=""validate tinput"" />
                        <label for=""amount"">Bedrag in €</label>
                        <span class=""helper-text"" data-error=""Vul a.u.b. een geldig bedrag in (kommagetal)""></span>
                    </div>
                    <div class=""col s6 m4"">
                        <p>
                            <label>
                                <input class=""with-gap"" value=""plus"" checked=""checked"" name=""mode"" type=""radio"" />
                                <span>Bij</span>
                            </label>
                            &nbsp;&nbsp;
                            <label>
                                <input class=""with-gap"" value=""min"" name=""mode"" type=""radio"" />
                                <span>Af</span>
                            <");
            WriteLiteral(@"/label>
                        </p>
                    </div>
                    <div class=""col s12 m4 row"">
                        <div class=""right-align col s6"">
                            <h6>Is definitief</h6>
                        </div>
                        <div class=""input-field left-align col s6"">
                            <div class=""switch"">
                                <label>
                                    Nee
                                    <input type=""checkbox"" id=""isDC"">
                                    <span class=""lever""></span>
                                    Ja
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class=""input-field col s12"">
                    <input id=""desc"" type=""text"" class=""validate tinput"" />
                    <label for=""desc"">Omschrijving</label>
                    ");
            WriteLiteral(@"<span class=""helper-text"" data-error=""Vul a.u.b. dit veld in""></span>
                </div>

                <div class=""row"">
                    <div class=""input-field col s12 m6"">
                        <input id=""when"" type=""text"" class=""datepicker tinput"" />
                        <label for=""when"">Datum</label>
                    </div>
                    <div class=""input-field col s12 m6"" id=""bij"">
                        <select id=""selbij"">
                            <option value=""1"" selected>Activiteiten</option>
                            <option value=""2"">Subsidie</option>
                            <option value=""3"">Sponsor (bedrijf)</option>
                            <option value=""4"">Sponsor (particulier)</option>
                            <option value=""5"">Sponsor (kerk)</option>
                            <option value=""6"">Overige</option>
                        </select>
                        <label>Selecteer inkomst</label>
                    </div>
  ");
            WriteLiteral(@"                  <div class=""input-field col s12 m6"" id=""af"" style=""display: none;"">
                        <select id=""selaf"">
                            <option value=""21"" selected>Boodschappen</option>
                            <option value=""22"">Huur gebouw</option>
                            <option value=""23"">Website</option>
                            <option value=""24"">Activiteiten</option>
                            <option value=""25"">Drukwerk</option>
                            <option value=""26"">Inboedel</option>
                            <option value=""27"">Reiskosten</option>
                            <option value=""28"">Overige</option>
                        </select>
                        <label>Selecteer uitgave</label>
                    </div>
                </div>

            </div>
        </div>
        <div class=""modal-footer"">
            <button class=""btn modal-close waves-effect"" onclick=""$('#amount, #desc').val('');"">Annuleren</button>
         ");
            WriteLiteral(@"   <button class=""btn waves-effect green"" onclick=""Transactions.addTransaction()"">Toevoegen</button>
        </div>
    </div>

    <div id=""areusure"" class=""modal"">
        <div class=""modal-content"">
            <h4>Weet je zeker dat je deze transactie wilt verwijderen?</h4>
            <p>Dit kan niet ongedaan worden gemaakt!</p>
        </div>
        <div class=""modal-footer"">
            <button class=""modal-close btn green waves-effect"">Annuleren</button>
            <button id=""btndelete"" onclick=""Transactions.delete($('#btndelete').data('id'));"" class=""btn red waves-effect modal-close"">Verwijderen</button>
        </div>
    </div>

    <div id=""areusure2"" class=""modal"">
        <div class=""modal-content"">
            <h4>Weet je zeker dat je deze transactie definitief wilt maken?</h4>
            <p>Dit kan niet ongedaan worden gemaakt!</p>
        </div>
        <div class=""modal-footer"">
            <button class=""modal-close btn orange waves-effect"">Annuleren</button>
      ");
            WriteLiteral("      <button id=\"btnsetpayed\" onclick=\"Transactions.setPayed($(\'#btnsetpayed\').data(\'id\'));\" class=\"btn green waves-effect modal-close\">Jazeker</button>\r\n        </div>\r\n    </div>\r\n\r\n<script>\r\n    let balanceWithDC = \'&euro;&nbsp;");
#nullable restore
#line 221 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                Write(Math.Round((double)Model.BalanceWithDC / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n    let balance = \'&euro;&nbsp;");
#nullable restore
#line 222 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                          Write(Math.Round((double)Model.Balance / 100, 2).ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
    var Transactions = {
        init: function () {
            $('#_acc, #_macc').addClass('active');
            Transactions.createLineChart();
            Transactions.createPieChart();
            Transactions.createPieChart2a();
            Transactions.createPieChart2b();
            $('#isDC').prop('checked', true);
            $('#useDC').prop('checked', false);
            $('.tinput').val('');
            Transactions.mapEvents();
        },
        mapEvents: function () {
            $(""input[name='mode']"").change(function () {
                Transactions.updateDD();
            });

            $('#useDC').change(function(){
                if ($(this).prop('checked')) {
                    $('.debitcredit').removeClass('hide');
                    $('#cash').html(balanceWithDC);
                } else {
                    $('.debitcredit').addClass('hide');
                    $('#cash').html(balance);
                }
            });
        },
        setPay");
            WriteLiteral(@"ed: function (id) {
            $.post('/Accountancy/setTransactionAsPayed', { id: id }, function (r) {
                if (r.status == 'success') {
                    M.toast({ html: 'Wijzigingen opgeslagen', classes: 'green' });
                    $('#t-' + id).removeClass('grey lighten-1 debitcredit');
                    $('#btnsp-' + id).remove();
                }
            });
        },
        updateDD: function () {
            //Select dropdown
            if ($(""input[name='mode']:checked"").val() == 'min') {
                $('#bij').hide();
                $('#af').show();
            } else {
                $('#bij').show();
                $('#af').hide();
            }
        },
        createLineChart: function () {
            var data = {
                labels: [");
#nullable restore
#line 271 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                    Write(Html.Raw("'"+string.Join(',', Model.Transactions_.Select(f => f.When)).Replace(",","','")+"'"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"],
                datasets: [
                {
                    label: 'Inhoud kas',
                    borderColor: '#B71C1C',
                    backgroundColor: '#B71C1C',
                    fill: false,
                    data: [
                        ");
#nullable restore
#line 279 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                   Write(Html.Raw(string.Join(',', Model.Transactions_.Select(f => f.Amount))));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    ]
                }]
            };

            var options = {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            };

            var tc = new Chart($('#stat'), {
                type: 'line',
                data: data,
                options: options
            });
        },
        createPieChart: function () {
            var data = {
                datasets: [{
                    data: [");
#nullable restore
#line 303 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                      Write(Html.Raw((Math.Round(Model.In/100,2)).ToString("F").Replace(",", ".")));

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 303 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                                                                               Write(Html.Raw((Math.Round(Model.Out/100,2)).ToString("F").Replace(",", ".")));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"],
                    backgroundColor: [
                        '#00C853',
                        '#F44336'
                    ],
                }],

                labels: [
                   'Inkomsten', 'Uitgaven'
                ]
            };

            var chart = new Chart($('#pie'), {
                type: 'pie',
                data: data
            });
        },
        createPieChart2a: function(){
            var data = {
                datasets: [{
                    data: [
                        ");
#nullable restore
#line 324 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                   Write(string.Join(",", Model.InPerType.Values.Select(f => Math.Round((double)f/100,2).ToString("F").Replace(",","."))));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    ],
                    backgroundColor: [
                        '#dc3042', '#108a19', '#ba197d', '#84db3f', '#0c86bf', '#741a93', '#bc506d', '#d47bdb', '#0aaff5', '#062ed3',
                        '#ad923a', '#908281', '#9505ed', '#06a157', '#6b3a63', '#a1f613', '#1c36b3'
                    ]
                }],

                labels: [
                   ");
#nullable restore
#line 333 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
              Write(Html.Raw(string.Join(",", Overstag.Accountancy.Transactions.Types.Where(g => g.Key <= 20).Select(f => "'"+f.Value+"'"))));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                ]
            };

            var chart = new Chart($('#pie2a'), {
                type: 'pie',
                data: data
            });
        },
        createPieChart2b: function(){
            var data = {
                datasets: [{
                    data: [
                        ");
#nullable restore
#line 346 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                   Write(string.Join(",", Model.OutPerType.Values.Select(f => Math.Round((double)f/100,2).ToString("F").Replace(",","."))));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    ],
                    backgroundColor: [
                        '#dc3042', '#108a19', '#ba197d', '#84db3f', '#0c86bf', '#741a93', '#bc506d', '#d47bdb', '#0aaff5', '#062ed3',
                        '#ad923a', '#908281', '#9505ed', '#06a157', '#6b3a63', '#a1f613', '#1c36b3'
                    ]
                }],

                labels: [
                   ");
#nullable restore
#line 355 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
              Write(Html.Raw(string.Join(",", Overstag.Accountancy.Transactions.Types.Where(g => g.Key > 20).Select(f => "'"+f.Value+"'"))));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                ]
            };

            var chart = new Chart($('#pie2b'), {
                type: 'pie',
                data: data
            });
        },
        validate: function () {
            var valid = true;
            if (!/^([0-9]*),([0-9][0-9])/.test($('#amount').val())) {
                $('#amount').addClass(""invalid"");
                $('#amount').prop(""aria-invalid"", ""true"");
                valid = false;
            }
            if ($('#desc').val() == '') {
                $('#desc').addClass(""invalid"");
                $('#desc').prop(""aria-invalid"", ""true"");
                valid = false;
            }
            return valid;
        },
        addTransaction: function () {
            if (Transactions.validate()) {
                M.Modal.getInstance($('#add')).close();
                var amount = Math.round(Number($('#amount').val().replace("","", ""."")) * 100);
                var type = $('#selbij option:selected').val();
                if ($");
            WriteLiteral(@"(""input[name='mode']:checked"").val() == 'min') {
                    amount *= -1;
                    type = $('#selaf option:selected').val();
                }
                $('#_progbar').show();
                $.post('/Accountancy/addTransaction', { Amount: amount, Description: $('#desc').val(), Type: type, When: $('#when').val(), Payed: ($('#isDC').prop('checked')) }, function (r) {
                    if (r.status == 'success') {
                        M.toast({ html: 'Transactie toegevoegd', classes: 'green' });
                        Core.doReload(500);
                    } else {
                        M.toast({ html: r.error, classes: 'red' });
                        $('#_progbar').hide();
                    }
                });
            }
        },
        delete: function (id) {
            $('#_progbar').show();
            $.post('/Accountancy/removeTransaction', { id: id }, function (r) {
                if (r.status == 'success') {
                    M.toast");
            WriteLiteral(@"({ html: 'Transactie verwijderd!', classes: 'green' });
                    Core.doReload(500);
                } else {
                    M.toast({ html: r.error, classes: 'red' });
                    $('#_progbar').hide();
                }
            });
        },
        filter: function () {
            $('#_progbar').show();
            window.location.href = '/Accountancy/Index/' + $('#limit').val();
        }
    }

    $(function () {
        $('.tabs').tabs();
        Transactions.init();
        $('select').formSelect();
        $('.datepicker').datepicker({ format: 'dd-mm-yyyy' });
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.NoDB._Transactions> Html { get; private set; }
    }
}
#pragma warning restore 1591
