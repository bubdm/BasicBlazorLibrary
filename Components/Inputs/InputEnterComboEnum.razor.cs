using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboEnum<TValue>
        where TValue : Enum
    {
        private readonly BasicList<string> _list = new();
        private TValue FirstValue { get; set; } = default!;
        private string _textDisplay = "";
        private ComboBoxStringList? _combo;
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
        }
        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
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
                return;
            }
            if (parsedValue!.Equals(FirstValue))
            {
                _textDisplay = "";
                return;
            }
            ValueChanged.InvokeAsync(parsedValue); 
        }
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