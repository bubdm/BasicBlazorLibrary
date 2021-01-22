using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupCustomPositions
    {
        [Parameter]
        public string Width { get; set; } = "40vmin"; //default to 40 percent minimum.  however, you can set whatever you want.
        protected override string GetWidth => Width;
        [Parameter]
        public string Top { get; set; } = "0px";
        [Parameter]
        public string Left { get; set; } = "0px";
    }
}