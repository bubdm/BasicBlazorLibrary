using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class MessageBoxBlazor
    {
        [Parameter]
        public string Message { get; set; } = "";
        [Parameter]
        public EventCallback CloseClicked { get; set; }
        private BasicList<string> Messages()
        {
            return Message.Split(Constants.VBLF).ToBasicList();
        }
    }
}