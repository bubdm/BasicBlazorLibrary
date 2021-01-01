using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public class ScrollListenerClass : BaseLibraryJavascriptClass
    {
        public ScrollListenerClass(IJSRuntime js) : base(js)
        {
        }
        public event Action<int>? Scrolled;
        [JSInvokable]
        public void ScrollChanged(int value)
        {
            Scrolled?.Invoke(value);
        }
        public async Task InitAsync(ElementReference? element)
        {
            if (element == null)
            {
                return; //just in case.
            }
            await ModuleTask.InvokeVoidAsync("start", DotNetObjectReference.Create(this), element); //i am forced to use the dotnetobjectreferencecreate method 
        }
        protected override string JavascriptFileName => "scrolllistener";
    }
}