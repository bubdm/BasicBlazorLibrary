using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.Tabs
{
    public partial class TabBarContainer : IBarContainer<TabPage>
    {
        [Parameter]
        public string ActiveColor { get; set; } = cc.White.ToWebColor();
        [Parameter]
        public string NormalColor { get; set; } = cc.LightGray.ToWebColor(); //i think.
        [Parameter]
        public string FontSize { get; set; } = "1.2rem"; //can be whatever i need it to be.
        [Parameter]
        public string Padding { get; set; } = "10px;";
        [Parameter]
        public string BackgroundColor { get; set; } = cc.Black.ToWebColor();
    }
}