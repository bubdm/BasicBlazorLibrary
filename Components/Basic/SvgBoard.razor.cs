using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class SvgBoard
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public string TargetWidth { get; set; } = "";
        [Parameter]
        public float X { get; set; }
        [Parameter]
        public float Y { get; set; }
        [Parameter]
        public SizeF BoardSize { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EventCallback BoardClicked { get; set; }
        private void PrivateClicked()
        {
            if (BoardClicked.HasDelegate == false)
            {
                return;
            }
            BoardClicked.InvokeAsync();
        }
        private string GetSvgStyle()
        {
            if (TargetHeight == "" && TargetWidth == "")
            {
                return "";
            }
            if (TargetHeight != "" && TargetWidth != "")
            {
                return ""; //try this too so you have to pick between 2 options.
            }
            if (TargetHeight != "")
            {
                return $"height: {TargetHeight}";
            }
            return $"width: {TargetWidth}";
        }
        private string GetViewBox()
        {
            return $"0 0 {BoardSize.Width} {BoardSize.Height}";
        }
    }
}