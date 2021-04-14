using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class PopupBase : JavascriptComponentBase
    {
        [Parameter]
        public bool Visible { get; set; }
        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }
        protected virtual void ClosePopup()
        {
            VisibleChanged.InvokeAsync(false);
        }
        [Parameter]
        public int ZIndex { get; set; } = 1; //this is the priority.  the higher means more likely this will be on top.
    }
}
