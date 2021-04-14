using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public abstract partial class PopupContentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string HeaderTitle { get; set; } = "";
        [Parameter]
        public string Style { get; set; } = ""; 
        [Parameter]
        public EventCallback CloseButtonClick { get; set; }
        [Parameter]
        public bool DisableBackgroundClick { get; set; } = true; 
        [Parameter]
        public bool ShowHeader { get; set; } = true;
        [Parameter]
        public RenderFragment? HeaderTemplate { get; set; }
        [Parameter]
        public RenderFragment? FooterTemplate { get; set; }
        [Parameter]
        public RenderFragment? Content { get; set; }
        [Parameter]
        public bool ShowCloseButton { get; set; }
        [Parameter]
        public bool Scrollable { get; set; } = true;
        [Parameter]
        public string Height { get; set; } = "";
        protected abstract string GetWidth { get; }
        [Parameter]
        public bool DisableParentClickThrough { get; set; } = true;
        [Parameter]
        public bool FullScreen { get; set; } = true;
        [Parameter]
        public string BackgroundColor { get; set; } = "white";
        [Parameter]
        public string BackgroundImage { get; set; } = "";
        [Parameter]
        public string HeaderColor { get; set; } = "black";
        private string GetBackgrounds()
        {
            if (BackgroundImage != "")
            {
                return $"background-image: url('{BackgroundImage}'); background-size: 100% 100%;";
            }
            return $"background-color: {BackgroundColor};";
        }
        protected string GetHiddenStyle()
        {
            if (Visible == true)
            {
                return "";
            }
            return "display: none;";
        }
        protected virtual bool ProtectedHiddenFull => DisableParentClickThrough;
        protected string TopModalStyle()
        {
            if (FullScreen)
            {
                return $"display: flex;  top: 0; left: 0; width: 100%; height: 100%; position: fixed; z-index: {ZIndex}; background-color: rgba(0,0,0,0.5); {GetHiddenStyle()}";
            }
            if (ProtectedHiddenFull)
            {
                return $"display: flex;  top: 0; left: 0; width: 100%; height: 100%; position: fixed; z-index: {ZIndex}; background-color: transparent; {GetHiddenStyle()}";
            }
            return $"display: flex;  z-index: {ZIndex};  {GetHiddenStyle()}";
        }
        protected string ScrollStyle()
        {
            if (Scrollable == false)
            {
                return "";
            }
            return "overflow: auto;";
        }
        protected override void ClosePopup()
        {
            if (CloseButtonClick.HasDelegate)
            {
                CloseButtonClick.InvokeAsync();
                return;
            }
            base.ClosePopup();
        }
        protected void PrivateBackgroundClicked()
        {
            if (DisableBackgroundClick)
            {
                return;
            }
            ClosePopup(); //do this instead.
        }
    }
}