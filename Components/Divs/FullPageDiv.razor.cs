using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class FullPageDiv
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}