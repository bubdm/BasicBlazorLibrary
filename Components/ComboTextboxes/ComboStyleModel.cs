using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public class ComboStyleModel
    {
        //decided its best to put into the style.
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor();
        public string HoverColor { get; set; } = cc.LightGray.ToWebColor();
        public string TextColor { get; set; } = cc.Black.ToWebColor();
        public string Width { get; set; } = "8vw"; //can adjust the defaults as needed.
        public string Height { get; set; } = "9vh"; //can adjust the defaults as needed.
        public string FontSize { get; set; } = "1rem";
        /// <summary>
        /// this is only used if virtualize so it knows the line height.  hint.  set to higher than fontsize or would get hosed.  this helps in margins.
        /// </summary>
        public string LineHeight { get; set; } = "1.5rem";
        public string BackgroundColor { get; set; } = cc.White.ToWebColor(); //so you change this as well.
    }
}
