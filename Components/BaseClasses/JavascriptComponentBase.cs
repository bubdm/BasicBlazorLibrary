using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class JavascriptComponentBase : ComponentBase
    {
        [Inject]
        protected IJSRuntime? JS { get; set; }
        //this can be useful for new classes where you want the javascript interop for many processes but don't want to have to have the extra line of code.
    }
}