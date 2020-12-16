using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.CssGrids
{
    public partial class GridList<T>
    {
        [Parameter]
        public CustomBasicList<T> RenderList { get; set; } = new CustomBasicList<T>();

        //[Parameter]
        //public int Rows { get; set; }
        [Parameter]
        public int Columns { get; set; } = 1; //lets allow even one.  in this case, will be list a div list.
        [Parameter]
        public bool Inline { get; set; } = true;
        //[Parameter]
        //public string RowGap { get; set; } = "5px";
        [Parameter]
        public string ColumnGap { get; set; } = "5px";

        [Parameter]
        public RenderFragment<T>? ChildContent { get; set; }

        //private string GetRows => aa.RepeatAuto(Rows);
        private string GetColumns => aa.RepeatAuto(Columns);
    }
}