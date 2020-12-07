using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.RenderHelpers
{
    public partial class RefreshComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool CanRender { get; set; } = true;
        [Parameter]
        public object? Key { get; set; }
        protected override bool ShouldRender()
        {
            return CanRender;
        }
    }
}