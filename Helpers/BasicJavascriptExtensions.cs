using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Helpers
{
    public static class BasicJavascriptExtensions
    {

        //private const string _path = "basichelpers";

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
    }
}