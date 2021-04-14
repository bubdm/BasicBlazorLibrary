using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using cc = CommonBasicLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.AutoCompleteHelpers
{
    public class AutoCompleteStyleModel
    {
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor();
        public string HoverColor { get; set; } = cc.LightGray.ToWebColor();
        public string HeaderTextColor { get; set; } = cc.Black.ToWebColor();
        public string ComboTextColor { get; set; } = cc.Black.ToWebColor();
        public string HeaderBackgroundColor { get; set; } = cc.White.ToWebColor();
        public string ComboBackgroundColor { get; set; } = cc.White.ToWebColor();
        public bool AllowDoubleClick { get; set; } = false; //so if you specify true, then double click would be to select without having to hit enter
        public string Width { get; set; } = "8vw"; //can adjust the defaults as needed.
        public string Height { get; set; } = "9vh"; //can adjust the defaults as needed.
        public string FontSize { get; set; } = "1rem";
        /// <summary>
        /// this is only used if virtualize so it knows the line height.  hint.  set to higher than fontsize or would get hosed.  this helps in margins.
        /// </summary>
        public string LineHeight { get; set; } = "1.5rem";
    }
}