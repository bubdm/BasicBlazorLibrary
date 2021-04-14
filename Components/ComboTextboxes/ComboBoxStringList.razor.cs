using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxStringList : IAsyncDisposable
    {
        private AutoCompleteService? _service;
        [Inject]
        private IJSRuntime? JS { get; set; }
        [Parameter]
        public BasicList<string>? ItemList { get; set; }
        private string _value = "";
        [Parameter]
        public string Value //may have to do this way now.
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    UpdateValuesAsync();
                }
            }
        }
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; } //to support bindings.
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        /// <summary>
        /// this is only used if virtualize so it knows the line height.  hint.  set to higher than fontsize or would get hosed.  this helps in margins.
        /// </summary>
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public int TabIndex { get; set; } = -1;
        [Parameter]
        public string Placeholder { get; set; } = "";
        [Parameter]
        public bool RequiredTab { get; set; } = false;
        private VirtualSimpleComponent<string>? _virtual; //this is used so it can do the autoscroll.
        private string _firstText = "";
        private ManuelTextBoxComponent? _text;
        public ElementReference? GetTextBox => _text!.Text;
        private ElementReference? _scrollreference;
        private ElementReference? _firstreference;
        private void PrivateUpdate(string value, bool laternewValue)
        {
            if (value == Value)
            {
                return; //to try to stop the never ending loop problem.
            }
            Value = value;
            if (laternewValue)
            {
                return;
            }
            ValueChanged.InvokeAsync(value); //i think.
        }
        protected override void OnInitialized()
        {
            _text = null;
            _service = new AutoCompleteService(JS!);
            _service.ArrowDown += ArrowDown;
            _service.ArrowUp += ArrowUp;
            _service.BackspacePressed += BackspacePressed;
            _scrollreference = null;
            _firstreference = null;
            _virtual = null;
            base.OnInitialized();
        }
        private async void BackspacePressed()
        {
            _firstText = ""; //try to set to blank now.  well see how that goes.
            await _text!.ClearAsync();
            PrivateUpdate(_firstText, false);
            StateHasChanged();
        }
        private async void ArrowUp()
        {
            _service!.MoveUp();
            PrivateUpdate(ItemList![_service.ElementHighlighted], false);
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            await ContinueArrowProcessesAsync();
        }
        private async void ArrowDown()
        {
            _service!.MoveDown();
            PrivateUpdate(ItemList![_service.ElementHighlighted], false);
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            await ContinueArrowProcessesAsync();
        }
        private async Task ContinueArrowProcessesAsync()
        {
            StateHasChanged();
            await Task.Delay(10);
            PrivateUpdate(ItemList![_service!.ElementHighlighted], false);
            _firstText = Value;
        }
        private async Task ElementDoubleClicked()
        {
            if (Style.AllowDoubleClick == false)
            {
                return;
            }
            await ComboEnterPressed.InvokeAsync();
        }
        public Action? ElementFocused { get; set; }
        private async Task ElementClicked(int x)
        {
            _service!.DoHighlight(x, false);
            PrivateUpdate(ItemList![_service.ElementHighlighted], false);
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            await _text.Text!.Value.FocusAsync(); //i think we need this.
            ElementFocused?.Invoke();
        }
        private string GetTextStyle()
        {
            return $"font-size: {Style!.FontSize}; color: {Style.ComboTextColor};";
        }
        private string GetHoverColor(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return Style.HoverColor;
            }
            return Style.HighlightColor;
        }
        private string GetBackgroundColor(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return Style.ComboBackgroundColor;
            }
            return Style.HighlightColor;
        }
        private string GetColorStyle(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return "";
            }
            return $"background-color: {Style.HighlightColor};";
        }
        protected override void OnParametersSet()
        {
            _service!.Update(ItemList!.Count);
        }
        private async void UpdateValuesAsync()
        {
            if (_didFirst)
            {
                string value = await _text!.GetValueAsync();
                if (value == "" && Value != "" || value != Value)
                {
                    await _text.SetInitValueAsync(Value);
                    var index = ItemList!.IndexOf(Value);
                    _service!.DoHighlight(index, true);
                    _firstText = Value;
                }
                else if (Value == "" && value != "")
                {
                    _firstText = "";
                    await _text!.ClearAsync();
                    _service!.Unhighlight(true);
                    PrivateUpdate("", false);
                }
            }
            else if (Value != "")
            {
                var index = ItemList!.IndexOf(Value);
                _service!.DoHighlight(index, true);
                _firstText = Value;
            }
        }
        private async void OnKeyPress(TextModel model)
        {
            if (model.KeyPressed == "Enter")
            {
                if (ComboEnterPressed.HasDelegate == false)
                {
                    return;
                }
                if (RequiredFromList)
                {
                    if (_service!.ElementScrollTo == -1 && _service.ElementHighlighted == -1)
                    {
                        await _text!.ClearAsync();
                        _firstText = "";
                        PrivateUpdate("", false);
                        return;
                    }
                }
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(model.Value);
                    await ComboEnterPressed.InvokeAsync();
                }
                return;
            }
            _firstText += model.KeyPressed;
            var item = ItemList!.FirstOrDefault(xxx => xxx.ToLower().StartsWith(_firstText.ToLower()));
            if (string.IsNullOrWhiteSpace(item))
            {
                if (RequiredFromList)
                {
                    await _text!.ClearAsync();
                    _firstText = "";
                    PrivateUpdate("", false);
                }
                else
                {
                    PrivateUpdate(_firstText, false);
                }
                _service!.Unhighlight(true);
                StateHasChanged();
                return;
            }
            var index = ItemList!.IndexOf(item);
            _service!.DoHighlight(index, true);
            await _text!.HighlightTextAsync(item, _firstText.Length);
            PrivateUpdate(item, false);
            StateHasChanged();
        }
        private bool _didFirst = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _didFirst = true;
                await _text!.SetInitValueAsync(Value);
                _text.KeyPress = OnKeyPress;
                await _service!.InitializeAsync(_text!.Text);
            }
            if (_service!.NeedsToScroll && _service.ElementScrollTo == -1)
            {
                if (Virtualized == false)
                {
                    await _service.ScrollToElementAsync(_firstreference);
                }
                else
                {
                    _virtual!.ScrollToSpecificElement(0); //i think.
                }
            }
            if (Virtualized == false && _service.NeedsToScroll && _scrollreference != null)
            {
                await _service.ScrollToElementAsync(_scrollreference);
                _scrollreference = null;
            }
            else if (Virtualized == true && _service.NeedsToScroll)
            {
                _virtual!.ScrollToSpecificElement(_service.ElementHighlighted);
            }
        }
        ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_service == null)
            {
                return ValueTask.CompletedTask;
            }
            _service.ArrowDown -= ArrowDown;
            _service.ArrowUp -= ArrowUp;
            _service.BackspacePressed -= BackspacePressed;
            return ValueTask.CompletedTask;
        }
    }
}