using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers;
using BasicBlazorLibrary.Helpers;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public class ResizeListenerClass : BaseLibraryJavascriptClass
    {
        public ResizeListenerClass(IJSRuntime js) : base(js)
        {
        }

        protected override string JavascriptFileName => "Resize";


        //decided to risk not even doing event but delegate this time.

        public Action<BrowserSize>? Onresized { get; set; }

        public async Task InitAsync()
        {
            await ModuleTask.InvokeVoidFromClassAsync("listenForResize", DotNetObjectReference.Create(this)); //hopefully this simple.
        }

        //hopefully no need to cancel this time.
        /// <summary>
        /// Invoked by jsInterop, use the OnResized delgate to subscribe.
        /// </summary>
        /// <param name="browserWindowSize"></param>
        [JSInvokable]
        public void RaiseOnResized(BrowserSize browserWindowSize) =>
            Onresized?.Invoke(browserWindowSize);

        //private async ValueTask Cancel()
        //{
        //    var module = await _moduleTask.Value;
        //    await module.InvokeVoidAsync("cancelListener");
        //}


        //private async ValueTask<bool> Start()
        //{
        //    var module = await _moduleTask.Value;
        //    bool rets = await module.InvokeAsync<bool>("listenForResize", DotNetObjectReference.Create(this));
        //    return rets;
        //}



    }
}
