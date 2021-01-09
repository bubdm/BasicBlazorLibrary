using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxNumberList
    {
        [Parameter]
        public CustomBasicList<int>? TextList { get; set; }
        [Parameter]
        public int Value { get; set; }
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        [Parameter]
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor();
        [Parameter]
        public string TextColor { get; set; } = cc.Black.ToWebColor();
        [Parameter]
        public bool RequiredFromList { get; set; } = true;
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
        private string _textDisplay = "";
        private readonly CustomBasicList<string> _list = new CustomBasicList<string>();
        protected override void OnParametersSet()
        {
            _list.Clear();
            TextList!.ForEach(item =>
            {
                _list.Add(item.ToString());
            });
            int index = TextList.IndexOf(Value);
            if (index == -1)
            {
                _textDisplay = "";
            }
            else
            {
                _textDisplay = _list[index];
            }
            base.OnParametersSet();
        }
        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                return; //because not there.
            }
            ValueChanged.InvokeAsync(TextList![index]); //hopefully this simple (?)
        }
    }
}