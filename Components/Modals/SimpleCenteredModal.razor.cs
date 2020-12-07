using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class SimpleCenteredModal
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EventCallback CloseDialog { get; set; }
        [Parameter]
        public string TargetWidth { get; set; } = "";
    }
}