using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class ScrollableTableDiv<TValue>
    {
        //can't use reflection because too slow.
        //the only workaround is when i can figure out source generators.
        //this would be a good use of source generators.



        [Parameter]
        public CustomBasicList<string> Headers { get; set; } = new();
        //private CustomBasicList<string> _headers = new(); //these are the headers.
        [Parameter]
        public string Height { get; set; } = "730px"; //default to 730 pixels.  but i can change it if necessary.
        //the purpose of this would be to have a workaround of the fact it cannot accurately calculate where it should be at.
        
        //if i ever was able to do a bid request, can do so.  until then has to do this way.

        //don't worry about width for now.
        [Parameter]
        public string BackgroundColor { get; set; } = "White";
        [Parameter]
        public CustomBasicList<TValue> ItemList { get; set; } = new();
        [Parameter]
        public RenderFragment<TValue>? ChildContent { get; set; }
    }
}