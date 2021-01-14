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

        protected override Task OnFirstRenderAsync()
        {
            InputElement = _combo!.GetTextBox; //try this first before the others.
            return Task.CompletedTask;
        }

        //public override async Task FocusAsync()
        //{
        //    await TabContainer.FocusAndSelectAsync(_combo!.GetTextBox);
        //}

        public override Task LoseFocusAsync()
        {
            if (_value != "" && RequiredFromList && ItemList.Any(xxx => xxx == _value) == false)
            {
                _value = "";
            }
            CurrentValue = _value;
            return Task.CompletedTask;
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