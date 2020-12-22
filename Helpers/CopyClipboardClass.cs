using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Helpers
{
    //this has to use the off the shelf di system.
    public class CopyClipboardClass : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        //this can use the off the shelf dependency injection system.
        public CopyClipboardClass(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BasicBlazorLibrary/Custom.js").AsTask());
        }
        //no longer needs the javascript alert because i can decide to do toasts or modals.
        public async ValueTask CopyTextAsync(string text)
        {
            var model = await _moduleTask.Value;
            await model.InvokeVoidAsync("clipboardCopy", text);
        }


        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}