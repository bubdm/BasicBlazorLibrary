using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class NavigationBase : ComponentBase
    {
        [Inject]
        private NavigationManager? Navigates { get; set; } //should not require javascript for this part.   could override javascriptcomponentbase if necessary (hopefully not).
        protected void NavigateTo(string url)
        {
            Navigates!.NavigateTo(url); //for cases where i want to override, gives me that option as well.
        }
    }
}