using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{

    public class HighlightTextBoxClass : BaseLibraryJavascriptClass
    {
        public HighlightTextBoxClass(IJSRuntime js) : base(js) { }
        protected override string JavascriptFileName => "highlighter";
        public async Task PartialHighlightText(ElementReference? element, int startat)
        {
            await ModuleTask.InvokeVoidAsync("highlighttext", element, startat);
        }
    }
}