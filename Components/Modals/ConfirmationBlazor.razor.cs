using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class ConfirmationBlazor
    {
        [Parameter]
        public string Title { get; set; } = "";
        [Parameter]
        public string Message { get; set; } = "";
        /// <summary>
        /// true means that you do want to do the action.
        /// false means you do not.
        /// </summary>
        [Parameter]
        public EventCallback<bool> Results { get; set; }
        private async Task DoConfirm()
        {
            await Results.InvokeAsync(true);
        }
        private async Task Cancel()
        {
            await Results.InvokeAsync(false);
        }
    }
}