using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboGenericLists<TValue>
    {
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
            ItemList.Sort(); //i think this too.
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
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public Func<TValue, string>? RetrieveValue { get; set; }

        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                return; //because not there.
            }
            ValueChanged.InvokeAsync(ItemList![index]); //hopefully this simple (?)
        }
        //maybe no need for losefocus this time (?)

        public override async Task FocusAsync()
        {
            await TabContainer.FocusAndSelectAsync(_combo!.TextReference);
        }
    }
}