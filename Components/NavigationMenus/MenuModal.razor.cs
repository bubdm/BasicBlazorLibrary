using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.NavigationMenus
{
    public partial class MenuModal
    {
        [Parameter]
        public string BackgroundColor { get; set; } = cc.Black.ToWebColor();
        [Parameter]
        public string TextColor { get; set; } = cc.White.ToWebColor();
        [Parameter]
        public string FontSize { get; set; } = "1.5rem";
        [Parameter]
        public string Height { get; set; } = "300px";
        [Parameter]
        public string Width { get; set; } = "50vmin";
        [Parameter]
        public BasicList<MenuItem> MenuList { get; set; } = new ();
        private void ClickMenu(MenuItem menu)
        {
            ClosePopup();
            menu.Clicked.Invoke();
        }
        private string GetMainStyle()
        {
            return $"font-size: {FontSize}; color: {TextColor}; justify-content: center; white-space: nowrap;";
        }
    }
}