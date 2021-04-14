using Microsoft.AspNetCore.Components;
using System.Text;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class MarginDiv
    {
        [Parameter]
        public string Margin { get; set; } = "5px"; //means all around
        [Parameter]
        public string TopMargin { get; set; } = "";
        [Parameter]
        public string BottomMargin { get; set; } = "";
        [Parameter]
        public string LeftMargin { get; set; } = "";
        [Parameter]
        public string RightMargin { get; set; } = "";
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Style { get; set; } = "";
        [Parameter]
        public string Class { get; set; } = "";
        private string GetStyle()
        {
            string temps = Style;
            if (temps != "" && temps.EndsWith(";") == false)
            {
                temps = $"{temps};";
            }
            if (TopMargin != "" || BottomMargin != "" || LeftMargin != "" || RightMargin != "")
            {
                StringBuilder builds = new StringBuilder();
                builds.Append(temps);
                if (TopMargin != "")
                {
                    builds.Append($"margin-top: {TopMargin};");
                }
                if (BottomMargin != "")
                {
                    builds.Append($"margin-bottom: {BottomMargin};");
                }
                if (LeftMargin != "")
                {
                    builds.Append($"margin-left: {LeftMargin}");
                }
                if (RightMargin != "")
                {
                    builds.Append($"margin-right: {RightMargin}");
                }
                return builds.ToString();
            }
            return $"{temps} margin: {Margin}";
        }
    }
}