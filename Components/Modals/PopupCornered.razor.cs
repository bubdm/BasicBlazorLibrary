using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupCornered
    {
        [Parameter]
        public string Width { get; set; } = "40vmin"; //default to 40 percent minimum.  however, you can set whatever you want.

        [Parameter]
        public string Margins { get; set; } = "32px"; //looks like i need to allow the possibility of setting margins different.

        protected override string GetWidth => Width;
        [Parameter]
        public EnumCornerPosition CornerPosition { get; set; } = EnumCornerPosition.TopRight; //default to top right.  however, you can change if you want.
        private string GetPositionStyle()
        {
            string output = CornerPosition switch
            {
                EnumCornerPosition.BottomLeft => $"position: absolute; bottom: {Margins}; left: {Margins};",
                EnumCornerPosition.BottomRight => $"position: absolute; bottom: {Margins}; right: {Margins};",
                EnumCornerPosition.TopLeft => $"position: absolute; top: {Margins}; left: {Margins};",
                EnumCornerPosition.TopRight => $"position: absolute; top: {Margins}; right: {Margins};",
                _ => ""
            };
            return output;
        }
    }
}