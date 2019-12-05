#pragma checksum "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Passreset.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a3e52653d666754d63c63b3141a36a859b9fbb0b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Register_Passreset), @"mvc.1.0.view", @"/Views/Register/Passreset.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3e52653d666754d63c63b3141a36a859b9fbb0b", @"/Views/Register/Passreset.cshtml")]
    public class Views_Register_Passreset : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Overstag.Models.Account>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Passreset.cshtml"
  
    ViewBag.Title = "Wachtwoord resetten";

#line default
#line hidden
            WriteLiteral("    <div>\r\n        <div class=\"row\">\r\n            <div class=\"col s12 m6 offset-m3\">\r\n                <h4 class=\"blue-light-text center-align\"><b>Hallo ");
#line 8 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Passreset.cshtml"
                                                             Write(Model.Firstname);

#line default
#line hidden
            WriteLiteral(@"</b></h4><br />
                <div class=""card-panel"" style=""display: block; margin-left: auto; margin-right: auto"">
                    <h6 class=""center-align grey-text"">Verzin een nieuw wachtwoord voor je account.</h6><br />
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""Password1"" placeholder=""Nieuw wachtwoord"" />
                        <label for=""Password1"">Nieuw wachtwoord</label>
                    </div>
                    <div class=""input-field"">
                        <i class=""material-icons prefix"">lock</i>
                        <input type=""password"" class=""checki"" id=""Password2"" placeholder=""Nieuw wachtwoord herhalen"" />
                        <label for=""Password2"">Nieuw wachtwoord herhalen</label>
                    </div>
                    <div class=""card-action center-align"">
                        <br />
                        <a ");
            WriteLiteral(@"class=""btn blue-light btn-large waves-effect"" id=""btnsubmit"">Opslaan</a><br />
                        <span id=""tbhelper"" style=""color: red;"">Vul alle velden in</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
<script>
    var Pass = {
        init: function () {
            $('#tbhelper').hide();
            Pass.mapEvents();
        },
        mapEvents: function () {
            $('#Password1').on('keyup', function () { Pass.checkii(); });
            $('#Password2').on('keyup', function () { Pass.checkii(); });
            $('#btnsubmit').click(function () { Pass.checki(); });
        },
        checkii: function () {
            if ($('#Password1').val() != $('#Password2').val()) {
                $('#tbhelper').text('Wachtwoorden komen niet overeen').show();
            } else {
                $('#tbhelper').hide();
            }
        },
        checki: function () {
            var isValid = ($('#Password1').val() == $(");
            WriteLiteral(@"'#Password2').val());
            $('.checki').each(function () {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                    $('#tbhelper').text('Vul a.u.b. alle velden in').show();
                }
            });
            if (isValid) {
                $('#_progbar').show();
                $('#btnsubmit').addClass('disabled');
                $.post(""/Register/postPassreset"", { Token: '");
#line 59 "C:\Users\maurict\Documents\GitHub\Overstag\Overstag\Views\Register\Passreset.cshtml"
                                                       Write(Uri.EscapeDataString(Model.Token));

#line default
#line hidden
            WriteLiteral(@"', Password: $('#Password1').val() }, function (response) {
                    if (response.status == 'success') {
                        $('.checki').val('');
                        M.toast({ html: 'Wachtwoord successvol opgeslagen.<br> U kunt nu opnieuw inloggen via het tabje &quot;Aanmelden&quot;', classes: 'green' });
                    }
                    else {
                        //status = error
                        M.toast({ html: response.error, classes: 'red' });
                        $('#btnsubmit').removeClass('disabled');
                        $('.checki').val('');
                    }
                },'json').fail(function () {
                    M.toast({ html: 'Er gaat iets fout en we weten niet wat', classes: 'red' });
                    $('#_progbar').hide();
                    $('#btnsubmit').removeClass('disabled');
                }).done(function() {
                    $('#_progbar').hide();
                });
            }
        }
    }

");
            WriteLiteral("    Pass.init();\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Overstag.Models.Account> Html { get; private set; }
    }
}
#pragma warning restore 1591
