using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class HorizontalDiv
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Margins { get; set; } = "5px";
        [Parameter]
        public string Width { get; set; } = "";
        [Parameter]
        public string LeftOnly { get; set; } = "";
        [Parameter]
        public string RightOnly { get; set; } = "";
        [Parameter]
        public string BackgroundColor { get; set; } = "";
        private string GetExtraMarginText()
        {
            if (LeftOnly == "" && RightOnly == "")
            {
                return $"margin: {Margins};";
            }
            if (RightOnly != "")
            {
                return $"margin-right: {RightOnly};";
            }
            return $"margin-left: {LeftOnly};";
        }
        private string GetExtraBackgroundText()
        {
            if (BackgroundColor == "")
            {
                return "";
            }
            return $"background-color: {BackgroundColor.ToWebColor()};"; //its intended to use my custom stuff.
        }
        private string GetWidthStyles()
        {
            if (Width == "")
            {
                return "";
            }
            return $"width: {Width};";
        }
    }
}