using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Helpers
{
    public static class BasicJavascriptExtensions
    {
        private static Lazy<Task<IJSObjectReference>> GetModuleTask(this IJSRuntime js )
        {
            return js.GetLibraryModuleTask("basichelpers");
        }

        public static async Task<int> GetContainerHeight(this IJSRuntime js, ElementReference? element)
        {
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("getcontainerheight", element);
        }
        public static async Task<int> PixelsPerRem(this IJSRuntime js)
        {
            var moduleTask = js.GetModuleTask();
            return await moduleTask.InvokeDisposeAsync<int>("convertRemToPixels", 1);
        }

        //these 2 was somewhat exceptions.  no problem this time though.
        public static async Task<string> GetOperatingSystemAsync(this IJSRuntime js)
        {
            var module = js.GetLibraryModuleTask("operatingsystemhelpers");
            string results = await module.InvokeDisposeAsync<string>("getOS");
            return results;
        }
        public static async Task<bool> HasKeyboard(this IJSRuntime js)
        {
            var module = js.GetLibraryModuleTask("operatingsystemhelpers");
            bool output = await module.InvokeDisposeAsync<bool>("hasKeyboard");
            return output;
        }
    }
}