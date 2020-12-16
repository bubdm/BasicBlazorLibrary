using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Basic
{
    public partial class WrapControl<T>
    {
        [Parameter]
        public CustomBasicList<T> RenderList { get; set; } = new CustomBasicList<T>();
        [Parameter]
        public RenderFragment<T>? ChildContent { get; set; }
        [Parameter]
        public string ColumnWidth { get; set; } = "100px"; //can be whatever you want.
    }
}