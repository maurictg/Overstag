#pragma checksum "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9758f70cee7bde16bdace46bb284ef56e5b75654"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9758f70cee7bde16bdace46bb284ef56e5b75654", @"/Views/Home/Index.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/logo.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("grey darken-2 responsive-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("grey darken-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\maurict\source\repos\Overstag\Overstag\Views\Home\Index.cshtml"
  
    ViewBag.Title = "Overstag - Welkom!";

#line default
#line hidden
            BeginContext(50, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(56, 3512, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fb02aaf0ab9c44d1af0981881b037b26", async() => {
                BeginContext(84, 170, true);
                WriteLiteral("\r\n        <div class=\"slider\">\r\n            <ul class=\"slides\">\r\n                <!-- Voeg meer li\'s toe voor een slideshow-->\r\n                <li>\r\n                    ");
                EndContext();
                BeginContext(254, 63, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "6aa0ecd1072c484dbdc7ff1d658c927f", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(317, 3244, true);
                WriteLiteral(@"
                    <div class=""caption center-align white-text"">
                        <h2><b>Welkom bij Overstag!</b></h2>
                        <h5 class=""light grey-text text-lighten-3"">Welkom op onze website van stichting Overstag!</h5>
                    </div>
                </li>
            </ul>
        </div>
        <div class=""white-text center-align flow-text"">
            <h4 class=""blue-middle-text""><b>Wat wij doen</b></h4>
            <p>
                Wij organiseren voor christelijke jongeren in Rilland en omgeving elke week een chill of activiteitenavond.<br />
                We vinden het belangrijk dat christelijke jongeren uit Rilland en omgeving elkaar ontmoeten<br />en het samen gezellig hebben met elkaar.
            </p>
        </div>
        <br />
        <div class=""white-text center-align grey darken-2"">
            <br />
            <div class=""flow-text"">
                <h4 class=""blue-middle-text""><b>Onze activiteiten</b></h4>
            </di");
                WriteLiteral(@"v>
            <div class=""row"">
                <div class=""col s12 m6"">
                    <div class=""card blue-dark"">
                        <div class=""card-content white-text"">
                            <span class=""card-title""><b>Hoe verloopt een chill-avond?</b></span>
                            <h6>
                                De kist is open vanaf 20.00 tot 23.00 uur ’s avonds. Je kunt dan gezellig op de bank hangen, lekker kletsen of een activiteit doen met andere jongeren.  Tussendoor krijg je wat te drinken en zorgen we voor wat lekkers op tafel. We beschikken in de kist over &quot;free wifi&quot; waar je ook gebruik van kan maken.  Ook hebben we een zingkwartiertje en zingen we (voor degenen die dat willen) wat liederen met elkaar. Aan het einde van de avond sluiten we af met bitterballen of minisnacks.
                            </h6>
                        </div>
                    </div>
                </div>
                <div class=""col s12 m6"">
                 ");
                WriteLiteral(@"   <div class=""card blue-dark"">
                        <div class=""card-content white-text"">
                            <span class=""card-title""><b>Hoe verloopt een activiteitenavond?</b></span>
                            <h6>
                                We verzamelen op een centraal punt in Rilland en vertrekken dan (meestal) met de auto naar de plek die is afgesproken.
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <div class=""white-text center-align flow-text"">
            <h4 class=""blue-middle-text""><b>Over ons</b></h4>
            <p>Voor meer informatie over onze stichting,<br />doelstelling en het opnemen van contact kunt u vinden op de pagina <a href=""/Home/About"">Over ons</a></p>
        </div>
        <br />
        <div class=""white-text grey darken-2 center-align flow-text"">
            <br />
            <h4 class=""blue-middle-text""><b>Contact");
                WriteLiteral("</b></h4>\r\n            <p>Bent u geintresseerd of heeft u nog vragen, neem dan <a href=\"/Home/Contact\">contact</a> met ons op.</p>\r\n            <br />\r\n        </div>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3568, 722, true);
            WriteLiteral(@"

<script>
    Home = {
        init: function () {
            //Maak knopje active
            $(""#_home"").addClass(""active"");
            $(""#_mhome"").addClass(""active"");

            //When user is redirected from login, toast message once
            /*if(localStorage.getItem(""login"")){
                M.toast({ html: 'Successvol ingelogd!', classes: 'green' });
                localStorage.removeItem(""login"");
            }*/
            if(localStorage.getItem(""logout"")){
                M.toast({ html: 'Successvol uitgelogd!', classes: 'orange' });
                localStorage.removeItem(""logout"");
            }
        },


    }
    //call init function
    Home.init();
</script>");
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
