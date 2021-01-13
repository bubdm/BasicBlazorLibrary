using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using System.Threading.Tasks;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboStringLists
    {
        private ComboBoxStringList? _combo;
        protected override void OnInitialized()
        {
            _combo = null;
            base.OnInitialized(); //needs this.
        }
        public override async Task FocusAsync()
        {
            await TabContainer.FocusAndSelectAsync(_combo!.TextReference);
        }
        public override void LoseFocus()
        {
            if (_value != "" && RequiredFromList && TextList.Any(xxx => xxx == _value) == false)
            {
                _value = "";
            }
            CurrentValue = _value;
        }
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            throw new BasicBlankException("No need to try to parse from string because i am doing something different here");
        }
        [Parameter]
        public CustomBasicList<string> TextList { get; set; } = new CustomBasicList<string>();
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor();
        [Parameter]
        public string HoverColor { get; set; } = cc.LightGray.ToWebColor();
        [Parameter]
        public string TextColor { get; set; } = cc.Black.ToWebColor();
        [Parameter]
        public string BackgroundColor { get; set; } = cc.White.ToWebColor(); //so you change this as well.
        [Parameter]
        public string Width { get; set; } = "8vw"; //can adjust the defaults as needed.
        [Parameter]
        public string Height { get; set; } = "9vh"; //can adjust the defaults as needed.
        [Parameter]
        public string FontSize { get; set; } = "1rem";
        /// <summary>
        /// this is only used if virtualize so it knows the line height.  hint.  set to higher than fontsize or would get hosed.  this helps in margins.
        /// </summary>
        [Parameter]
        public string LineHeight { get; set; } = "1.5rem";
        [Parameter]
        public bool Virtualized { get; set; } = false;
        private string _value = "";
    }
}