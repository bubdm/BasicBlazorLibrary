using Microsoft.JSInterop;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public abstract class BaseLibraryJavascriptClass : BaseStandardJavascriptClass
    {
        protected BaseLibraryJavascriptClass(IJSRuntime js) : base(js)
        {
        }
        protected override bool IsLocal => false;
    }
}
