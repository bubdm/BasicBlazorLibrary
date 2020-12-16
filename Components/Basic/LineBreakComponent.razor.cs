using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class LineBreakComponent
    {
        [Parameter]
        public string TopMargin { get; set; } = "5px";
    }
}