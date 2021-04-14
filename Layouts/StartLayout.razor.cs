using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using aa = CommonBasicLibraries.BasicDataSettingsAndProcesses.UIPlatform;
namespace BasicBlazorLibrary.Layouts
{
    public partial class StartLayout
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private string ErrorMessage { get; set; } = "";
        protected bool Exited { get; set; }

        [Parameter]
        public bool ShowFriendlyError { get; set; } = false;


        private bool _messageOpened;

        private string _messageShown = "";

        private Task ClosePopupAsync()
        {
            _messageShown = "";
            _messageOpened = false;
            return Task.CompletedTask;
        }


        private async Task ShowMessageAsync(string message)
        {
            await InvokeAsync(async () =>
            {
                _messageShown = message;
                _messageOpened = true;
                StateHasChanged();
                do
                {
                    await Task.Delay(50);
                    if (_messageOpened == false)
                    {
                        return;
                    }
                } while (true);
            });
        }
        protected override void OnInitialized()
        {
            aa.ShowMessageAsync = ShowMessageAsync;
            aa.ShowSystemError = (message =>
            {
                if (ShowFriendlyError)
                {
                    aa.ShowUserErrorToast($"System Error: {message}");
                }
                else
                {
                    throw new CustomBasicException(message);
                }
            });
            //for now, keep this in.  can rethink later if necessary.  for now, desktop can't even do startlayout anyways.
            aa.ExitApp = () =>
            {
                Exited = true;
                aa.ShowSuccessToast("Should close out because you are finished with everything.  Please close out manually");
                StateHasChanged();
            };
        }
    }
}