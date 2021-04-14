using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Tabs
{
    public partial class TabPage
    {
        [CascadingParameter]
        private ITabContainer? TabContainer { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private string GetDisplay => TabContainer!.ActivePage == this ? "" : "none";
        [Parameter]
        public string Text { get; set; } = "";
        protected override void OnInitialized()
        {
            if (TabContainer == null)
            {
                throw new CustomBasicException("Needs tab container");
            }
            base.OnInitialized();
            TabContainer.AddPage(this);
        }
    }
}