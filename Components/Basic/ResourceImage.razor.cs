using System.Reflection;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class ResourceImage
    {
        [Parameter]
        public string Transform { get; set; } = "";
        [Parameter]
        public string Width { get; set; } = "";
        [Parameter]
        public string Height { get; set; } = "";
        [Parameter]
        public string X { get; set; } = "";
        [Parameter]
        public string Y { get; set; } = "";
        [Parameter]
        public string ID { get; set; } = ""; //this may be needed too.
        [Parameter]
        public string FileName { get; set; } = "";
        [Parameter]
        public bool Fixed { get; set; } = true; //spinners has to be false.
        [Parameter]
        public Assembly? Assembly { get; set; }
        private string GetLink()
        {
            string text;
            text = Assembly!.ResourcesBinaryTextFromFile(FileName);
            if (FileName.ToLower().EndsWith(".svg"))
            {
                return $"data:image/svg+xml;base64,{text}";
            }
            if (FileName.ToLower().EndsWith(".png") == false)
            {
                throw new CustomBasicException("Only pngs or svg are supported for now");
            }
            return $"data:image/png;base64,{text}";
        }
        protected override bool ShouldRender()
        {
            return !Fixed; //hopefully this can speed up as well since you should not have to rerender this anyways.
        }
        private string GetFinalID => $"#{ID}";
    }
}