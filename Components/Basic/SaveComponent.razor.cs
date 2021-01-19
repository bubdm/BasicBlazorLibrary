using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class SaveComponent
    {
        /// <summary>
        /// this is the color of the main area.  default is black
        /// </summary>
        [Parameter]
        public string BackgroundColor { get; set; } = "black";
        /// <summary>
        /// this is the color of the rectangles inside the main.  default is white.  if you want transparent, set to same color as the color in that area of the screen
        /// </summary>
        [Parameter]
        public string FillColor { get; set; } = "white"; //i think default to white but can be anything you want.
        [Parameter]
        public string TargetHeight { get; set; } = "";

        [Parameter]
        public string TargetWidth { get; set; } = "";

        [Parameter]
        public EventCallback SaveClicked { get; set; }

    }
}