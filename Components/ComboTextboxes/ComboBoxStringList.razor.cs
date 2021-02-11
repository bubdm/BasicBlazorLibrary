using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxStringList : IAsyncDisposable
    {
        private ComboBoxService? _service;
        [Inject]
        private IJSRuntime? JS { get; set; }
        [Parameter]
        public CustomBasicList<string>? ItemList { get; set; }
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
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();


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
        //in this case, has to use this one and not the other one.


        //private string _displayText = "";

        
        
        
        private string _firstText = "";

        //has to rethink now.
        private ManuelTextBoxComponent? _text;

        public ElementReference? GetTextBox => _text!.Text;


        //decided to make this public.  therefore, the validation version can use it to focus and selectall.
        //public ElementReference? TextReference;
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
            _service = new ComboBoxService(JS!);
            _service.ArrowDown += ArrowDown;
            _service.ArrowUp += ArrowUp;
            _service.BackspacePressed += BackspacePressed;
            _scrollreference = null;
            _firstreference = null;
            _virtual = null;
            base.OnInitialized();
        }



        //for now, backspace means you have to start over again for the combo lists
        //even on the old version, it meant starting over.
        private async void BackspacePressed()
        {
            //this is a real problem now.
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
            PrivateUpdate("", true);
            StateHasChanged();
            await Task.Delay(10);
            PrivateUpdate(ItemList![_service!.ElementHighlighted], false);
            _firstText = Value;
        }

        private async Task ElementDoubleClicked(int x)
        {
            if (Style.AllowDoubleClick == false)
            {
                return; //ignore because not even allowed.
            }
            //this means act like you chose item.
            await ComboEnterPressed.InvokeAsync(); //hopefully this simple.
        }

        private async Task ElementClicked(int x)
        {
            _service!.DoHighlight(x, false);
            PrivateUpdate(ItemList![_service.ElementHighlighted], false); //might as well do this as well.
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            //await _text!.set
            await _text.Text!.Value.FocusAsync(); //i think we need this.
            //await _text!.FocusAsync(); //needs to focus on the control as well obviously.
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
                //return "white";
            }
            return Style.HighlightColor;
        }
        private string GetColorStyle(int id)
        {
            if (id != _service!.ElementHighlighted)
            {
                return ""; //because its not the correct one.
            }
            return $"background-color: {Style.HighlightColor};"; //for this, just use aqua.
        }
        protected override void OnParametersSet() //refreshes again in this case.
        {
            _service!.Reset(ItemList!.Count); //maybe this will show it needs to scroll (?)



            //Console.WriteLine("On Parameters Set");
        }

        protected override async Task OnParametersSetAsync()
        {
            if (TabIndex != -1)
            {
                return;
            }
            if (_didFirst)
            {
                string value = await _text!.GetValueAsync();
                if (value == "" && Value != "")
                {
                    await _text.SetInitValueAsync(Value);
                    var index = ItemList!.IndexOf(Value);
                    _service!.DoHighlight(index, true); //i think this was missing now.
                    _firstText = Value;
                }
                else if (Value == "" && value != "")
                {
                    //this means to reset now.
                    _firstText = "";
                    await _text!.ClearAsync();
                    _service!.Unhighlight(true); //i think.
                    //_service!.ElementScrollTo == -1;
                    PrivateUpdate("", false); //i think.
                }
            }
            else if (Value != "")
            {
                var index = ItemList!.IndexOf(Value);
                _service!.DoHighlight(index, true); //i think this was missing now.
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
                        //clear out the text.
                        await _text!.ClearAsync();
                        _firstText = ""; //i think.
                        PrivateUpdate("", false); //i think.
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
                _service!.Unhighlight(true); //i think this is better.  even if not required, then has to do anyways.
                StateHasChanged(); //this is needed
                return; //try this way.
            }
            var index = ItemList.IndexOf(item);
            _service!.DoHighlight(index, true); //i think this was missing now.
            await _text!.HighlightTextAsync(item, _firstText.Length);
            PrivateUpdate(item, false); //try this way just in case (?)
            StateHasChanged();
        }
        private bool _didFirst = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _didFirst = true;
                await _text!.SetInitValueAsync(Value); //i think this is fine because its the first time.
                _text.KeyPress = OnKeyPress;
                await _service!.InitializeAsync(_text!.Text); //this will start the listener.
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
                _scrollreference = null; //now can set to null for next time.
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