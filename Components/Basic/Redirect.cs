using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public class Redirect : ComponentBase
    {
        [Inject]
        private NavigationManager? Navigates { get; set; }
        [Parameter]
        public string Url { get; set; } = "";
        protected override void OnInitialized()
        {
            Navigates!.NavigateTo(Url);
        }
    }
}