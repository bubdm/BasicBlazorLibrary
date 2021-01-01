using Microsoft.JSInterop;

namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    public abstract class BaseLocalJavascriptClass : BaseStandardJavascriptClass
    {
        protected BaseLocalJavascriptClass(IJSRuntime js) : base(js)
        {
        }

        protected override bool IsLocal => true;
        
    }
}
