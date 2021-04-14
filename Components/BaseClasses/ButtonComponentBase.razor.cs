using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract partial class ButtonComponentBase
    {
        public abstract string BackColor { get; }
        public abstract string TextColor { get; }
        public abstract string DisabledColor { get; }
        [Parameter]
        public EventCallback OnClick { get; set; }
        [Parameter]
        public bool IsEnabled { get; set; } = true;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}