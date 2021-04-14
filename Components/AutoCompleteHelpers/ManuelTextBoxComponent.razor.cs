using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using cc = CommonBasicLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.AutoCompleteHelpers
{
    public partial class ManuelTextBoxComponent
    {
        //this is not cascading this time.  therefore, something will reference this object and run methods on it.
        private TextBoxHelperClass? _helps;
        public ElementReference? Text;
        [Parameter]
        public AutoCompleteStyleModel? Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public int TabIndex { get; set; } = -1;
        [Parameter]
        public string Placeholder { get; set; } = "";
        private string GetTextStyle()
        {
            return $"font-size: {Style!.FontSize}; color: {Style.HeaderTextColor};";
        }
        //i think a delegate is fine. only one control will use it anyways.  this is used for now the combo or the simple search.  however, there can be other autocomplete boxes in the future.
        private string GetTextBackgroundColor => Style!.HeaderBackgroundColor == cc.Transparent.ToWebColor() ? "inherit" : Style.HeaderBackgroundColor;
        public async Task<string> GetValueAsync()
        {
            return await _helps!.GetValueAsync(Text);
        }
        public Action<TextModel>? KeyPress { get; set; }
        private async void PrivateKeyPress(string key)
        {
            //this time, has to append
            string value = await _helps!.GetValueAsync(Text);
            if (key != "Enter")
            {
                value = $"{value}{key}"; //has to add
            }



            TextModel text = new(key, value);
            KeyPress?.Invoke(text);
        }
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