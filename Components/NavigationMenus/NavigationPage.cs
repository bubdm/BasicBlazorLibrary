using BasicBlazorLibrary.Components.Tabs;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.NavigationMenus
{
    public class NavigationPage : TabPage
    {
        [Parameter]
        public bool ShowNavigationBar { get; set; }
    }
}