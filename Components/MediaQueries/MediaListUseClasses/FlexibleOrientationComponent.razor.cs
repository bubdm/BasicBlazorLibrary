using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using Microsoft.AspNetCore.Components;
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
    }
}