using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public class AutoScrollClass : BaseLibraryJavascriptClass
    {
        public AutoScrollClass(IJSRuntime js) : base(js) { }
        protected override string JavascriptFileName => "autoscroll";
        public async Task ScrollToElementAsync(ElementReference? element)
        {
            if (element == null)
            {
                return;
            }
            await ModuleTask.InvokeVoidAsync("scrolltoelement", element);
        }
        public async Task SetScrollPosition(ElementReference? element, float position)
        {
            await ModuleTask.InvokeVoidAsync("setscroll", element, position);
        }
    }
}