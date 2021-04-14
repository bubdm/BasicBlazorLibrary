using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class MobileListBox<TValue>
    {
        [Parameter]
        public BasicList<TValue> Data { get; set; } = new();
        [Parameter]
        public string FontSize { get; set; } = "7vmin"; //to make it easy to choose something.
        [Parameter]
        public EventCallback<TValue> OnItemSelected { get; set; }
        [Parameter]
        public RenderFragment<TValue>? ChildContent { get; set; }
    }
}