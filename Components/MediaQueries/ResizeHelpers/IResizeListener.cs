using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers
{
    public interface IResizeListener
    {
        event Action<BrowserSize>? OnResized;
        ValueTask<BrowserSize> GetBrowserWindowSize();
    }
}