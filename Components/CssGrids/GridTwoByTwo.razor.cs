using Microsoft.AspNetCore.Components;
using aa = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BasicBlazorLibrary.Components.CssGrids
{
    public partial class GridTwoByTwo
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// If set to true then the layout of the grid will be "inline" instead of stretching to fill the container.
        /// </summary>
        /// <value>Default is false</value>
        [Parameter]
        public bool Inline { get; set; }
        [Parameter]
        public string RowGap { get; set; } = "";
        [Parameter]
        public string ColumnGap { get; set; } = "";
        [Parameter]
        public string Class { get; set; } = ""; //so you can add other parts to this.
        [Parameter]
        public string Style { get; set; } = "";
        private static string Get2SpreadContentEntries()
        {
            return $"{aa.OneSpread} {aa.OneSpread}";
        }
        private static string Get2AutoContentEntries()
        {
            return $"{aa.Auto} {aa.Auto}";
        }
    }
}