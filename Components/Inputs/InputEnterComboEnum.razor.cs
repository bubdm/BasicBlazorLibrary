using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboEnum<TValue>
        where TValue : Enum
    {
        private readonly CustomBasicList<string> _list = new();
        private TValue FirstValue { get; set; } = default!;
        private string _textDisplay = "";
        private ComboBoxStringList? _combo;
        protected override void OnInitialized()
        {
            _combo = null;
            //attempt to do this way. and use the string one.  would be better performance.
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
        }
        [Parameter]
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        private void TextChanged(string value)
        {
            var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
            if (success == false)
            {
                _textDisplay = "";
                return; //don't even change our stuff.
            }
            if (parsedValue!.Equals(FirstValue))
            {
                _textDisplay = "";
                return; //because this means you chose the default or even none.  later can do something else (?).
                //not ready to use forms yet
            }
            ValueChanged.InvokeAsync(parsedValue); //let the child recall this again.
        }

        //maybe no need for losefocus this time (?)
        protected override Task OnFirstRenderAsync()
        {
            InputElement = _combo!.GetTextBox;
            _combo.ElementFocused = () =>
            {
                TabContainer.ResetFocus(this);
            };
            return base.OnFirstRenderAsync();
        }
       
    }
}