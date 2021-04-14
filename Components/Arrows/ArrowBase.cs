using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.Arrows
{
    public class ArrowBase : ComponentBase
    {
        [Parameter]
        public EventCallback Clicked { get; set; }
        [Parameter]
        public string BackgroundColor { get; set; } = cc.Black.ToWebColor();
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public string StrokeWidth { get; set; } = "1px";
        [Parameter]
        public string StrokeColor { get; set; } = cc.Transparent.ToWebColor();
        [Parameter]
        public string TargetWidth { get; set; } = "";
        protected string GetSvgStyle()
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
    }
}