using BasicBlazorLibrary.BasicJavascriptClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ManuelTextBoxComponent
    {
        //this is not cascading this time.  therefore, something will reference this object and run methods on it.

        private TextBoxHelperClass? _helps;


        public ElementReference? Text;

        //private ElementReference? _element;

        [Parameter]
        public ComboStyleModel? Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public int TabIndex { get; set; } = -1;
        [Parameter]
        public string Placeholder { get; set; } = "";

        private string GetTextStyle()
        {
            return $"font-size: {Style!.FontSize}; color: {Style.TextColor};";
        }

        //i think a delegate is fine.  only the combobox will use it anyways.

        internal Action<TextModel>? KeyPress { get; set; }

        private async Task PrivateKeyPressAsync(KeyboardEventArgs keyboard)
        {
            string value = await _helps!.GetValueAsync(Text);
            TextModel text = new TextModel(keyboard.Key, value);
            KeyPress?.Invoke(text);
        }
        protected override void OnInitialized()
        {
            Text = null;
            _helps = new TextBoxHelperClass(JS!);
            base.OnInitialized();
        }
        public async Task SetInitValueAsync(string value)
        {
            await _helps!.SetInitTextAsync(Text, value);
        }
        public async Task ClearAsync()
        {
            await _helps!.ClearTextAsync(Text);
        }
        public async Task HighlightTextAsync(string value, int startAt)
        {
            await _helps!.SetNewValueAndHighlightAsync(Text, value, startAt);
        }

        public async Task SetTextValueAloneAsync(string value)
        {
            await _helps!.SetNewValueAloneAsync(Text, value);
        }
        protected override bool ShouldRender()
        {
            return false;
        }
        

    }
}