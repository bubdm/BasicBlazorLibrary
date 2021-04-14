using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboNumberLists
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
                _list.Add(item.ToString());
            });
            ItemList.Sort(); //i think this needs to sort as well.
            int index = ItemList.IndexOf(Value);
            if (index == -1 && RequiredFromList)
            {
                _textDisplay = "";
            }
            else if (index == -1 && Value == 0)
            {
                _textDisplay = "";
            }
            else if (index == -1)
            {
                _textDisplay = Value.ToString();
            }
            else
            {
                _textDisplay = _list[index];
            }
        }
        [Parameter]
        public BasicList<int>? ItemList { get; set; }
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                if (RequiredFromList)
                {
                    _textDisplay = "";
                    return;
                }
                bool rets = int.TryParse(value, out int aa);
                if (rets == false)
                {
                    _textDisplay = "";
                    return;
                }
                ValueChanged.InvokeAsync(aa);
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