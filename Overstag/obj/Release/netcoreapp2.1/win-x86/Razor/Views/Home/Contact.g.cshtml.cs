#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "16ba7917ba1f561934f3e76a285891db4f84916c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Contact), @"mvc.1.0.view", @"/Views/Home/Contact.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Contact.cshtml", typeof(AspNetCore.Views_Home_Contact))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"16ba7917ba1f561934f3e76a285891db4f84916c", @"/Views/Home/Contact.cshtml")]
    public class Views_Home_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("grey darken-3 white-text"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Contact.cshtml"
  
    ViewBag.Title = "Contact";

#line default
#line hidden
            BeginContext(39, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(41, 2320, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "037db14c7c754c5ab7febe6c7b49abf9", async() => {
                BeginContext(80, 522, true);
                WriteLiteral(@"
    <div class=""col s10 offset-s1 center-align"">
        <h2 class=""blue-light-text"">Contact</h2>
        <br />
        <h4 class=""blue-middle-text""><b>Contact openemen</b></h4>
        <h5 class=""blue-light-text"">Schriftelijk</h5>
        <p class=""flow-text"">Voor schriftelijke correspondentie kan het adres van de secretaris gebruikt worden.</p>
        <br />
        <h5 class=""blue-light-text"">Email</h5>
        <p class=""flow-text"">Stuur een mail naar: <a href=""mailto:secretaris@overstag.nl"">secretaris");
                EndContext();
                BeginContext(603, 1751, true);
                WriteLiteral(@"@overstag.nl</a></p>
        <br />
        <h5 class=""blue-light-text"">Adres chillavonden:</h5>
        <p class=""flow-text"">
            <i>
                De Kist<br />
                Cromvlietstraat 30<br />
                4411 AE Rilland
            </i>
        </p>
        <br />

        <h4 class=""blue-middle-text""><b>Stichtingsbestuur</b></h4>
        <p>Het stichtingsbestuur bestaat uit 5 leden.</p>
        <table class=""responsive-table highlight"">
            <thead>
                <tr>
                    <th>Naam</th>
                    <th>Functie</th>
                    <th>Telefoonnummer</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Marien Golverdingen</td>
                    <td>Voorzitter</td>
                    <td>06-13606432</td>
                </tr>
                <tr>
                    <td>Lydia de Voogd</td>
                    <td>Penningmeesteres</td>
                    <t");
                WriteLiteral(@"d>06-16117714</td>
                </tr>
                <tr>
                    <td>Johanna Dekker</td>
                    <td>Algemeen bestuurslid</td>
                    <td>06-21624483</td>
                </tr>
                <tr>
                    <td>Bas Westsrate</td>
                    <td>Algemeen bestuurslid</td>
                    <td>06-86812790</td>
                </tr>
                <tr>
                    <td><b>--VACATURE--</b></td>
                    <td>Secretaris</td>
                    <td><b>--VACATURE--</b></td>
                </tr>
            </tbody>
        </table>
        <!--Foto bestuur-->
        <!--Voorstelverhaal-->

        <br />

    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2361, 142, true);
            WriteLiteral("\r\n\r\n<script>\r\n    $(function () {\r\n        $(\'#_contact\').addClass(\'active\');\r\n        $(\'#_mcontact\').addClass(\'active\');\r\n    });\r\n</script>");
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
