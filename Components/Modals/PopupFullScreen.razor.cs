using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupFullScreen
    {
        [Parameter]
        public string BackgroundColor { get; set; } = "white";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Style { get; set; } = "";

        //the user of it can obviously can do logic to make it not show.
        private string GetFirstClass()
        {
            if (Visible == true)
            {
                return "";
            }
            return "hidden";
        }
    }
}