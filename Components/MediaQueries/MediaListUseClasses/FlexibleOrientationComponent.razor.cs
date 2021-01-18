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
        private static string GetColumns => aa.RepeatMaximum(2);
    }
}