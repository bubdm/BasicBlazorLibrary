using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class ScrollableTableDiv<TValue>
    {
        //can't use reflection because too slow.
        //the only workaround is when i can figure out source generators.
        //this would be a good use of source generators.
        [Parameter]
        public BasicList<string> Headers { get; set; } = new();
        [Parameter]
        public string Height { get; set; } = "730px"; //default to 730 pixels.  but i can change it if necessary.
        [Parameter]
        public string BackgroundColor { get; set; } = "White";
        [Parameter]
        public BasicList<TValue> ItemList { get; set; } = new();
        [Parameter]
        public RenderFragment<TValue>? ChildContent { get; set; }
    }
}