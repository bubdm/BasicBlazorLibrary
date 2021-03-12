using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using Microsoft.AspNetCore.Components;
using aa = CommonBasicStandardLibraries.MVVMFramework.UIHelpers.ToastPlatform;
using Microsoft.JSInterop;
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


        //attempt to not have this inherit from layoutbase (?)
        //in this case, needs childcontent (?)

        protected override void OnInitialized()
        {

            //if you are doing messages for lots of people, doing it this way does not work well.

            //i liked the way it was done for the game package.  attempt it here.
            UIPlatform.ShowMessageAsync = ShowMessageAsync;
            UIPlatform.ShowError = (message =>
            {
                if (ShowFriendlyError)
                {
                    aa.ShowError($"System Error: {message}");
                    //ErrorMessage = message;
                    //StateHasChanged();
                }
                else
                {
                    throw new BasicBlankException(message);
                }
            });
            //in .net 6 or maybe even with photino, if the method is already implemented, then can ignore this part.

            UIPlatform.ExitApp = () =>
            {
                Exited = true;
                aa.ShowSuccess("Should close out because you are finished with everything.  Please close out manually");
                //try this way.
                StateHasChanged();
            };

        }


    }
}