using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupCornered
    {
        [Parameter]
        public string Width { get; set; } = "40vmin"; //default to 40 percent minimum.  however, you can set whatever you want.
        protected override string GetWidth => Width;
        [Parameter]
        public EnumCornerPosition CornerPosition { get; set; } = EnumCornerPosition.TopRight; //default to top right.  however, you can change if you want.
        private string GetPositionStyle()
        {
            string output = CornerPosition switch
            {
                EnumCornerPosition.BottomLeft => "position: absolute; bottom: 32px; left: 32px;",
                EnumCornerPosition.BottomRight => "position: absolute; bottom: 32px; right: 32px;",
                EnumCornerPosition.TopLeft => "position: absolute; top: 32px; left: 32px;",
                EnumCornerPosition.TopRight => "position: absolute; top: 32px; right: 32px;",
                _ => ""
            };
            return output;
        }
    }
}