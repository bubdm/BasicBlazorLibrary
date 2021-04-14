using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class SimpleHighlightListDiv<TValue>
    {
        protected override void OnInitialized()
        {
            if (HighlightFirstItem)
            {
                _elementHighlighted = 0; //do this way only.
            }
            if (PreviouslyHighlighted is not null)
            {
                _elementHighlighted = ItemList.IndexOf(PreviouslyHighlighted);
                if (_elementHighlighted == -1)
                {
                    throw new CustomBasicException("You cannot previously hightlight an item that does not exist.");
                }
            }
        }
        [Parameter]
        public string HighlightColor { get; set; } = "aqua";
        [Parameter]
        public bool HighlightFirstItem { get; set; }
        /// <summary>
        /// this has to be set at the beginning.  will allow something other than the first item to be highlighted.
        /// </summary>
        [Parameter]
        public TValue? PreviouslyHighlighted { get; set; }
        [Parameter]
        public BasicList<TValue> ItemList { get; set; } = new();
        [Parameter]
        public RenderFragment<TValue>? ChildContent { get; set; }
        //maybe no need for scrolling because something else should handle it anyways.
        //take that risk (may use with my leftover component).
        [Parameter]
        public EventCallback<TValue> OnItemSelected { get; set; }
        private int _elementHighlighted = -1; //start out with nothing highlighted.
        private string GetColorStyle(int id)
        {
            if (id != _elementHighlighted)
            {
                return ""; //because its not the correct one.
            }
            return $"background-color: {HighlightColor};";
        }
        private void ElementClicked(int x)
        {

            _elementHighlighted = x; //not sure if we need statehaschanged (?)
            OnItemSelected.InvokeAsync(ItemList[x]);
        }
    }
}