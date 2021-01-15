using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxGenericList<TValue>
    {
        [Parameter]
        public CustomBasicList<TValue>? ItemList { get; set; }
        [Parameter]
        public TValue? Value { get; set; }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Func<TValue, string>? RetrieveValue { get; set; }

        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }

        [Parameter]
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public string Placeholder { get; set; } = "";
       
        public ElementReference? TextReference => _combo!.GetTextBox;

        private ComboBoxStringList? _combo;
        private string _textDisplay = "";
        private readonly CustomBasicList<string> _list = new CustomBasicList<string>();
        protected override void OnInitialized()
        {
            _combo = null;
        }
        protected override void OnParametersSet()
        {
            _list.Clear();
            ItemList!.ForEach(item =>
            {
                _list.Add(RetrieveValue!.Invoke(item));
            });
            int index = ItemList.IndexOf(Value!);
            if (index == -1)
            {
                _textDisplay = "";
            }
            else
            {
                _textDisplay = _list[index];
            }
            base.OnParametersSet();
        }
        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                _textDisplay = "";
                return; //because not there.
            }
            ValueChanged.InvokeAsync(ItemList![index]); //hopefully this simple (?)
        }
    }
}