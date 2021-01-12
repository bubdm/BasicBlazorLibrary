using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class JavascriptComponentBase : ComponentBase
    {
        [Inject]
        public IJSRuntime? JS { get; set; }
        //this can be useful for new classes where you want the javascript interop for many processes but don't want to have to have the extra line of code.
        //was going to be public.  however, there are cases where its doing cascading and it needs access to that javascript part.  this can be a good idea.
    }
}