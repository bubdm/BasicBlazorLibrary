using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using BasicBlazorLibrary.Components.ComboTextboxes;
using BasicBlazorLibrary.Components.SimpleSearchBoxes;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterSearchStringLists
    {
        private SearchStringList? _search;
        protected override void OnInitialized()
        {
            _value = CurrentValue; //try this way.
            _search = null;

            base.OnInitialized(); //needs this.
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        protected override void AfterCurrentChanged()
        {
            _value = CurrentValue;
        }

        protected override Task OnFirstRenderAsync()
        {
            InputElement = _search!.GetTextBox; //try this first before the others.
            _search.ElementFocused = () =>
            {
                TabContainer.ResetFocus(this);
            };
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
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public EventCallback SearchEnterPressed { get; set; }
        private string _value = "";
    }
}