using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract partial class FullPageComponentBase
    {
        public abstract string BackgroundColor { get; }
        public abstract string TextColor { get; }
        [CascadingParameter]
        private MediaQueryListComponent? MediaQuery { get; set; }
        private string Height => $"{MediaQuery!.BrowserInfo!.Height}px";

        //protected string Height => "200px"; //pretend like its 200 pixels.
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}