#pragma checksum "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bb71a7f33344c5ec457be30e7c310768efe97bb7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Items_All), @"mvc.1.0.view", @"/Views/Items/All.cshtml")]
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
#nullable restore
#line 1 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\_ViewImports.cshtml"
using FastFood.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb71a7f33344c5ec457be30e7c310768efe97bb7", @"/Views/Items/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4ed879bff0478396c899ea94a6589fe8b9c42e19", @"/Views/_ViewImports.cshtml")]
    public class Views_Items_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<FastFood.Core.ViewModels.Items.ItemsAllViewModels>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
  
    ViewData["Title"] = "All Items";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<h1 class=""text-center"">All Items</h1>
<hr class=""hr-2"" />
<table class=""table mx-auto"">
    <thead>
        <tr class=""row"">
            <th class=""col-md-1"">#</th>
            <th class=""col-md-2"">Name</th>
            <th class=""col-md-2"">Price</th>
            <th class=""col-md-2"">Category</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 18 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
         for(var i = 0; i < Model.Count(); i++)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr class=\"row\">\r\n            <th class=\"col-md-1\">");
#nullable restore
#line 21 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
                             Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n            <td class=\"col-md-2\">");
#nullable restore
#line 22 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
                            Write(Model[i].Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"col-md-2\">");
#nullable restore
#line 23 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
                            Write(Model[i].Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"col-md-2\">");
#nullable restore
#line 24 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
                            Write(Model[i].Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 26 "D:\Repos\07. Auto-Mapping-Objects-Project\FastFood.Core\Views\Items\All.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </tbody>\r\n</table>\r\n<div class=\"button-holder btn-block\">\r\n    <button type=\"submit\" class=\"btn-lg col-9\">Create</button>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<FastFood.Core.ViewModels.Items.ItemsAllViewModels>> Html { get; private set; }
    }
}
#pragma warning restore 1591
