using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Repeaters
{
    public partial class GridRepeater
    {
        [Parameter]
        public int TotalRows { get; set; }
        [Parameter]
        public int TotalColumns { get; set; }
        [Parameter]
        public RenderFragment<(int row, int column, int index)>? ChildContent { get; set; }
    }
}