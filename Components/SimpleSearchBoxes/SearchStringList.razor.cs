using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBlazorLibrary.Components.SimpleSearchBoxes
{
    public partial class SearchStringList : IAsyncDisposable
    {
        //the first one will focus on strings.
        //if my idea works, then will apply to other parts.
        //will use the same combo styles though.
        //because i like how that idea works.

        private AutoCompleteService? _service;

        [Parameter]
        public CustomBasicList<string>? ItemList { get; set; }


        private CustomBasicList<string> _displayList = new(); //start out with showing 0 items.
        //i propose first showing the entire list.



        [Parameter]
        public string Value { get; set; } = "";
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; } //to support bindings.
        [Parameter]
        public EventCallback SearchEnterPressed { get; set; }

        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.

        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel(); //i like this idea.


        /// <summary>
        /// this is only used if virtualize so it knows the line height.  hint.  set to higher than fontsize or would get hosed.  this helps in margins.
        /// </summary>


        //since this is is intended to search, then i have to type a term.
        //i don't see this one requiring virtualized at all.



        [Parameter]
        public int TabIndex { get; set; } = -1;
        [Parameter]
        public string Placeholder { get; set; } = "";

        [Parameter]
        public bool RequiredTab { get; set; } = false;


        private string _firstText = ""; //maybe still needed (?)


        private ManuelTextBoxComponent? _text; //this i think is still needed.

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
            _displayList = ItemList!.ToCustomBasicList(); //start out with a copy.
            _service.Update(_displayList.Count);
            base.OnInitialized();
        }

        private async void ArrowUp()
        {
            _service!.MoveUp();
            PrivateUpdate(_displayList![_service.ElementHighlighted], false);
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            await ContinueArrowProcessesAsync();
        }
        private async void ArrowDown()
        {
            _service!.MoveDown();
            PrivateUpdate(_displayList![_service.ElementHighlighted], false);
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            await ContinueArrowProcessesAsync();
        }





        //for now, backspace means you have to start over again for the combo lists
        //even on the old version, it meant starting over.
        private async void BackspacePressed()
        {
            //for now, will clear all text.  but will change eventually.

            //this is a real problem now.
            _firstText = ""; //try to set to blank now.  well see how that goes.
            await _text!.ClearAsync();
            _displayList = ItemList!.ToCustomBasicList(); //reset now.
            //_service!.Unhighlight(true);
            PrivateUpdate(_firstText, false);
            StateHasChanged();
        }

        private async Task ContinueArrowProcessesAsync()
        {//no need to set to blank first anymore since it reaches to javascript now.
            StateHasChanged();
            await Task.Delay(10);
            PrivateUpdate(_displayList![_service!.ElementHighlighted], false);
            _firstText = Value;
        }

        private async Task ElementDoubleClicked()
        {
            if (Style.AllowDoubleClick == false)
            {
                return; //ignore because not even allowed.
            }
            //this means act like you chose item.
            await SearchEnterPressed.InvokeAsync(); //hopefully this simple.
        }
        public Action? ElementFocused { get; set; }

        private async Task ElementClicked(int x)
        {
            _service!.DoHighlight(x, false);
            PrivateUpdate(_displayList![_service.ElementHighlighted], false); //might as well do this as well.
            _firstText = Value;
            await _text!.SetTextValueAloneAsync(Value);
            //await _text!.set
            await _text.Text!.Value.FocusAsync(); //i think we need this.
            ElementFocused?.Invoke();
            //needs to let the 

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

        protected override async Task OnParametersSetAsync()
        {
            //has to attempt to do even for tabindex.  especially since otherwise, does not update when they autoupdate.


            //if (TabIndex != -1)
            //{
            //    return;
            //}
            if (_didFirst)
            {
                string value = await _text!.GetValueAsync();
                if (value == "" && Value != "" || value != Value)
                {
                    await _text.SetInitValueAsync(Value);
                    var index = _displayList!.IndexOf(Value);
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
                //else if (Value != value)
                //{
                //    await _text.SetInitValueAsync(Value);
                //    var index = ItemList!.IndexOf(Value);
                //    _service!.DoHighlight(index, true); //i think this was missing now.
                //    _firstText = Value;
                //}
            }
            else if (Value != "")
            {
                var index = _displayList!.IndexOf(Value);
                _service!.DoHighlight(index, true); //i think this was missing now.
                _firstText = Value;
            }
        }



        private async void OnKeyPress(TextModel model)
        {
            if (model.KeyPressed == "Enter")
            {
                if (SearchEnterPressed.HasDelegate == false)
                {
                    return;
                }
                string realValue = model.Value;
                if (RequiredFromList)
                {
                    if (_service!.ElementScrollTo == -1 && _service.ElementHighlighted == -1)
                    {

                        //if required, then whatever the first entry is, use it.

                        _service.DoHighlight(0, true); //scroll to top.
                        realValue = _displayList.First();
                        //await _text!.HighlightTextAsync(temps, temps.Length);
                        PrivateUpdate(realValue, false); //try this way just in case (?)
                        await _text!.SetTextValueAloneAsync(realValue);
                        StateHasChanged();


                        //clear out the text.
                        //await _text!.ClearAsync();
                        //_firstText = ""; //i think.
                        //PrivateUpdate("", false); //i think.
                        //return;
                    }
                }
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(realValue); //to double check.
                    await SearchEnterPressed.InvokeAsync();
                }
                return;
            }
            _firstText += model.KeyPressed;

            _displayList = ItemList!.Where(xxx => xxx.ToLower().Contains(_firstText.ToLower())).ToCustomBasicList();
            _service!.Update(_displayList.Count);

            await _text!.SetTextValueAloneAsync(_firstText);

            _service.Unhighlight(true); //i think we need to unhighlight while entering text.

            //var item = ItemList!.FirstOrDefault(xxx => xxx.ToLower().StartsWith(_firstText.ToLower()));
            //if (string.IsNullOrWhiteSpace(item))
            //{
            //    if (RequiredFromList)
            //    {
            //        await _text!.ClearAsync();
            //        _firstText = "";
            //        PrivateUpdate("", false);
            //    }
            //    else
            //    {
            //        PrivateUpdate(_firstText, false);
            //    }
            //    _service!.Unhighlight(true); //i think this is better.  even if not required, then has to do anyways.
            //    StateHasChanged(); //this is needed
            //    return; //try this way.
            //}
            //var index = ItemList!.IndexOf(item);
            //_service!.DoHighlight(index, true); //i think this was missing now.
            //await _text!.HighlightTextAsync(item, _firstText.Length);
            //PrivateUpdate(item, false); //try this way just in case (?)
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
                await _service.ScrollToElementAsync(_firstreference);
            }
            if (_service.NeedsToScroll && _scrollreference != null)
            {
                await _service.ScrollToElementAsync(_scrollreference);
                _scrollreference = null; //now can set to null for next time.
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