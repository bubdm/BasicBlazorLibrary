using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public class HoverClass : BaseLibraryJavascriptClass
    {
        public HoverClass(IJSRuntime js) : base(js)
        {
        }
        protected override string JavascriptFileName => "hover.js";
        public async Task HoverAsync(ElementReference element, string source)
        {
            await ModuleTask.InvokeVoidFromClassAsync("hover", element, source);
        }
        public async Task UnHoverAsync(ElementReference element, string source)
        {
            await ModuleTask.InvokeVoidFromClassAsync("unhover", element, source);
        }
    }
}