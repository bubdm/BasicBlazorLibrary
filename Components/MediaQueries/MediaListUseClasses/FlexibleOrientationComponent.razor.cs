using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using Microsoft.AspNetCore.Components;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.MediaQueries.MediaListUseClasses
{
    public partial class FlexibleOrientationComponent
    {
        [CascadingParameter]
        private MediaQueryListComponent? MediaList { get; set; } //anybody can use it if needed anyways.
        [Parameter]
        public RenderFragment? MainContent { get; set; }
        [Parameter]
        public RenderFragment? SideContent { get; set; }
        [Parameter]
        public RenderFragment? HeaderContent { get; set; }
        private string GetColumns
        {
            get
            {
                if (HeaderContent == null)
                {
                    return aa.RepeatMaximum(2);
                }
                return $"{aa.RepeatMaximum(1)} {aa.RepeatSpreadOut(1)}";
            }
        }
        private string GetRows(bool horizontal)
        {
            if (horizontal == false)
            {
                if (HeaderContent == null)
                {
                    return aa.RepeatAuto(2);
                }
                return aa.RepeatAuto(3);
            }
            return "auto";
        }
    }
}