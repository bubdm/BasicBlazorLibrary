using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.JSInterop;
using System;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public abstract class BaseStandardJavascriptClass : IAsyncDisposable
    {
        protected Lazy<Task<IJSObjectReference>> ModuleTask; //i think this is okay.  i have seen many cases where the first part of the name is the same.
        protected abstract bool IsLocal { get; } //this influences how it would create this module.
        protected abstract string JavascriptFileName { get; } //this is the javascript file being used by this class.
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (ModuleTask.IsValueCreated)
            {
                var module = await ModuleTask.Value;
                await module.DisposeAsync();
            }
        }
        public BaseStandardJavascriptClass(IJSRuntime js)
        {
            string jsName = JavascriptFileName;
            if (jsName.EndsWith("js") == false)
            {
                jsName = $"{jsName}.js";
            }
            if (IsLocal)
            {
                ModuleTask = new(() => js.InvokeAsync<IJSObjectReference>(
                "import", $"./{jsName}").AsTask());
            }
            else
            {
                Assembly? aa = Assembly.GetAssembly(GetType());
                if (aa == null)
                {
                    throw new CustomBasicException("You need an assmebly for this.  Otherwise, rethink");
                }
                string firsts = aa.FullName!;
                int index = firsts.IndexOf(", ");
                string ns = firsts.Substring(0, index);
                ModuleTask = new(() => js.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/{ns}/{jsName}").AsTask());
            }
        }
    }
}