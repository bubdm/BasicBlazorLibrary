using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupCornered
    {
        [Parameter]
        public string Width { get; set; } = "40vmin";
        [Parameter]
        public string Margins { get; set; } = "32px";
        protected override string GetWidth => Width;
        [Parameter]
        public EnumCornerPosition CornerPosition { get; set; } = EnumCornerPosition.TopRight;
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