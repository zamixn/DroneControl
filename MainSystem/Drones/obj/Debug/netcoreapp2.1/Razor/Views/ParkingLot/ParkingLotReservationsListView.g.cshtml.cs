#pragma checksum "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ad894da136e9302a67840a3337ab076fef6af29e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ParkingLot_ParkingLotReservationsListView), @"mvc.1.0.view", @"/Views/ParkingLot/ParkingLotReservationsListView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ParkingLot/ParkingLotReservationsListView.cshtml", typeof(AspNetCore.Views_ParkingLot_ParkingLotReservationsListView))]
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
#line 1 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\_ViewImports.cshtml"
using Drones;

#line default
#line hidden
#line 2 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\_ViewImports.cshtml"
using Drones.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad894da136e9302a67840a3337ab076fef6af29e", @"/Views/ParkingLot/ParkingLotReservationsListView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9cc7f2395d6081105a90e1a7b5173787dd7cb41", @"/Views/_ViewImports.cshtml")]
    public class Views_ParkingLot_ParkingLotReservationsListView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Drones.Models.Reservation>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(47, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml"
  
    ViewData["Title"] = "ParkingLotReservationsListView";

#line default
#line hidden
            BeginContext(115, 54, true);
            WriteLiteral("\r\n<h2>ParkingLotReservationsListView</h2>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(169, 37, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4360d762bca44d2aa0f3c18c342e00dc", async() => {
                BeginContext(192, 10, true);
                WriteLiteral("Create New");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(206, 123, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 19 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(361, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(410, 65, false);
#line 22 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(475, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(496, 71, false);
#line 23 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml"
           Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(567, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(588, 69, false);
#line 24 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(657, 36, true);
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 27 "C:\Stuff\_UNI_\3 Kursas\2 Semestras\Programų sistemų analizės ir projektavimo įrankiai\Drones\Drones\MainSystem\Drones\Views\ParkingLot\ParkingLotReservationsListView.cshtml"
}

#line default
#line hidden
            BeginContext(696, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Drones.Models.Reservation>> Html { get; private set; }
    }
}
#pragma warning restore 1591
