#pragma checksum "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\DroneSubsystem\tempDroneForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fb931b2a70aea37ae9e27064f39dd93c3770a83e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DroneSubsystem_tempDroneForm), @"mvc.1.0.view", @"/Views/DroneSubsystem/tempDroneForm.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DroneSubsystem/tempDroneForm.cshtml", typeof(AspNetCore.Views_DroneSubsystem_tempDroneForm))]
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
#line 1 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\_ViewImports.cshtml"
using Drones;

#line default
#line hidden
#line 2 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\_ViewImports.cshtml"
using Drones.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb931b2a70aea37ae9e27064f39dd93c3770a83e", @"/Views/DroneSubsystem/tempDroneForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9cc7f2395d6081105a90e1a7b5173787dd7cb41", @"/Views/_ViewImports.cshtml")]
    public class Views_DroneSubsystem_tempDroneForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(16, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\DroneSubsystem\tempDroneForm.cshtml"
  
    ViewData["Title"] = "tempDroneForm";

#line default
#line hidden
            BeginContext(67, 147, true);
            WriteLiteral("\r\n<h2>testavimas</h2>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>numeris</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 16 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\DroneSubsystem\tempDroneForm.cshtml"
         foreach (string item in (List<string>)Model)
        {

#line default
#line hidden
            BeginContext(280, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(341, 30, false);
#line 20 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\DroneSubsystem\tempDroneForm.cshtml"
               Write(Html.DisplayFor(plate => item));

#line default
#line hidden
            EndContext();
            BeginContext(371, 21, true);
            WriteLiteral("\r\n            </tr>\r\n");
            EndContext();
#line 22 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programu sistemu analizes ir projektavimo irankiai\Drones\Drones\MainSystem\Drones\Views\DroneSubsystem\tempDroneForm.cshtml"
        }

#line default
#line hidden
            BeginContext(403, 28, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n");
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
