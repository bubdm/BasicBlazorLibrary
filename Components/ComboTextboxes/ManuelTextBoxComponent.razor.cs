using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
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

        public async Task<string> GetValueAsync()
        {
            return await _helps!.GetValueAsync(Text);
        }

        internal Action<TextModel>? KeyPress { get; set; }

        private async void PrivateKeyPress(string key)
        {
            //this time, has to append
            string value = await _helps!.GetValueAsync(Text);
            if (key != "Enter")
            {
                value = $"{value}{key}"; //has to add
            }



            TextModel text = new TextModel(key, value);
            KeyPress?.Invoke(text);
        }

        //private async Task PrivateKeyPressAsync(KeyboardEventArgs keyboard)
        //{
        //    string value = await _helps!.GetValueAsync(Text);
        //    TextModel text = new TextModel(keyboard.Key, value);
            
        //    KeyPress?.Invoke(text);
        //    await SetTextValueAloneAsync("Hello"); //try this way.
        //}
        protected override void OnInitialized()
        {
            Text = null;
            _helps = new TextBoxHelperClass(JS!);
            _helps.OnKeyPress += PrivateKeyPress;
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
            //await _helps!.SetNewValueAloneAsync(Text, "Eastern");
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
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _helps!.StartAsync(Text);
            }
            
        }

    }
}