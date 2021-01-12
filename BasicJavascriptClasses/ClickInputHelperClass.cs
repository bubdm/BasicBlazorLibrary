using BasicBlazorLibrary.Helpers;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    //decided to be internal because its only needed for the class that needs it.
    internal class ClickInputHelperClass : BaseLibraryJavascriptClass
    {
        public ClickInputHelperClass(IJSRuntime js) : base(js)
        {
        }
        public Action<int>? InputClicked;
        public Action? OtherClicked; //this means something else was clicked.
        protected override string JavascriptFileName => "elementselector";
        public async Task InitAsync()
        {
            await ModuleTask.InvokeVoidFromClassAsync("start", DotNetObjectReference.Create(this));
        }


        [JSInvokable]
        public void JsOtherClicked()
        {
            OtherClicked?.Invoke();
        }

        [JSInvokable]
        public void JsMainClicked(string id)
        {
            if (id == "")
            {
                OtherClicked?.Invoke(); //maybe no change to javascript.  because id would not be there anyways.
                return; //because none was found.
            }
            bool rets = int.TryParse(id, out int tabindex);
            if (rets == false)
            {
                OtherClicked?.Invoke(); //maybe no change to javascript.  because id would not be there anyways.
                return; //because none was found.
            }
            InputClicked?.Invoke(tabindex);
        }
    }
}