using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Tabs
{
    internal interface IBarContainer<T> : ITabContainer //make internal to avoid confusion.
        where T : TabPage
    {
        string ActiveColor { get; set; }
        string NormalColor { get; set; }
        string FontSize { get; set; }
        string Padding { get; set; }
        string BackgroundColor { get; set; }
        void ActivatePage(T page);
        RenderFragment? ChildContent { get; set; }
    }
}
