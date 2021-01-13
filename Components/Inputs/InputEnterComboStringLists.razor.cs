using BasicBlazorLibrary.Components.ComboTextboxes;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterComboStringLists
    {
        private ComboBoxStringList? _combo;
        protected override void OnInitialized()
        {
            _combo = null;
            base.OnInitialized(); //needs this.
        }
        public override async Task FocusAsync()
        {
            await TabContainer.FocusAndSelectAsync(_combo!.TextReference);
        }
        public override void LoseFocus()
        {
            if (_value != "" && RequiredFromList && ItemList.Any(xxx => xxx == _value) == false)
            {
                _value = "";
            }
            CurrentValue = _value;
        }
        [Parameter]
        public CustomBasicList<string> ItemList { get; set; } = new CustomBasicList<string>();
        [Parameter]
        public bool RequiredFromList { get; set; } = true; //if not required, then if you enter and its not on the list, then listindex would be -1 and you can still keep typing away.
        [Parameter]
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        private string _value = "";
    }
}