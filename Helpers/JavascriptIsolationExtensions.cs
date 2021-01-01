using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Helpers
{
    public static class JavascriptIsolationExtensions
    {
        private static string GetJsName(string name)
        {
            string jsName = name;
            if (jsName.EndsWith("js") == false)
            {
                jsName = $"{jsName}.js";
            }
            return jsName;
        }
        public static Lazy<Task<IJSObjectReference>> GetLocalModuleTask(this IJSRuntime js, string javascriptfile)
        {
            return js.GetModuleTask($"./{GetJsName(javascriptfile)}");
        }
        public static Lazy<Task<IJSObjectReference>> GetLibraryModuleTask(this IJSRuntime js, string libraryName, string javascriptfile)
        {
            return js.GetModuleTask($"./_content/{libraryName}/{GetJsName(javascriptfile)}");
        }
        internal static Lazy<Task<IJSObjectReference>> GetLibraryModuleTask(this IJSRuntime js, string javascriptfile)
        {
            return js.GetModuleTask($"./_content/BasicBlazorLibrary/{GetJsName(javascriptfile)}");
        }
        private static Lazy<Task<IJSObjectReference>> GetModuleTask(this IJSRuntime js, string fullPath)
        {
            Lazy<Task<IJSObjectReference>> output = new(() => js.InvokeAsync<IJSObjectReference>(
             "import", fullPath).AsTask());
            return output;
        }
        public static async Task InvokeVoidAsync(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(identifier, args);
            await module.DisposeAsync(); //i think should dispose each time as well.
        }
        public static async Task<T> InvokeAsync<T>(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[] args)
        {
            var module = await moduleTask.Value;
            var output = await module.InvokeAsync<T>(identifier, args);
            await module.DisposeAsync();
            return output;
        }
    }
}