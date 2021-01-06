using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.Basic
{
    public class ReaderModel
    {
        //i like this way even better.
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor(); //needs to use towebcolor
        public int ElementHighlighted { get; internal set; } = 0; //this means at top.
        //i think its okay for external sources to get the values but not set.  they need to set the scroll values instead.  
        public int ElementScrollTo { get; set; }
    }
}
