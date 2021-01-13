using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxEnums<TValue>
        where TValue : Enum
    {
        [Parameter]
        public TValue? Value { get; set; }
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }

        [Parameter]
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor();
        [Parameter]
        public string TextColor { get; set; } = cc.Black.ToWebColor();
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
        [Parameter]
        public int TabIndex { get; set; } = -1;
        [Parameter]
        public string Placeholder { get; set; } = "";
        [Parameter]
        public string HoverColor { get; set; } = cc.LightGray.ToWebColor();
        [Parameter]
        public string BackgroundColor { get; set; } = cc.White.ToWebColor(); //so you change this as well.
        public ElementReference? TextReference => _combo!.TextReference;

        private ComboBoxStringList? _combo;


        private string _textDisplay = "";
        private TValue FirstValue { get; set; } = default!;
        private readonly CustomBasicList<string> _list = new();
        protected override void OnInitialized()
        {
            _combo = null;
            var firsts = Enum.GetValues(typeof(TValue));
            foreach (var item in firsts)
            {
                if (item.ToString() != "None")
                {
                    _list.Add(item.ToString()!);
                }
                else
                {
                    BindConverter.TryConvertTo<TValue>(item.ToString(), CultureInfo.CurrentCulture, out var ff);
                    FirstValue = ff!;
                }
            }
            _list.Sort();
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            if (Value!.Equals(FirstValue))
            {
                _textDisplay = "";
            }
            else
            {
                _textDisplay = Value.ToString();
            }
            base.OnParametersSet();
        }
        private void TextChanged(string value)
        {
            var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
            if (success == false)
            {
                return; //don't even change our stuff.
            }
            if (parsedValue!.Equals(FirstValue))
            {
                return; //because this means you chose the default or even none.  later can do something else (?).
                //not ready to use forms yet
            }
            ValueChanged.InvokeAsync(parsedValue); //let the child recall this again.
        }
    }
}