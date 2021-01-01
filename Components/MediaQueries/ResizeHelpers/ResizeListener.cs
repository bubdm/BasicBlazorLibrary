using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers
{
    //decided to not make people register.  just one more thing that would be needed.  i think this time, no need for it.
    public class ResizeListener : IAsyncDisposable
    {
        public ResizeListener(IJSRuntime jS)
        {
            _moduleTask = new(() => jS.InvokeAsync<IJSObjectReference>(
              "import", "./_content/BasicBlazorLibrary/Resize.js").AsTask());
        }

        private event Action<BrowserSize>? Onresized;

        public event Action<BrowserSize>? OnResized
        {
            add
            {
                Subscribe(value!);
            }
            remove
            {
                Unsubscribe(value!);
            }
        }

        private void Subscribe(Action<BrowserSize> value)
        {
            if (Onresized == null)
            {
                Task.Run(async () => await Start());
            }
            Onresized += value;
        }


        private void Unsubscribe(Action<BrowserSize> value)
        {
            Onresized -= value;
            if (Onresized == null)
            {
#pragma warning disable CA2012 // Use ValueTasks correctly
                _ = Cancel().ConfigureAwait(false);
#pragma warning restore CA2012 // Use ValueTasks correctly
            }
        }

        private async ValueTask<bool> Start()
        {
            var module = await _moduleTask.Value;
            bool rets = await module.InvokeAsync<bool>("listenForResize", DotNetObjectReference.Create(this));
            return rets;
        }

        private async ValueTask Cancel()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("cancelListener");
        }
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        public async ValueTask<BrowserSize> GetBrowserWindowSize()
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<BrowserSize>("getBrowserWindowSize");
        }

        /// <summary>
        /// Invoked by jsInterop, use the OnResized event handler to subscribe.
        /// </summary>
        /// <param name="browserWindowSize"></param>
        [JSInvokable]
        public void RaiseOnResized(BrowserSize browserWindowSize) =>
            Onresized?.Invoke(browserWindowSize);
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    Onresized = null;
                }
                _disposed = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
            Dispose(true);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }

    }
}
