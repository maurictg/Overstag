#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5057bc143554a38c3e61e424ad27d7504150879a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Mentor_Stats), @"mvc.1.0.view", @"/Views/Mentor/Stats.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5057bc143554a38c3e61e424ad27d7504150879a", @"/Views/Mentor/Stats.cshtml")]
    public class Views_Mentor_Stats : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Overstag.Models.NoDB.SSubEvent>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
  
    Layout = "_MentorLayout";
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
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
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
        e.Sub.RemoveAll(f => f.account.Type != 0);
        if (e.Sub.Count() > 0)
        {
            aadata.Add(Math.Round(e.Sub.Select(f => Overstag.Core.General.getAge(f.account.Birthdate)).Average(),1).ToString().Replace(",","."));
        }

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
#nullable disable
            WriteLiteral(@"
<div>
    <div class=""row"">
        <h2 class=""blue-middle-text center-align"">Statistieken</h2>
        <hr />

        <div class=""col s6 m3"">
            <div class=""card-panel center-align"">
                <span class=""flow-text"">Aantal activiteiten afgelopen</span>
                <h3><b class=""black-text"">");
#nullable restore
#line 76 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                     Write(after.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h3>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"col s6 m3\">\r\n            <div class=\"card-panel center-align\">\r\n                <span class=\"flow-text\">Aantal deelnemers geweest</span>\r\n                <h3><b class=\"blue-text\">");
#nullable restore
#line 83 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                    Write(tm);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> m & <b class=\"red-text\">");
#nullable restore
#line 83 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                    Write(tv);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> v</h3>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"col s6 m3\">\r\n            <div class=\"card-panel center-align\">\r\n                <span class=\"flow-text\">Aantal komende evenementen</span>\r\n                <h3><b class=\"green-text\">");
#nullable restore
#line 90 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                     Write(upcoming.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></h3>
            </div>
        </div>

        <div class=""col s6 m3"">
            <div class=""card-panel center-align"">
                <span class=""flow-text"">Aantal deelnemers al ingeschreven</span>
                <h3><b class=""grey-text"">");
#nullable restore
#line 97 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                    Write(tt);

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 138 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
             foreach (var e in upcoming.OrderBy(f => f.Event.When))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>\r\n                    <div class=\"collapsible-header\"><i class=\"material-icons\">event</i><b>");
#nullable restore
#line 141 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                     Write(e.Event.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> &nbsp;&nbsp;");
#nullable restore
#line 141 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                    Write(e.Event.When.ToString("dd-MM-yyyy HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <div class=\"collapsible-body\">\r\n                        <h6><b>Omschrijving:</b> ");
#nullable restore
#line 143 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                            Write(e.Event.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                        <h6>Totaal aantal deelnemers: <b>");
#nullable restore
#line 144 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                    Write(e.Sub.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" (<span class=\"blue-text\">");
#nullable restore
#line 144 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                            Write(e.Sub.Where(f => f.account.Sex == 0).Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> / <span class=\"red-text\">");
#nullable restore
#line 144 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                                                                          Write(e.Sub.Where(f => f.account.Sex == 1).Count());

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 156 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                 foreach (var a in e.Sub.Where(f => f.account.Type < 3))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 159 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 160 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 161 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Overstag.Core.General.getAge(a.account.Birthdate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 162 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.account.Sex == 0) ? "<b class=\"blue-text\">Man</b>" : "<b class=\"red-text\">Vrouw</b>"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 163 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.part.FriendCount > 0) ? "Ja, <b class=\"green-text\">" + a.part.FriendCount + "</b>" : "Nee"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 165 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </li>\r\n");
#nullable restore
#line 170 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n\r\n        <h4>Afgelopen activiteiten</h4>\r\n        <ul class=\"collapsible\">\r\n");
#nullable restore
#line 175 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
             foreach (var e in after.OrderBy(f => f.Event.When).ToArray().Reverse())
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>\r\n                    <div class=\"collapsible-header\"><i class=\"material-icons\">event</i><b>");
#nullable restore
#line 178 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                     Write(e.Event.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> &nbsp;&nbsp;");
#nullable restore
#line 178 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                    Write(e.Event.When.ToString("dd-MM-yyyy HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <div class=\"collapsible-body\">\r\n                        <h6><b>Omschrijving:</b> ");
#nullable restore
#line 180 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                            Write(e.Event.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                        <h6>Totaal aantal deelnemers: <b>");
#nullable restore
#line 181 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                    Write(e.Sub.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" (<span class=\"blue-text\">");
#nullable restore
#line 181 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                            Write(e.Sub.Where(f => f.account.Sex == 0).Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> / <span class=\"red-text\">");
#nullable restore
#line 181 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                                                                                                                                                          Write(e.Sub.Where(f => f.account.Sex == 1).Count());

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 193 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                 foreach (var a in e.Sub)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 196 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 197 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(a.account.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 198 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Overstag.Core.General.getAge(a.account.Birthdate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 199 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.account.Sex == 0) ? "<b class=\"blue-text\">Man</b>" : "<b class=\"red-text\">Vrouw</b>"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 200 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                       Write(Html.Raw((a.part.FriendCount > 0) ? "Ja, <b class=\"green-text\">" + a.part.FriendCount + "</b>" : "Nee"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 202 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </li>\r\n");
#nullable restore
#line 207 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
            }

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 224 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(gm);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            var v = ");
#nullable restore
#line 225 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(gv);

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 248 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(tm);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            var v = ");
#nullable restore
#line 249 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
               Write(tv);

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 273 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
#nullable disable
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Mannen\',\r\n                    backgroundColor: \'rgb(75, 133, 227)\',\r\n                    data: [\r\n                        ");
#nullable restore
#line 279 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', mdata)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ]\r\n                },\r\n                {\r\n                    label: \'Vrouwen\',\r\n                    backgroundColor: \'rgb(227, 75, 141)\',\r\n                    data: [\r\n                        ");
#nullable restore
#line 286 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', vdata)));

#line default
#line hidden
#nullable disable
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
#nullable restore
#line 318 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
#nullable disable
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Aantal deelnemers\',\r\n                    backgroundColor: \'rgba(41, 158, 41, 0.5)\',\r\n                    data: [\r\n                        ");
#nullable restore
#line 324 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', tdata)));

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

            var tc = new Chart($('#trendchart'), {
                type: 'line',
                data: data,
                options: options
            });
        },
        createAChart: function () {
            var data = {
                labels: [");
#nullable restore
#line 347 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                    Write(Html.Raw("'"+string.Join(',', ddata).Replace(",","','")+"'"));

#line default
#line hidden
#nullable disable
            WriteLiteral("],\r\n                datasets: [\r\n                {\r\n                    label: \'Leeftijd\',\r\n                    backgroundColor: \'rgba(50, 137, 168, 0.5)\',\r\n                    data: [\r\n                        ");
#nullable restore
#line 353 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Mentor\Stats.cshtml"
                   Write(Html.Raw(string.Join(',', aadata)));

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
