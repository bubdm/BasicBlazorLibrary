using Microsoft.JSInterop;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Helpers
{
    public static class CopyClipboardExtensions
    {
        public static async Task CopyTextAsync(this IJSRuntime js, string text)
        {
            var moduleTask = js.GetLibraryModuleTask("clipboard");
            await moduleTask.InvokeVoidAsync("clipboardCopy", text);

        }
    }
}