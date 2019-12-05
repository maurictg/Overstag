#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1050b02fc7ca23ea9cfc196ff2ba73db8e5b6176"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1050b02fc7ca23ea9cfc196ff2ba73db8e5b6176", @"/Views/Mentor/Accountancy/Index.cshtml")]
    public class Views_Mentor_Accountancy_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Accountancy.Transaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
  
    Layout = "_AccountancyLayout";


    List<string> ddata = new List<string>();
    List<string> mdata = new List<string>();
    int balance = 0;
    int total = (int)ViewBag.Total;
    double _out = Math.Round((double)Model.Where(g => g.Amount < 0).Sum(f => f.Amount)/-100, 2);
    double _in = Math.Round((double)Model.Where(g => g.Amount > 0).Sum(f => f.Amount)/100, 2);


    var results = Model.ToArray().Reverse().GroupBy(a => a.When.Date, b => b.Amount,
        (c, d) => new { When = c, Amount = d.Sum() });

    //Generate line stats
    foreach(var item in results)
    {
        ddata.Add(item.When.ToString("dd-MM-yyyy"));
        balance += item.Amount;
        mdata.Add(Math.Round((double)balance / 100, 2).ToString("F").Replace(",", "."));
    }

    int limit = (ViewBag.Limit != null) ? (int)ViewBag.Limit : 1000;

    Dictionary<int, string> Types = new Dictionary<int, string>()
    {
        //Inkomsten
        {1, "Activiteiten"},
        {2, "Subsidie"},
        {3, "Sponsor (bedrijf)"},
        {4, "Sponsor (particulier)"},
        {5, "Sponsor (kerk)"},
        {6, "Overige"},

        //Uitgaven
        {21, "Boodschappen"},
        {22, "Huur gebouw"},
        {23, "Website"},
        {24, "Activiteiten"},
        {25, "Drukwerk"},
        {26, "Inboedel"},
        {27, "Reiskosten"},
        {28, "Overige" }
    };

#line default
#line hidden
            WriteLiteral(@"
    <div>
        <!--Boekhouding overzicht-->
        <h2 class=""red-text text-darken-4 center-align"">Kasboek</h2>

        <h5 class=""center-align"">Transactieoverzicht</h5>
        <div class=""row"">
            <div class=""col s12"">
                <div class=""col s12"">
                    Toon nieuwste:
                    &nbsp;
                    <div class=""input-field inline"">
                        <input id=""limit"" type=""number"" max=""1000"" style=""width: 5em""");
            BeginWriteAttribute("value", " value=\"", 1938, "\"", 1952, 1);
#line 60 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 1946, limit, 1946, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>&nbsp;
                    <button class=""btn inline red darken-4 waves-effect"" onclick=""Transactions.filter()"">Go</button>
                    &nbsp;
                    <button class=""btn red darken-4 waves-effect modal-trigger"" onclick=""Transactions.updateDD()"" data-target=""add"">Transactie toevoegen</button>
                </div>
            </div>
        </div>
        <table>
            <tr>
                <td class=""grey darken-1 white-text center-align"">Geld in de kas: <b>&euro;");
#line 70 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                                                                      Write(Math.Round((double)total / 100, 2).ToString("F"));

#line default
#line hidden
            WriteLiteral(@"</b></td>
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
#line 84 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                 foreach (var t in Model)
                {

#line default
#line hidden
            WriteLiteral("                    <tr>\r\n                        <td>");
#line 87 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                       Write(Html.Raw((t.Amount > 0) ? $"<b class=\"green-text text-accent-4\">+ &euro;{Math.Round((double)t.Amount / 100, 2).ToString("F")}</b>" : $"<b class=\"red-text\">- &euro;{Math.Round((double)t.Amount / 100, 2).ToString("F").Replace("-", "")}</b>"));

#line default
#line hidden
            WriteLiteral("</td>\r\n                        <td>");
#line 88 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                       Write(t.When.ToString("dd-MM-yyyy"));

#line default
#line hidden
            WriteLiteral(" om ");
#line 88 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                                         Write(t.When.ToString("HH:mm"));

#line default
#line hidden
            WriteLiteral("</td>\r\n                        <td>");
#line 89 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                       Write(t.Description);

#line default
#line hidden
            WriteLiteral("</td>\r\n                        <td>");
#line 90 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                       Write(Html.Raw((t.Type!=null)? Types[Convert.ToInt32(t.Type)] : "-"));

#line default
#line hidden
            WriteLiteral("</td>\r\n                        <td><button class=\"btn-small red waves-effect modal-trigger\" data-target=\"areusure\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3636, "\"", 3680, 3);
            WriteAttributeValue("", 3646, "$(\'#btndelete\').data(\'id\',\'", 3646, 27, true);
#line 91 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
WriteAttributeValue("", 3673, t.Id, 3673, 5, false);

#line default
#line hidden
            WriteAttributeValue("", 3678, "\')", 3678, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Verwijderen</button></td>\r\n                    </tr>\r\n");
#line 93 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                }

#line default
#line hidden
            WriteLiteral(@"            </tbody>
        </table>
        <br />
        <h5 class=""center-align"">Statistieken</h5>
        <div class=""row"">
            <div class=""col s12 m4"">
                <canvas id=""pie"" style=""height: 1em; width: 1em""></canvas>
            </div>
            <div class=""col s12 m4"">
                <canvas id=""stat"" style=""height: 1em; width: 1em""></canvas>
            </div>
        </div>
    </div>

    <div id=""add"" class=""modal"">
        <div class=""modal-content"">
            <h4>Transactie toevoegen</h4>
            <br />
            <div class=""row"">
                <div class=""input-field col s6"">
                    <input id=""amount"" type=""text"" class=""validate"" />
                    <label for=""amount"">Bedrag in €</label>
                    <span class=""helper-text"" data-error=""Vul a.u.b. een geldig bedrag in (kommagetal)""></span>
                </div>
                <div class=""col s6"">
                    <p>
                        <label>
         ");
            WriteLiteral(@"                   <input class=""with-gap"" value=""plus"" checked=""checked"" name=""mode"" type=""radio"" />
                            <span>Bij</span>
                        </label>
                        &nbsp;&nbsp;
                        <label>
                            <input class=""with-gap"" value=""min"" name=""mode"" type=""radio"" />
                            <span>Af</span>
                        </label>
                    </p>
                </div>
                <div class=""input-field col s12"">
                    <input id=""desc"" type=""text"" class=""validate"" />
                    <label for=""desc"">Omschrijving</label>
                    <span class=""helper-text"" data-error=""Vul a.u.b. dit veld in""></span>
                </div>
                <div class=""input-field col s12 m8"">
                    <input id=""when"" type=""text"" class=""datepicker"" />
                    <label for=""when"">Datum</label>
                </div>
                <br /><br />
                <di");
            WriteLiteral(@"v class=""row"">
                    <div class=""input-field col s12 m8"" id=""bij"">
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
                    <div class=""input-field col s12 m8"" id=""af"" style=""display: none;"">
                        <select id=""selaf"">
                            <option value=""21"" selected>Boodschappen</option>
                            <option value=""22"">Huur gebouw</option>
                            <option value=""23"">Website</option");
            WriteLiteral(@">
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
            <button class=""btn waves-effect green"" onclick=""Transactions.addTransaction()"">Toevoegen</button>
        </div>
    </div>

    <div id=""areusure"" class=""modal"">
        <div class=""modal-content"">
            <h4>Weet je zeker dat je deze transactie wilt verwijderen?</h4>
            <p>Dit kan niet ongedaan worden gemaakt!</p>
 ");
            WriteLiteral(@"       </div>
        <div class=""modal-footer"">
            <button class=""modal-close btn green waves-effect"">Annuleren</button>
            <button id=""btndelete"" onclick=""Transactions.delete($('#btndelete').data('id'));"" class=""btn red waves-effect modal-close"">Verwijderen</button>
        </div>
    </div>

<script>
    var Transactions = {
        init: function () {
            $('#_acc, #_macc').addClass('active');
            Transactions.createLineChart();
            Transactions.createPieChart();
            Transactions.mapEvents();
        },
        mapEvents: function () {
            $(""input[name='mode']"").change(function () {
                Transactions.updateDD();
            });
        },
        updateDD: function () {
            //Select dropdown
            if ($(""input[name='mode']:checked"").val() == 'min') {
                $('#bij').hide();
                $('#af').show();
            } else {
                $('#bij').show();
                $('#af').h");
            WriteLiteral("ide();\r\n            }\r\n        },\r\n        createLineChart: function () {\r\n            var data = {\r\n                labels: [");
#line 211 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Inhoud kas\',\r\n                    backgroundColor: \'#B71C1C\',\r\n                    data: [\r\n                        ");
#line 217 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                   Write(Html.Raw(string.Join(',', mdata)));

#line default
#line hidden
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
#line 241 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                      Write(_in.ToString("F").Replace(",", "."));

#line default
#line hidden
            WriteLiteral(", ");
#line 241 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Accountancy\Index.cshtml"
                                                            Write(_out.ToString("F").Replace(",", "."));

#line default
#line hidden
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

            var MVChart = new Chart($('#pie'), {
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
     ");
            WriteLiteral(@"           M.Modal.getInstance($('#add')).close();
                var amount = Math.round(Number($('#amount').val().replace("","", ""."")) * 100);
                var type = $('#selbij option:selected').val();
                if ($(""input[name='mode']:checked"").val() == 'min') {
                    amount *= -1;
                    type = $('#selaf option:selected').val();
                }
                $('#_progbar').show();
                $.post('/Accountancy/addTransaction', { Amount: amount, Description: $('#desc').val(), Type: type, When: $('#when').val() }, function (r) {
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
        delete: function (id)");
            WriteLiteral(@" {
            $('#_progbar').show();
            $.post('/Accountancy/removeTransaction', { id: id }, function (r) {
                if (r.status == 'success') {
                    M.toast({ html: 'Transactie verwijderd!', classes: 'green' });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Accountancy.Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591
