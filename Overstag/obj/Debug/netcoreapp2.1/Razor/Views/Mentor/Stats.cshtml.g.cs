#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13411d445309e34c09f3d321bb0926c7daf46f12"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Stats), @"mvc.1.0.view", @"/Views/Mentor/Stats.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Mentor/Stats.cshtml", typeof(AspNetCore.Views_Mentor_Stats))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"13411d445309e34c09f3d321bb0926c7daf46f12", @"/Views/Mentor/Stats.cshtml")]
    public class Views_Mentor_Stats : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.NoDB.SSubEvent>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
  
    Layout = "~/Views/_MentorLayout.cshtml";
    ViewBag.Title = "Statistieken";

    var after = new List<Overstag.Models.NoDB.SSubEvent>();
    var upcoming = new List<Overstag.Models.NoDB.SSubEvent>();

    foreach (var sse in Model)
    {
        if (sse.Event.When.Date >= DateTime.Now.Date)
        {
            upcoming.Add(sse);
        }
        else
        {
            after.Add(sse);
        }
    }

#line default
#line hidden
            BeginContext(484, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 22 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
  
    //DATA ALGORITHM FOR CHARTS AND STATS
    int tm = 0;
    int tv = 0;
    int tt = 0;
    int gm = 0;
    int gv = 0;

    List<int> mdata = new List<int>(); //man data (count)
    List<int> vdata = new List<int>(); //vrouw data (count)
    List<int> tdata = new List<int>(); //totaal data (count)
    List<string> aadata = new List<string>(); //age data (avg)
    List<string> ddata = new List<string>(); //dates

    foreach (var e in after)
    {
        aadata.Add(Math.Round(e.Sub.Select(f => Overstag.Core.General.getAge(f.account.Birthdate)).Average(),1).ToString().Replace(",","."));

        int ttm = e.Sub.Where(f => f.account.Sex == 0).Count();
        int ttv = e.Sub.Where(f => f.account.Sex == 1).Count();
        tm += ttm;
        tv += ttv;
        mdata.Add(ttm);
        vdata.Add(ttv);
        tdata.Add(ttm + ttv);
        ddata.Add(e.Event.When.ToString("dd-MM-yyyy"));
    }

    foreach (var u in upcoming)
    {
        tt += u.Sub.Count();
    }

    try
    {
        gm = Convert.ToInt32(Math.Round(((double)tm / after.Count()), MidpointRounding.AwayFromZero));
        gv = Convert.ToInt32(Math.Round(((double)tv / after.Count()), MidpointRounding.AwayFromZero));
    }
    catch { }


#line default
#line hidden
            BeginContext(1751, 323, true);
            WriteLiteral(@"
<div>
    <div class=""row"">
        <h2 class=""blue-middle-text center-align"">Statistieken</h2>
        <hr />

        <div class=""col s6 m3"">
            <div class=""card-panel center-align"">
                <span class=""flow-text"">Aantal activiteiten afgelopen</span>
                <h3><b class=""black-text"">");
            EndContext();
            BeginContext(2075, 13, false);
#line 72 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                     Write(after.Count());

#line default
#line hidden
            EndContext();
            BeginContext(2088, 248, true);
            WriteLiteral("</b></h3>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"col s6 m3\">\r\n            <div class=\"card-panel center-align\">\r\n                <span class=\"flow-text\">Aantal deelnemers geweest</span>\r\n                <h3><b class=\"blue-text\">");
            EndContext();
            BeginContext(2337, 2, false);
#line 79 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                    Write(tm);

#line default
#line hidden
            EndContext();
            BeginContext(2339, 29, true);
            WriteLiteral("</b> m & <b class=\"red-text\">");
            EndContext();
            BeginContext(2369, 2, false);
#line 79 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                    Write(tv);

#line default
#line hidden
            EndContext();
            BeginContext(2371, 252, true);
            WriteLiteral("</b> v</h3>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"col s6 m3\">\r\n            <div class=\"card-panel center-align\">\r\n                <span class=\"flow-text\">Aantal komende evenementen</span>\r\n                <h3><b class=\"green-text\">");
            EndContext();
            BeginContext(2624, 16, false);
#line 86 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                     Write(upcoming.Count());

#line default
#line hidden
            EndContext();
            BeginContext(2640, 256, true);
            WriteLiteral(@"</b></h3>
            </div>
        </div>

        <div class=""col s6 m3"">
            <div class=""card-panel center-align"">
                <span class=""flow-text"">Aantal deelnemers al ingeschreven</span>
                <h3><b class=""grey-text"">");
            EndContext();
            BeginContext(2897, 2, false);
#line 93 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                    Write(tt);

#line default
#line hidden
            EndContext();
            BeginContext(2899, 1685, true);
            WriteLiteral(@"</b></h3>
            </div>
        </div>

        <div class=""row"">
            <div class=""col s12 m6"">
                <div class=""row"">
                    <div class=""card-panel col s12 m6"">
                        <h5 class=""center-align"">Gemiddelde aantal m/v</h5>
                        <canvas id=""gmvchart"" style=""height: 1em; width: 1em""></canvas>
                    </div>
                    <div class=""card-panel col s12 m6"">
                        <h5 class=""center-align"">Totaal aantal m/v</h5>
                        <canvas id=""tmvchart"" style=""height: 1em; width: 1em""></canvas>
                    </div>
                    <div class=""card-panel col s12 m6"">
                        <h5 class=""center-align"">Trend deelnemers</h5>
                        <canvas id=""trendchart"" style=""height: 1em; width: 1em""></canvas>
                    </div>
                    <div class=""card-panel col s12 m6"">
                        <h5 class=""center-align"">Gemiddelde leeftijd</h5");
            WriteLiteral(@">
                        <canvas id=""agechart"" style=""height: 1em; width: 1em""></canvas>
                    </div>
                </div>
            </div>
            <div class=""col s12 m6"">
                <div class=""row"">
                    <div class=""card-panel col s12"">
                        <canvas id=""mvchart"" style=""height: 1em; width: 1em""></canvas>
                    </div>
                </div>
            </div>
        </div>

        <hr />

        <h2 class=""blue-middle-text center-align"" id=""inschrijvingen"">Inschrijvingen</h2>

        <h4>Eerstvolgende activiteiten</h4>

        <ul class=""collapsible"">
");
            EndContext();
#line 134 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
             foreach (var e in upcoming.OrderBy(f => f.Event.When))
            {

#line default
#line hidden
            BeginContext(4668, 112, true);
            WriteLiteral("                <li>\r\n                    <div class=\"collapsible-header\"><i class=\"material-icons\">event</i><b>");
            EndContext();
            BeginContext(4781, 13, false);
#line 137 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                     Write(e.Event.Title);

#line default
#line hidden
            EndContext();
            BeginContext(4794, 17, true);
            WriteLiteral("</b> &nbsp;&nbsp;");
            EndContext();
            BeginContext(4812, 41, false);
#line 137 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                    Write(e.Event.When.ToString("dd-MM-yyyy HH:mm"));

#line default
#line hidden
            EndContext();
            BeginContext(4853, 109, true);
            WriteLiteral("</div>\r\n                    <div class=\"collapsible-body\">\r\n                        <h6><b>Omschrijving:</b> ");
            EndContext();
            BeginContext(4963, 19, false);
#line 139 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                            Write(e.Event.Description);

#line default
#line hidden
            EndContext();
            BeginContext(4982, 64, true);
            WriteLiteral("</h6>\r\n                        <h6>Totaal aantal deelnemers: <b>");
            EndContext();
            BeginContext(5047, 13, false);
#line 140 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                    Write(e.Sub.Count());

#line default
#line hidden
            EndContext();
            BeginContext(5060, 26, true);
            WriteLiteral(" (<span class=\"blue-text\">");
            EndContext();
            BeginContext(5087, 44, false);
#line 140 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                            Write(e.Sub.Where(f => f.account.Sex == 0).Count());

#line default
#line hidden
            EndContext();
            BeginContext(5131, 33, true);
            WriteLiteral("</span> / <span class=\"red-text\">");
            EndContext();
            BeginContext(5165, 44, false);
#line 140 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                                                                          Write(e.Sub.Where(f => f.account.Sex == 1).Count());

#line default
#line hidden
            EndContext();
            BeginContext(5209, 549, true);
            WriteLiteral(@"</span>)</b></h6>
                        <table class=""responsive-table"">
                            <thead>
                                <tr>
                                    <th>Naam</th>
                                    <th>Achternaam</th>
                                    <th>Leeftijd</th>
                                    <th>Geslacht</th>
                                    <th>Vriend(inn)en mee?</th>
                                </tr>
                            </thead>
                            <tbody>
");
            EndContext();
#line 152 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                 foreach (var a in e.Sub)
                                {

#line default
#line hidden
            BeginContext(5852, 86, true);
            WriteLiteral("                                    <tr>\r\n                                        <td>");
            EndContext();
            BeginContext(5939, 19, false);
#line 155 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(5958, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(6010, 18, false);
#line 156 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(6028, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(6080, 49, false);
#line 157 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Overstag.Core.General.getAge(a.account.Birthdate));

#line default
#line hidden
            EndContext();
            BeginContext(6129, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(6181, 101, false);
#line 158 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.account.Sex == 0) ? "<b class=\"blue-text\">Man</b>" : "<b class=\"red-text\">Vrouw</b>"));

#line default
#line hidden
            EndContext();
            BeginContext(6282, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(6334, 105, false);
#line 159 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.part.FriendCount > 0) ? "Ja, <b class=\"green-text\">" + a.part.FriendCount + "</b>" : "Nee"));

#line default
#line hidden
            EndContext();
            BeginContext(6439, 50, true);
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
            EndContext();
#line 161 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                }

#line default
#line hidden
            BeginContext(6524, 123, true);
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </li>\r\n");
            EndContext();
#line 166 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
            }

#line default
#line hidden
            BeginContext(6662, 92, true);
            WriteLiteral("        </ul>\r\n\r\n        <h4>Afgelopen activiteiten</h4>\r\n        <ul class=\"collapsible\">\r\n");
            EndContext();
#line 171 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
             foreach (var e in after.OrderBy(f => f.Event.When).ToArray().Reverse())
            {

#line default
#line hidden
            BeginContext(6855, 112, true);
            WriteLiteral("                <li>\r\n                    <div class=\"collapsible-header\"><i class=\"material-icons\">event</i><b>");
            EndContext();
            BeginContext(6968, 13, false);
#line 174 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                     Write(e.Event.Title);

#line default
#line hidden
            EndContext();
            BeginContext(6981, 17, true);
            WriteLiteral("</b> &nbsp;&nbsp;");
            EndContext();
            BeginContext(6999, 41, false);
#line 174 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                    Write(e.Event.When.ToString("dd-MM-yyyy HH:mm"));

#line default
#line hidden
            EndContext();
            BeginContext(7040, 109, true);
            WriteLiteral("</div>\r\n                    <div class=\"collapsible-body\">\r\n                        <h6><b>Omschrijving:</b> ");
            EndContext();
            BeginContext(7150, 19, false);
#line 176 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                            Write(e.Event.Description);

#line default
#line hidden
            EndContext();
            BeginContext(7169, 64, true);
            WriteLiteral("</h6>\r\n                        <h6>Totaal aantal deelnemers: <b>");
            EndContext();
            BeginContext(7234, 13, false);
#line 177 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                    Write(e.Sub.Count());

#line default
#line hidden
            EndContext();
            BeginContext(7247, 26, true);
            WriteLiteral(" (<span class=\"blue-text\">");
            EndContext();
            BeginContext(7274, 44, false);
#line 177 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                            Write(e.Sub.Where(f => f.account.Sex == 0).Count());

#line default
#line hidden
            EndContext();
            BeginContext(7318, 33, true);
            WriteLiteral("</span> / <span class=\"red-text\">");
            EndContext();
            BeginContext(7352, 44, false);
#line 177 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                                                                          Write(e.Sub.Where(f => f.account.Sex == 1).Count());

#line default
#line hidden
            EndContext();
            BeginContext(7396, 549, true);
            WriteLiteral(@"</span>)</b></h6>
                        <table class=""responsive-table"">
                            <thead>
                                <tr>
                                    <th>Naam</th>
                                    <th>Achternaam</th>
                                    <th>Leeftijd</th>
                                    <th>Geslacht</th>
                                    <th>Vriend(inn)en mee?</th>
                                </tr>
                            </thead>
                            <tbody>
");
            EndContext();
#line 189 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                 foreach (var a in e.Sub)
                                {

#line default
#line hidden
            BeginContext(8039, 86, true);
            WriteLiteral("                                    <tr>\r\n                                        <td>");
            EndContext();
            BeginContext(8126, 19, false);
#line 192 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(8145, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(8197, 18, false);
#line 193 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Lastname);

#line default
#line hidden
            EndContext();
            BeginContext(8215, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(8267, 49, false);
#line 194 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Overstag.Core.General.getAge(a.account.Birthdate));

#line default
#line hidden
            EndContext();
            BeginContext(8316, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(8368, 101, false);
#line 195 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.account.Sex == 0) ? "<b class=\"blue-text\">Man</b>" : "<b class=\"red-text\">Vrouw</b>"));

#line default
#line hidden
            EndContext();
            BeginContext(8469, 51, true);
            WriteLiteral("</td>\r\n                                        <td>");
            EndContext();
            BeginContext(8521, 105, false);
#line 196 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.part.FriendCount > 0) ? "Ja, <b class=\"green-text\">" + a.part.FriendCount + "</b>" : "Nee"));

#line default
#line hidden
            EndContext();
            BeginContext(8626, 50, true);
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
            EndContext();
#line 198 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                }

#line default
#line hidden
            BeginContext(8711, 123, true);
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </li>\r\n");
            EndContext();
#line 203 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
            }

#line default
#line hidden
            BeginContext(8849, 403, true);
            WriteLiteral(@"        </ul>
    </div>


</div>
<script>
    var Stats = {
        init: function () {
            $('#_stats, #_mstats').addClass('active');
            Stats.creategMVChart();
            Stats.createtMVChart();
            Stats.createTChart();
            Stats.createEChart();
            Stats.createAChart();
        },
        creategMVChart: function () {
            var m = ");
            EndContext();
            BeginContext(9253, 2, false);
#line 220 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(gm);

#line default
#line hidden
            EndContext();
            BeginContext(9255, 23, true);
            WriteLiteral(";\r\n            var v = ");
            EndContext();
            BeginContext(9279, 2, false);
#line 221 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(gv);

#line default
#line hidden
            EndContext();
            BeginContext(9281, 614, true);
            WriteLiteral(@";

            var data = {
                datasets: [{
                    data: [m, v],
                    backgroundColor: [
                        'rgb(75, 133, 227)',
                        'rgb(227, 75, 141)'
                    ],
                }],

                labels: [
                    m+ ' mannen',
                    v+ ' vrouwen'
                ]
            };

            var MVChart = new Chart($('#gmvchart'), {
                type: 'doughnut',
                data: data
            });
        },
        createtMVChart: function () {
            var m = ");
            EndContext();
            BeginContext(9896, 2, false);
#line 244 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(tm);

#line default
#line hidden
            EndContext();
            BeginContext(9898, 23, true);
            WriteLiteral(";\r\n            var v = ");
            EndContext();
            BeginContext(9922, 2, false);
#line 245 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(tv);

#line default
#line hidden
            EndContext();
            BeginContext(9924, 643, true);
            WriteLiteral(@";

            var data = {
                datasets: [{
                    data: [m, v],
                    backgroundColor: [
                        'rgb(75, 133, 227)',
                        'rgb(227, 75, 141)'
                    ],
                }],

                labels: [
                    m+ ' mannen',
                    v+ ' vrouwen'
                ]
            };

            var MVChart = new Chart($('#tmvchart'), {
                type: 'doughnut',
                data: data
            });
        },
        createEChart: function () {
            var data = {
                labels: [");
            EndContext();
            BeginContext(10568, 60, false);
#line 269 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
            EndContext();
            BeginContext(10628, 202, true);
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Mannen\',\r\n                    backgroundColor: \'rgb(75, 133, 227)\',\r\n                    data: [\r\n                        ");
            EndContext();
            BeginContext(10831, 33, false);
#line 275 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', mdata)));

#line default
#line hidden
            EndContext();
            BeginContext(10864, 215, true);
            WriteLiteral("\r\n                    ]\r\n                },\r\n                {\r\n                    label: \'Vrouwen\',\r\n                    backgroundColor: \'rgb(227, 75, 141)\',\r\n                    data: [\r\n                        ");
            EndContext();
            BeginContext(11080, 33, false);
#line 282 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', vdata)));

#line default
#line hidden
            EndContext();
            BeginContext(11113, 598, true);
            WriteLiteral(@"
                    ]
                }]

            };

            var chart = new Chart($('#mvchart'), {
				type: 'bar',
				data: data,
				options: {
					title: {
						display: true,
						text: 'Mannen/vrouwen afgelopen avonden'
					},
					tooltips: {
						mode: 'index',
						intersect: false
					},
					responsive: true,
					scales: {
						xAxes: [{
							stacked: true,
						}],
						yAxes: [{
							stacked: true
						}]
					}
				}
			});
        },
        createTChart: function () {
            var data = {
                labels: [");
            EndContext();
            BeginContext(11712, 60, false);
#line 314 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
            EndContext();
            BeginContext(11772, 218, true);
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Aantal deelnemers\',\r\n                    backgroundColor: \'rgba(41, 158, 41, 0.5)\',\r\n                    data: [\r\n                        ");
            EndContext();
            BeginContext(11991, 33, false);
#line 320 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', tdata)));

#line default
#line hidden
            EndContext();
            BeginContext(12024, 582, true);
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

            var tc = new Chart($('#trendchart'), {
                type: 'line',
                data: data,
                options: options
            });
        },
        createAChart: function () {
            var data = {
                labels: [");
            EndContext();
            BeginContext(12607, 60, false);
#line 343 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
            EndContext();
            BeginContext(12667, 210, true);
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Leeftijd\',\r\n                    backgroundColor: \'rgba(50, 137, 168, 0.5)\',\r\n                    data: [\r\n                        ");
            EndContext();
            BeginContext(12878, 34, false);
#line 349 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', aadata)));

#line default
#line hidden
            EndContext();
            BeginContext(12912, 625, true);
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

            var tc = new Chart($('#agechart'), {
                type: 'line',
                data: data,
                options: options
            });
        }
    };

    $(document).ready(function () {
        $('.collapsible').collapsible();
        Stats.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Overstag.Models.NoDB.SSubEvent>> Html { get; private set; }
    }
}
#pragma warning restore 1591
