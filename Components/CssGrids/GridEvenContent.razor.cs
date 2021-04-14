using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.CssGrids
{
    public partial class GridEvenContent
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
        public int Rows { get; set; }
        [Parameter]
        public int Columns { get; set; }
        [Parameter]
        public string Class { get; set; } = ""; //so you can add other parts to this.
        [Parameter]
        public string Style { get; set; } = "";
        private string RowText => Rows.RepeatSpreadOut(); //try to make it even.  well see how this works.
        private string ColumnText => Columns.RepeatSpreadOut(); //try to make it even using 1fr.
    }
}