namespace BasicBlazorLibrary.Components.Tabs
{
    public interface ITabContainer
    {
        void AddPage(TabPage page);
        TabPage? ActivePage { get; }
        bool CollapsePages { get; set; }
    }
}