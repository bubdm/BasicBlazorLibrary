using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.RenderHelpers
{
    public partial class VisibleHiddenComponent
    {
        [Parameter]
        public bool Visible { get; set; } = true;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private string GetDisplay => Visible ? "" : "none";
    }
}