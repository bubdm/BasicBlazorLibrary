using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using System.Threading.Tasks;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboNumberLists
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
            ItemList!.ForEach(item =>
            {
                _list.Add(item.ToString());
            });
            ItemList.Sort(); //i think this needs to sort as well.
            int index = ItemList.IndexOf(Value);
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
        public CustomBasicList<int>? ItemList { get; set; }
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                if (RequiredFromList)
                {
                    return; //because not there.
                }
                bool rets = int.TryParse(value, out int aa);
                if (rets == false)
                {
                    return;
                }
                ValueChanged.InvokeAsync(aa); //i think.
                return;
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