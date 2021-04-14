namespace BasicBlazorLibrary.Components.Tabs
{
    public partial class TabButtonContainer
    {
        private string GetButtonClass(TabPage page)
        {
            return page == ActivePage ? "btn-primary" : "btn-secondary";
        }
    }
}