using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public class CenterClass : BaseLibraryJavascriptClass
    {
        public CenterClass(IJSRuntime js) : base(js)
        {
        }

        protected override string JavascriptFileName => "center";
        public async Task CenterDiv(ElementReference? element)
        {
            await ModuleTask.InvokeVoidFromClassAsync("center", element);
        }
    }
}
