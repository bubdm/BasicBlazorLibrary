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

        //found out there was a workaround to get css isolation to work in this case.

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
                //has to do based on any of them.
                StringBuilder builds = new StringBuilder();
                builds.Append(temps);
                //StrCat cats = new StrCat();
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