using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboGenericLists<TValue>
    {
        private ComboBoxStringList? _combo;
        private string _textDisplay = "";
        private readonly BasicList<string> _list = new ();
        protected override void OnInitialized()
        {
            _combo = null;
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
        public BasicList<TValue> ItemList { get; set; } = new ();
        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public Func<TValue, string>? RetrieveValue { get; set; }
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
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
            InputElement = _combo!.GetTextBox;
            _combo.ElementFocused = () =>
            {
                TabContainer.ResetFocus(this);
            };
            return base.OnFirstRenderAsync();
        }
    }
}