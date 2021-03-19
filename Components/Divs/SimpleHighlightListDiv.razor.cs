using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

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
        }

        [Parameter]
        public string HighlightColor { get; set; } = "aqua";
        [Parameter]
        public bool HighlightFirstItem { get; set; }
        [Parameter]
        public CustomBasicList<TValue> ItemList { get; set; } = new();
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