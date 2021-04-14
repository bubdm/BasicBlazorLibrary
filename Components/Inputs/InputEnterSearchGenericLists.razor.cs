using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.SimpleSearchBoxes;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterSearchGenericLists<TValue>
    {
        private SearchStringList? _search;
        private string _textDisplay = "";
        private readonly BasicList<string> _list = new();
        protected override void OnInitialized()
        {
            _search = null;
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            _list.Clear();
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
        public BasicList<TValue> ItemList { get; set; } = new();
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
                _textDisplay = "";
                return;
            }
            ValueChanged.InvokeAsync(ItemList![index]);
        }
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