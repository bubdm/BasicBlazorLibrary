using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using System;
using System.Linq;
using System.Net.Http;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Drawing;

namespace BasicBlazorLibrary.Components.Divs
{
    public partial class CanvasContainerDiv
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string ContainerHeight { get; set; } = "";
        [Parameter]
        public string ContainerWidth { get; set; } = "";

        [Parameter]
        public SizeF ViewPort { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; } = cc.Transparent.ToWebColor();
    }
}