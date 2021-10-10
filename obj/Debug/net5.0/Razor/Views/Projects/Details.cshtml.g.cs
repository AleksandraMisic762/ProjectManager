#pragma checksum "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c682042cccd4f9fe5498c2706b3279331615a723"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Projects_Details), @"mvc.1.0.view", @"/Views/Projects/Details.cshtml")]
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
#line 1 "C:\Users\HP\interviews\get\ProjectManager\Views\_ViewImports.cshtml"
using ProjectManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HP\interviews\get\ProjectManager\Views\_ViewImports.cshtml"
using ProjectManager.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c682042cccd4f9fe5498c2706b3279331615a723", @"/Views/Projects/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a47e88e4e21750c9b1b123cb51fb857497f44a97", @"/Views/_ViewImports.cshtml")]
    public class Views_Projects_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProjectManager.ViewModels.ProjectTasksViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>Project</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 14 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Project.ProjectName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 17 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
       Write(Html.DisplayFor(model => model.Project.ProjectName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 20 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ProjectProgress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 23 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
       Write(Html.DisplayFor(model => model.ProjectProgress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n\r\n<div>\r\n    <h5>Project Tasks</h5>\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    Total Tasks\r\n                </th>\r\n");
#nullable restore
#line 36 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                 foreach (var item in (IEnumerable<string>)ViewData["Statuses"])
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <th>\r\n                        ");
#nullable restore
#line 39 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                   Write(item);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n");
#nullable restore
#line 41 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <th>\r\n                   Overdue in 2 Days of Fewer\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n            \r\n            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Model.Tasks.ToArray().Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n");
#nullable restore
#line 53 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                 foreach (var item in (IEnumerable<string>)ViewData["Statuses"])
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        ");
#nullable restore
#line 56 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                   Write(Model.Tasks.Where(t => t.Status.Name.Equals(item)).ToArray().Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n");
#nullable restore
#line 58 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\r\n                    ");
#nullable restore
#line 60 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Model.Tasks.Where(t => t.Deadline.Day - DateTime.Now.Day <= 2).ToArray().Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n            \r\n        </tbody>\r\n    </table>\r\n</div>\r\n\r\n\r\n<div>\r\n    <h5>Project Tasks Details</h5>\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 75 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Tasks.FirstOrDefault().Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 78 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Tasks.FirstOrDefault().Deadline));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 81 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Tasks.FirstOrDefault().Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 84 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Tasks.FirstOrDefault().Progress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 87 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.Tasks.FirstOrDefault().Assignee));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 92 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
             foreach (var item in Model.Tasks)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 96 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Status.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 99 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Deadline));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 102 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 105 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Progress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 108 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
               Write(Html.DisplayFor(modelItem => item.Assignee.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 111 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c682042cccd4f9fe5498c2706b3279331615a72312247", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 117 "C:\Users\HP\interviews\get\ProjectManager\Views\Projects\Details.cshtml"
                           WriteLiteral(Model.Project.ProjectCode);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c682042cccd4f9fe5498c2706b3279331615a72314400", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProjectManager.ViewModels.ProjectTasksViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
