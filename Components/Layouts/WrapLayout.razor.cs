using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Layouts
{
    public partial class WrapLayout<T>
    {
        [Parameter]
        public BasicList<T> RenderList { get; set; } = new ();
        [Parameter]
        public RenderFragment<T>? ChildContent { get; set; }
        [Parameter]
        public string ColumnWidth { get; set; } = "100px"; //can be whatever you want.
        [Parameter]
        public string Margins { get; set; } = "2px";
        [Parameter]
        public string Style { get; set; } = "";
    }
}