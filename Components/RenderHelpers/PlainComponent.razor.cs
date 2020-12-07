using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.RenderHelpers
{
    public partial class PlainComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}