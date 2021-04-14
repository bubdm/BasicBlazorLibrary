using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupCustomPositions
    {
        [Parameter]
        public string Width { get; set; } = "40vmin";
        protected override string GetWidth => Width;
        [Parameter]
        public string Top { get; set; } = "0px";
        [Parameter]
        public string Left { get; set; } = "0px";
    }
}