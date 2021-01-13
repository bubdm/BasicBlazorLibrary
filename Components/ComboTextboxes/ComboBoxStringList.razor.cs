using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxStringList : IAsyncDisposable
    {
        private ComboBoxService? _service;
        [Inject]
        private IJSRuntime? JS { get; set; }
        [Parameter]
        public CustomBasicList<string>? TextList { get; set; }
        [Parameter]
        public string Value { get; set; } = "";
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; } //to support bindings.
        //if listindex is needed, requires rethinking.
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public string HighlightColor { get; set; } = cc.Aqua.ToWebColor();
        [Parameter]
        public string HoverColor { get; set; } = cc.LightGray.ToWebColor();
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
        public string PlaceHolder { get; set; } = "";

        private string GetId()
        {
            if (TabIndex == -1)
            {
                return "";
            }
            return TabIndex.ToString();
        }


        private VirtualSimpleComponent<string>? _virtual; //this is used so it can do the autoscroll.
        //in this case, has to use this one and not the other one.

        
        private string GetTextStyle()
        {
            return $"font-size: {FontSize}; color: {TextColor};";
        }
        private string _firstText = "";
        //decided to make this public.  therefore, the validation version can use it to focus and selectall.
        public ElementReference? TextReference;
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
            _service = new ComboBoxService(JS!);
            _service.ArrowDown += ArrowDown;
            _service.ArrowUp += ArrowUp;
            _service.BackspacePressed += BackspacePressed;
            TextReference = null;
            _scrollreference = null;
            _firstreference = null;
            _virtual = null;
            base.OnInitialized();
        }
        private void BackspacePressed()
        {
            _firstText = Value; //because nothing is highlighted anymore.
            StateHasChanged();
        }
        private async void ArrowUp()
        {
            _service!.MoveUp();
            await ContinueArrowProcessesAsync();
        }
        private async void ArrowDown()
        {
            _service!.MoveDown();
            await ContinueArrowProcessesAsync();
        }
        private async Task ContinueArrowProcessesAsync()
        {
            PrivateUpdate("", true);
            StateHasChanged();
            await Task.Delay(10);
            PrivateUpdate(TextList![_service!.ElementHighlighted], false);
            _firstText = Value;
        }
        private async void KeyPress(KeyboardEventArgs keyboard)
        {
            if (keyboard.Key == "Enter")
            {
                if (ComboEnterPressed.HasDelegate == false)
                {
                    return;
                }
                if (RequiredFromList)
                {
                    if (_service!.ElementScrollTo == -1 && _service.ElementHighlighted == -1)
                    {
                        return;
                    }
                }
                string text = Value;
                await ValueChanged.InvokeAsync(text);
                await ComboEnterPressed.InvokeAsync(); //hopefully this way is better.  if i run into problems, refer to the sample one and add extra logic.
                return;
            }
            _firstText += keyboard.Key;
            var item = TextList!.FirstOrDefault(xxx => xxx.ToLower().StartsWith(_firstText.ToLower()));
            if (string.IsNullOrWhiteSpace(item))
            {
                if (RequiredFromList)
                {
                    _firstText = ""; //i think always (?)
                    await Task.Delay(10);
                    PrivateUpdate("", false);
                }
                _service!.Unhighlight(true); //i think this is better.  even if not required, then has to do anyways.
                StateHasChanged(); //this is needed
                return; //try this way.
            }
            var index = TextList.IndexOf(item);
            _service!.DoHighlight(index, true);
            await Task.Delay(10);
            PrivateUpdate(item, false);
            await _service.PartialHighlightText(TextReference, _firstText.Length); //try this way.
        }
        private async Task ElementClicked(int x)
        {
            _service!.DoHighlight(x, false);
            PrivateUpdate(TextList![_service.ElementHighlighted], false);
            _firstText = Value;
            await TextReference!.Value.FocusAsync(); //needs to focus on the control as well obviously.
        }
        private string GetHoverColor(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return HoverColor;
            }
            return HighlightColor;
        }
        private string GetBackgroundColor(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return "white";
            }
            return HighlightColor;
        }
        private string GetColorStyle(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return ""; //because its not the correct one.
            }
            return $"background-color: {HighlightColor};"; //for this, just use aqua.
        }
        protected override void OnParametersSet() //refreshes again in this case.
        {
            _service!.Reset(TextList!.Count); //maybe this will show it needs to scroll (?)
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (TextReference == null)
            {
                return;
            }
            if (firstRender)
            {
                await _service!.InitializeAsync(TextReference); //this will start the listener.
            }
            if (_service!.NeedsToScroll && _service.ElementScrollTo == -1) //-1 means needs to scroll to top.
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
            }
            else if (Virtualized == true && _service.NeedsToScroll)
            {
                _virtual!.ScrollToSpecificElement(_service.ElementHighlighted);
            }
        }
        ValueTask IAsyncDisposable.DisposeAsync()
        {
            _service!.ArrowDown -= ArrowDown;
            _service.ArrowUp -= ArrowUp;
            _service.BackspacePressed -= BackspacePressed;
            return ValueTask.CompletedTask;
        }
    }
}