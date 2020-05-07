#pragma checksum "F:\dev\frontend-service\Pages\ApplicationResult.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4b47f834a9b3486623012822bab82b2b5666d2be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(frontend_service.Pages.Pages_ApplicationResult), @"mvc.1.0.razor-page", @"/Pages/ApplicationResult.cshtml")]
namespace frontend_service.Pages
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
#line 1 "F:\dev\frontend-service\Pages\_ViewImports.cshtml"
using frontend_service;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b47f834a9b3486623012822bab82b2b5666d2be", @"/Pages/ApplicationResult.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c5432ea88ec0cbca8bd70c96e1024b9507c63a0d", @"/Pages/_ViewImports.cshtml")]
    public class Pages_ApplicationResult : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
  
    ViewData["Title"] = "Результат интеграции";
    string token = Request.Cookies["token_"];
    var error = Model.error;
    List<string> resultElements = Startup.clientData[Model.filejobPage3.IndexClientData].ResultElements;
    List<string> resultLinks = Startup.clientData[Model.filejobPage3.IndexClientData].ResultLinks;
    var resultDcmp = Startup.clientData[Model.filejobPage3.IndexClientData].ResultDcmp;
    var url = Model.filejobPage3.Url_filestorage + "download?token=" + token;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""jumbotron"">
    <p style=""font-size: 58px"">
        <img src=""/Files/logo.png"" width=""80"" height=""80"" align=""middle""> Готово!
    </p>
    <p class=""lead"">Процесс соединения окончен. Получите результат!</p>
    <p>
        <a class=""btn btn-primary btn-lg""");
            BeginWriteAttribute("href", " href=\"", 838, "\"", 849, 1);
#nullable restore
#line 19 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
WriteAttributeValue("", 845, url, 845, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("download", " download=\"", 850, "\"", 861, 0);
            EndWriteAttribute();
            WriteLiteral(@">
            Загрузить файл
            <svg class=""bi bi-download"" width=""1em"" height=""1em"" viewBox=""0 0 16 16"" fill=""currentColor"" xmlns=""http://www.w3.org/2000/svg"">
                <path fill-rule=""evenodd"" d=""M.5 8a.5.5 0 01.5.5V12a1 1 0 001 1h12a1 1 0 001-1V8.5a.5.5 0 011 0V12a2 2 0 01-2 2H2a2 2 0 01-2-2V8.5A.5.5 0 01.5 8z"" clip-rule=""evenodd"" />
                <path fill-rule=""evenodd"" d=""M5 7.5a.5.5 0 01.707 0L8 9.793 10.293 7.5a.5.5 0 11.707.707l-2.646 2.647a.5.5 0 01-.708 0L5 8.207A.5.5 0 015 7.5z"" clip-rule=""evenodd"" />
                <path fill-rule=""evenodd"" d=""M8 1a.5.5 0 01.5.5v8a.5.5 0 01-1 0v-8A.5.5 0 018 1z"" clip-rule=""evenodd"" />
            </svg>
        </a>
    </p>
</div>

<div class=""accordion"" id=""accordionExample"">
    <div class=""card"">
        <div class=""card-header"" id=""headingOne"">
            <h5 class=""mb-0"">
                <button class=""btn btn-link"" type=""button"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collap");
            WriteLiteral(@"seOne"">
                    Содержимое элементов диаграммы
                </button>
            </h5>
        </div>
        <div id=""collapseOne"" class=""collapse"" aria-labelledby=""headingOne"" data-parent=""#accordionExample"">
            <div class=""card-body"">
");
#nullable restore
#line 41 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
                 foreach (string item in resultElements)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
               Write(item);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 44 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
    <div class=""card"">
        <div class=""card-header"" id=""headingTwo"">
            <h5 class=""mb-0"">
                <button class=""btn btn-link collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#collapseTwo"" aria-expanded=""false"" aria-controls=""collapseTwo"">
                    Содержимое связей диаграммы
                </button>
            </h5>
        </div>
        <div id=""collapseTwo"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordionExample"">
            <div class=""card-body"">
");
#nullable restore
#line 58 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
                 foreach (string item in resultLinks)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
               Write(item);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 61 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
    </div>
    <div class=""card"">
        <div class=""card-header"" id=""heading3"">
            <h5 class=""mb-0"">
                <button class=""btn btn-link collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#collapse3"" aria-expanded=""false"" aria-controls=""collapse3"">
                    Содержимое декомпозиции
                </button>
            </h5>
        </div>
        <div id=""collapse3"" class=""collapse"" aria-labelledby=""heading3"" data-parent=""#accordionExample"">
            <div class=""card-body"">
                ");
#nullable restore
#line 75 "F:\dev\frontend-service\Pages\ApplicationResult.cshtml"
           Write(resultDcmp);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<frontend_service.ApplicationResultModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<frontend_service.ApplicationResultModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<frontend_service.ApplicationResultModel>)PageContext?.ViewData;
        public frontend_service.ApplicationResultModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
