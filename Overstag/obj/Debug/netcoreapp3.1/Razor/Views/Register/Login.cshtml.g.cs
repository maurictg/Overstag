#pragma checksum "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "554c464b74a7e9b3721b94f64c2a70c89873816e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Register_Login), @"mvc.1.0.view", @"/Views/Register/Login.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"554c464b74a7e9b3721b94f64c2a70c89873816e", @"/Views/Register/Login.cshtml")]
    public class Views_Register_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<string>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("blue-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 100%; vertical-align: middle;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
#nullable restore
#line 2 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml"
  
    ViewBag.Title = "Inloggen bij Overstag";
    ViewBag.Description = "Log in met jouw Overstag Account om je in te schrijven voor onze avonden en om te kunnen stemmen.";
    ViewBag.Keywords = "stichting overstag, overstag rilland, overstag reimerswaal, overstag inloggen, meedoen";
    ViewBag.Canonical = "https://stoverstag.nl/inloggen";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "554c464b74a7e9b3721b94f64c2a70c89873816e3755", async() => {
                WriteLiteral(@"
    <div class=""row"">
        <div class=""row""></div>
        <h2 class=""white-text center-align"" id=""lblmoment"">Momentje...</h2>
        <div class=""col s12 m8 offset-m2 l6 offset-l3"">
            <div class=""card rounded"" id=""logincard"" style=""display: none"">
                <div class=""card-content"">
                    <h3 class=""center-align blue-middle-text"">Inloggen</h3>
                    <div class=""row"">
                        <div class=""input-field col s12"">
                            <i class=""material-icons prefix"">mail_outline</i>
                            <input id=""Username"" class=""checki bigger-input"" type=""text"">
                            <label for=""Username"" id=""lUsername"">Gebruikersnaam/email</label>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""input-field col s12"">
                            <i class=""material-icons prefix"">lock_outline</i>
                            <input");
                WriteLiteral(@" id=""Password"" class=""checki bigger-input"" type=""password"">
                            <label for=""password"" id=""lPassword"">Wachtwoord</label>
                        </div>
                    </div>
                    <div class=""row"">
                        <a class=""btn btn-large blue-light col s12 waves-effect disabled rounded"" id=""btnlogin"">Inloggen</a>
                    </div>
                    <div class=""row"">
                        <div class=""input-field col s6"">
                            <p class=""margin left-align""><a href=""/Register/Register""><b>Geen account?</b></a></p>
                        </div>
                        <div class=""input-field col s6"">
                            <p class=""margin right-align""><a class=""modal-trigger"" href=""#memailreset""><b>Wachtwoord vergeten?</b></a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <!-- Modal Structure -->
    <div id=""me");
                WriteLiteral(@"mailreset"" class=""modal"">
        <div class=""modal-content"">
            <h4>Wachtwoord reset aanvragen</h4>
            <p>Voer hieronder het emailadres in dat gekoppeld is aan uw account</p><br />
            <div class=""input-field"">
                <i class=""material-icons prefix"">email</i>
                <input type=""email"" class=""validate"" id=""tbrmail"" placeholder=""Emailadres"" />
                <label for=""tbrmail"">Emailadres</label>
                <span class=""helper-text"" data-error=""Vul a.u.b. een geldig emailadres in""></span>
                <p class=""red-text"" id=""mailhelper"">Vul a.u.b. alle velden in</p>
                <div class=""progress"" id=""progbar2""><div class=""indeterminate""></div></div>
            </div>
        </div>
        <div class=""modal-footer"">
            <a href=""#!"" class=""modal-close waves-effect waves-red btn-flat"">Annuleren</a>
            <a href=""#!"" id=""sendmailreset"" class=""waves-effect waves-green btn-flat"">Verzenden</a>
        </div>
    </div>
");
                WriteLiteral(@"
    <!-- Modal Structure -->
    <div id=""tfa"" class=""modal"">
        <div class=""modal-content"">
            <div class=""row"">
                <div class=""center-align"">
                    <h4>Tweetrapsvertificatie (2FA)</h4>
                    <h5>Open uw 2FA app, selecteer het account en type de code in:</h5>
                    <div class=""row"">
                        <div class=""input-field col m4 s12 offset-m4"">
                            <input type=""number"" style=""font-size: xx-large"" maxlength=""6"" data-length=""6"" id=""2facode"" placeholder=""000000"" />
                        </div>
                    </div>
                    <a href=""#!"" class=""modal-close waves-effect orange btn modal-trigger"" data-target=""tfarestore"" onclick=""Login.cancel2fa();"">Code vergeten</a>
                    <a href=""#!"" class=""modal-close waves-effect red btn"" onclick=""Login.cancel2fa();"">Annuleren</a>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Structure --");
                WriteLiteral(@">
    <div id=""tfarestore"" class=""modal"">
        <div class=""modal-content"">
            <div class=""row"">
                <div class=""center-align"">
                    <h4>2FA uitschakelen</h4>
                    <h5>Type hier een van uw backupcodes in:</h5>
                    <div class=""row"">
                        <div class=""input-field col s12 m4 offset-m4"">
                            <input type=""text"" placeholder=""Herstelcode"" id=""tfarest"" />
                        </div>
                    </div>
                    <a href=""#!"" class=""modal-close waves-effect red btn"">Sluiten</a>
                    <a href=""#!"" class=""waves-effect btn green"" onclick=""Login.restore2fa()"">Verzenden</a>
                </div>
            </div>
        </div>
    </div>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<script>\r\n    let wrong2fa=0;var token=\"\";let redirecturl=\"");
#nullable restore
#line 105 "C:\Users\mauri\Documents\GitHub\Overstag\Overstag\Views\Register\Login.cshtml"
                                            Write(Model);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",errorcnt=0;Login={init:function(){$(""#_progbar"").show(),$(""#_reg, #_mreg"").addClass(""active""),$(""#progbar2, #mailhelper"").hide(),Login.mapEvents(),OverstagJS.General.restoreMe(Login.toggle,!0,Login.redirect)},mapEvents:function(){$(""#Password"").keydown(function(e){13==e.which&&Login.login()}),$(""#Username"").keydown(function(e){13==e.which&&$(""#Password"").focus()}),$(""#lUsername"").click(function(){$(""#Username"").focus()}),$(""#lPassword"").click(function(){$(""#Password"").focus()}),$(""#btnlogin"").click(function(){Login.login()}),$(""#tbrmail"").on(""keyup"",function(){$(""#mailhelper"").hide()}),$(""#sendmailreset"").click(function(){""""!=$(""#tbrmail"").val()?($(""#mailhelper"").hide(),Login.sendmail()):$(""#mailhelper"").show()}),$(""#2facode"").on(""paste"",function(e){var t=e.originalEvent.clipboardData.getData(""text"");(null==t||null==t||t.length<1)&&(t=$(""#2facode"").val()),t.length>=6&&Login.validate2fa()}),$(""#2facode"").on(""keyup"",function(){$(""#2facode"").val().length>=6&&Login.validate2fa()})},sendmail:function(){$(""#progb");
            WriteLiteral(@"ar2"").show(),$.post(""/Register/postMailreset"",{Email:$(""#tbrmail"").val()},function(e){""success""==e.status?($(""#tbrmail"").val(""""),M.toast({html:""Mail verzonden. Check je email a.u.b. <br><b>Controleer ook je spam map</b>"",classes:""green ""})):(M.toast({html:e.error,classes:""red ""}),$("".checki"").val(""""))},""json"").done(function(){$(""#progbar2"").hide(),M.Modal.getInstance($(""#memailreset"")).close()})},login:function(){var e=!0;$("".checki"").each(function(){""""==$.trim($(this).val())&&(e=!1)}),e?Login.post():M.toast({html:""Vul a.u.b. alle velden in"",classes:""orange""})},toggle:function(e){e?($(""#_progbar"").hide(),$(""#lblmoment"").hide(),$(""#logincard"").fadeIn(""slow""),$(""#btnlogin"").removeClass(""disabled""),$(""#Username"").removeAttr(""disabled""),$(""#Password"").removeAttr(""disabled""),$(""#Username"").focus()):($(""#btnlogin"").addClass(""disabled""),$(""#Username"").attr(""disabled"",!0),$(""#Password"").attr(""disabled"",!0),$(""#_progbar"").show())},post:function(){Login.toggle(!1),$.post(""/Register/postLogin"",{Username:$(""#Username"").v");
            WriteLiteral(@"al(),Password:$(""#Password"").val()},function(e){""success""==e.status?(3==e.type&&localStorage.setItem(""redirect"",""/Admin""),2==e.type&&localStorage.setItem(""redirect"",""/Mentor""),""yes""==e.twofactor?(Login.open2fa(),$(""#2facode"").focus(),token=e.token):(Login.redirect(),localStorage.setItem(""remember"",e.remember))):(M.toast({html:e.error,classes:""red ""}),$("".checki"").val(""""))},""json"").done(function(){Login.toggle(!0),$(""#Username"").focus()}).fail(function(){$(""#_progbar"").hide(),M.toast({html:""Inloggen mislukt door interne fout"",classes:""red""})})},redirect:function(){localStorage.setItem(""login"",!0),""""==redirecturl?localStorage.getItem(""redirect"")?($(location).attr(""href"",localStorage.getItem(""redirect"")),localStorage.removeItem(""redirect"")):$(location).attr(""href"",""/User""):$(location).attr(""href"",redirecturl)},open2fa:function(){M.Modal.getInstance($(""#tfa"")).open(),$(""#2facode"").focus()},validate2fa:function(){if(6!=$(""#2facode"").val().length||isNaN($(""#2facode"").val()))M.toast({html:""Type aub een geldige code ");
            WriteLiteral(@"in"",classes:""orange""}),$(""#2facode"").val("""");else{var e=new Date,t=e.getDate().toString().padStart(2,""0"")+""-""+(e.getMonth()+1).toString().padStart(2,""0"")+""-""+e.getFullYear()+"" ""+e.getHours().toString().padStart(2,""0"")+"":""+e.getMinutes().toString().padStart(2,""0"")+"":""+e.getSeconds().toString().padStart(2,""0"");$.post(""/Register/Validate2FA"",{token:token,code:$(""#2facode"").val(),datetime:t},function(e){""success""==e.status?(localStorage.setItem(""remember"",e.remember),Login.redirect(),M.Modal.getInstance($(""#tfa"")).close(),$(""#2facode"").val("""")):""timeout""==e.status?M.toast({html:""Uw klok staat niet goed."",classes:""orange""}):(M.toast({html:""Validatie mislukt."",classes:""red""}),$(""#2facode"").val(""""),++wrong2fa>5&&(M.toast({html:""Te veel pogingen. Probeer het later opnieuw"",classes:""orange""}),Login.cancel2fa()))},""json"").fail(function(){console.log(""Er gaat iets fout""),++errorcnt>3&&M.toast({html:""Er is iets fout gegaan"",classes:""red""})})}},cancel2fa:function(){$(""#Username"").val(""""),$(""#Password"").val(""""),$(""#2facode");
            WriteLiteral(@""").val(""""),M.Modal.getInstance($(""#tfa"")).close()},restore2fa:function(){""""==$(""#tfarest"").val()?M.toast({html:""Vul a.u.b. alle velden in!"",classes:""orange""}):$.get(""/Register/Restore2FA/""+token+""/""+encodeURIComponent($(""#tfarest"").val()),function(e){""success""==e.status?(M.Modal.getInstance($(""#tfarestore"")).close(),M.toast({html:""Code is juist. Log a.u.b. opnieuw in"",classes:""green""})):M.toast({html:""Code onjuist"",classes:""red""}),$(""#tfarest"").val("""")},""json"").fail(function(){M.toast({html:""Er is iets fout gegaan"",classes:""red""})})}},Login.init(),$(document).ready(function(){$(""input#2facode"").characterCounter()});
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<string> Html { get; private set; }
    }
}
#pragma warning restore 1591
