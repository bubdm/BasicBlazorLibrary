using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
using BasicBlazorLibrary.Components.SimpleSearchBoxes;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterSearchGenericLists<TValue>
    {
        private SearchStringList? _search;
        private string _textDisplay = "";
        private readonly CustomBasicList<string> _list = new();



        protected override void OnInitialized()
        {
            _search = null;
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            _list.Clear();

            //looks like for the generic list, can't sort unfortunately.
            ItemList!.ForEach(item =>
            {
                _list.Add(RetrieveValue!.Invoke(item));
            });
            _list.Sort();
            int index = ItemList.IndexOf(Value!);
            if (index == -1)
            {
                _textDisplay = "";
            }
            else
            {
                _textDisplay = _list[index];
            }
        }

        [Parameter]
        public CustomBasicList<TValue> ItemList { get; set; } = new CustomBasicList<TValue>();

        //if model, then has to be required unfortunately.

        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public Func<TValue, string>? RetrieveValue { get; set; }
        [Parameter]
        public EventCallback SearchEnterPressed { get; set; }
        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                _textDisplay = ""; //i think.
                return; //because not there.
            }
            ValueChanged.InvokeAsync(ItemList![index]); //hopefully this simple (?)
        }
        //maybe no need for losefocus this time (?)
        protected override Task OnFirstRenderAsync()
        {
            InputElement = _search!.GetTextBox;
            _search.ElementFocused = () =>
            {
                TabContainer.ResetFocus(this);
            };
            return base.OnFirstRenderAsync();
        }
    }
}