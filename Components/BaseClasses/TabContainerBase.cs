using BasicBlazorLibrary.Components.Tabs;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class TabContainerBase<T> : ComponentBase, ITabContainer
        where T: TabPage
    {
        public T? ActivePage { get; set; }
        public CustomBasicList<T> Pages { get; set; } = new CustomBasicList<T>(); //has to be public so a helper can use it.
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        TabPage? ITabContainer.ActivePage => _page;
        [Parameter]
        public bool CollapsePages { get; set; }

        protected void AddPage(T tabPage)
        {
            Pages.Add(tabPage);
            if (Pages.Count == 1)
            {
                ActivatePage(tabPage); //i think
            }
            StateHasChanged();
        }

        TabPage? _page;

        public virtual void ActivatePage(T page) //so the navigation class would do basics plus new.
        {
            _page = page;
            ActivePage = page;
        }


        void ITabContainer.AddPage(TabPage page)
        {
            AddPage((T) page); //unfortunately needs to cast it.  should not be too bad on performance though.
        }
    }
}
