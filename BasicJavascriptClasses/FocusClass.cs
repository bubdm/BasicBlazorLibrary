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
        //its common enough to need a class for it.
        protected override string JavascriptFileName => "selectalltext";
        public async Task FocusAsync(ElementReference? element)
        {
            await element!.Value.FocusAsync();
            await ModuleTask.InvokeVoidFromClassAsync("selectall", element);
        }
    }
}