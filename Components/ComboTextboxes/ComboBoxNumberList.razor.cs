using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxNumberList
    {
        [Parameter]
        public CustomBasicList<int>? ItemList { get; set; }
        [Parameter]
        public int Value { get; set; }
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        [Parameter]
        public ComboStyleModel Style { get; set; } = new ComboStyleModel();
        [Parameter]
        public bool RequiredFromList { get; set; } = true;
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
                _list.Add(item.ToString());
            });
            int index = ItemList.IndexOf(Value);
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
                if (RequiredFromList)
                {
                    _textDisplay = "";
                    return; //because not there.
                }
                bool rets = int.TryParse(value, out int aa);
                if (rets == false)
                {
                    _textDisplay = "";
                    return;
                }
                ValueChanged.InvokeAsync(aa); //i think.
                return;
            }
            ValueChanged.InvokeAsync(ItemList![index]); //hopefully this simple (?)
        }
    }
}