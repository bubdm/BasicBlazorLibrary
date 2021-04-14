using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    internal class FocusClass : BaseLibraryJavascriptClass
    {
        public FocusClass(IJSRuntime js) : base(js)
        {
        }
        protected override string JavascriptFileName => "selectalltext";
        public async Task FocusAsync(ElementReference? element)
        {
            await ModuleTask.InvokeVoidFromClassAsync("selectall", element);
        }
    }
}