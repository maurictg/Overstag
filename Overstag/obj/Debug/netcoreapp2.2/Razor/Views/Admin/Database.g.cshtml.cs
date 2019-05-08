#pragma checksum "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Database.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "77f57d53f30ebc33f30d69d01f323047bfdba7de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Database), @"mvc.1.0.view", @"/Views/Admin/Database.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Database.cshtml", typeof(AspNetCore.Views_Admin_Database))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"77f57d53f30ebc33f30d69d01f323047bfdba7de", @"/Views/Admin/Database.cshtml")]
    public class Views_Admin_Database : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\MaurICT\Documents\GitHub\Overstag\Overstag\Views\Admin\Database.cshtml"
  
    Layout = "~/Views/_AdminLayout.cshtml";
    ViewBag.Title = "Database";

#line default
#line hidden
            BeginContext(85, 2429, true);
            WriteLiteral(@"<br />
<div class=""center-align white-text"">
    <h3>Database</h3>
    <br />
    <hr />
    <h5>Query uitvoeren</h5>
    <div class=""row"">
        <div class=""col s12 m8 valign-wrapper"">
            <div class=""input-field col s8 m6"">
                <input type=""text"" id=""query"" placeholder=""SQL-Query..."" class=""white-text"" />
            </div>
            <div class=""input-field col s4 m2"">
                <button id=""firequery"" onclick=""Database.postquery();"" class=""btn red white-text waves-effect waves-light"">Uitvoeren</button>
            </div>

        </div>
        <br />
        <div class=""row"">
            <div class=""col s12 m6"">
                <input type=""text"" id=""tablename"" placeholder=""Tabelnaam"" class=""white-text"" />
                <p>Tabelnaam: Accounts, Events of Participate</p>
            </div>
            <div class=""col s12 m6"">
                <input type=""text"" id=""type"" placeholder=""Type"" class=""white-text"" />
                <p>Type: 0: Uitvoer-query, ");
            WriteLiteral(@"1: read single, 2: read list, 3: read array</p>
            </div>
            <div class=""progress"" id=""progbar""><div class=""indeterminate""></div></div>

        </div>
        <br />
    </div>
    <hr />
    <h5>Output</h5>
    <div id=""output"">

    </div>
</div>

<script>
    $('#_database').addClass('active');

    var Database = {
        init: function () {
            $('#progbar').hide();
        },
        postquery: function () {
            $('#progbar').show();
            $.post(""/Admin/Query"", { Query: $('#query').val(), TableName: $('#tablename').val(), Type: $('#type').val() }, function (response) {
                if (response.status == 'success') {
                    M.toast({ html: 'Query successfull', classes: 'green rounded' });
                    console.log(response.data);
                    var output = JSON.stringify(response.data);
                    $('#output').text(output);
                }
                else {
                    //status =");
            WriteLiteral(@" error
                    M.toast({ html: response.error, classes: 'red' });
                    var output = JSON.stringify(response.error);
                    $('#output').text(output);
                }
                }, 'json'
            ).done(function(){
                $('#progbar').hide();
            });
        }
    }

    Database.init();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
