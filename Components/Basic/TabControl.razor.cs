using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class TabControl
    {
        public TabPage? ActivePage { get; set; }
        protected CustomBasicList<TabPage> Pages = new CustomBasicList<TabPage>();
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        protected internal void AddPage(TabPage tabPage)
        {
            Pages.Add(tabPage);
            if (Pages.Count == 1)
            {
                ActivePage = tabPage;
            }
            StateHasChanged();
        }
        protected string GetButtonClass(TabPage page)
        {
            return page == ActivePage ? "btn-primary" : "btn-secondary";
        }
        protected void ActivatePage(TabPage page)
        {
            ActivePage = page;
        }
    }
}