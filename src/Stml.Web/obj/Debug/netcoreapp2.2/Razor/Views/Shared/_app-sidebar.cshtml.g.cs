#pragma checksum "F:\AspNet\github\Stml\src\Stml.Web\Views\Shared\_app-sidebar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66998013bd840abd68f84f67237fffb598ee6d42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__app_sidebar), @"mvc.1.0.view", @"/Views/Shared/_app-sidebar.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_app-sidebar.cshtml", typeof(AspNetCore.Views_Shared__app_sidebar))]
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
#line 1 "F:\AspNet\github\Stml\src\Stml.Web\Views\_ViewImports.cshtml"
using Stml.Web;

#line default
#line hidden
#line 2 "F:\AspNet\github\Stml\src\Stml.Web\Views\_ViewImports.cshtml"
using Stml.Web.Models;

#line default
#line hidden
#line 1 "F:\AspNet\github\Stml\src\Stml.Web\Views\Shared\_app-sidebar.cshtml"
using Stml.Infrastructure.Applications.Navigation;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66998013bd840abd68f84f67237fffb598ee6d42", @"/Views/Shared/_app-sidebar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54229468630c964651849623a22131df11515c82", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__app_sidebar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(52, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(114, 59, true);
            WriteLiteral("\r\n<nav class=\"sidebar-nav\">\r\n    <ul class=\"nav\">\r\n        ");
            EndContext();
            BeginContext(174, 78, false);
#line 7 "F:\AspNet\github\Stml\src\Stml.Web\Views\Shared\_app-sidebar.cshtml"
   Write(await Html.PartialAsync("_SidebarNavRenderPartial", NavManager.MainMenu.Items));

#line default
#line hidden
            EndContext();
            BeginContext(252, 19, true);
            WriteLiteral("\r\n    </ul>\r\n</nav>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public INavigationManager<MenuGroup, MenuItem> NavManager { get; private set; }
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
