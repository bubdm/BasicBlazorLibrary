using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class RadioButtonGroupComponent
    {
        [Parameter]
        public string Name { get; set; } = "";
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}