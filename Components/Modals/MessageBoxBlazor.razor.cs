using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class MessageBoxBlazor
    {
        [Parameter]
        public string Message { get; set; } = "";
        [Parameter]
        public EventCallback CloseClicked { get; set; }
        private CustomBasicList<string> Messages()
        {
            return Message.Split(Constants.vbLf).ToCustomBasicList();
        }
    }
}