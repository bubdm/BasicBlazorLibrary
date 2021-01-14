﻿using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public class TextBoxHelperClass : BaseLibraryJavascriptClass
    {
        public TextBoxHelperClass(IJSRuntime js) : base(js)
        {
        }
        protected override string JavascriptFileName => "text";

        public async Task SetInitTextAsync(ElementReference? element, string value)
        {
            await ModuleTask.InvokeVoidFromClassAsync("setInitialText", element, value);
        }
        public async Task ClearTextAsync(ElementReference? element)
        {
            await ModuleTask.InvokeVoidFromClassAsync("clearText", element);
        }
        public async Task SetNewValueAndHighlightAsync(ElementReference? element, string value, int startAt)
        {
            await ModuleTask.InvokeVoidFromClassAsync("setvalueandhighlight", element, value, startAt);
        }
        public async Task SetNewValueAloneAsync(ElementReference? element, string value)
        {
            await ModuleTask.InvokeVoidFromClassAsync("setvaluealone", element, value);
        }
        public async Task<string> GetValueAsync(ElementReference? element)
        {
            return await ModuleTask.InvokeFromClassAsync<string>("getValue", element);
        }
    }
}