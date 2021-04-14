using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicLibraries.BasicDataSettingsAndProcesses.SColorString;
using BasicBlazorLibrary.Helpers;
using System.Drawing;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.Arrows
{
    public partial class ArrowCompleteComponent
    {
        [Parameter]
        public EnumArrowCategory ArrowCategory { get; set; }

        [Parameter]
        public EventCallback RightClicked { get; set; }
        [Parameter]
        public EventCallback LeftClicked { get; set; }
        [Parameter]
        public EventCallback UpClicked { get; set; }
        [Parameter]
        public EventCallback DownClicked { get; set; }
        [Parameter]
        public string BackgroundColor { get; set; } = cc.Black.ToWebColor();
        [Parameter]
        public string StrokeColor { get; set; } = cc.Transparent.ToWebColor();
        [Parameter]
        public string StrokeWidth { get; set; } = "1px";
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public string TargetWidth { get; set; } = "";
        [Parameter]
        public string RowColumnGap { get; set; } = "10px";
        private readonly int _howManyColumns = 3;
        private readonly int _howManyRows = 3;
        private readonly SizeF _singleRatio = new (1, 1); 
        private SizeF _containerViewPort;
        protected override void OnInitialized()
        {
            _containerViewPort = new (_howManyColumns * 100, _howManyRows * 100);
            base.OnInitialized();
        }
        private bool CanStart()
        {
            if (TargetHeight == "" && TargetWidth == "")
            {
                return false;
            }
            if (TargetHeight != "" && TargetWidth != "")
            {
                return false;
            }
            return true;
        }
        private string GetContainerHeight
        {
            get
            {
                string output;
                if (TargetHeight != "")
                {
                    output = TargetHeight.ContainerHeight(_howManyRows, _singleRatio);
                    return output;
                }
                output = TargetWidth.ContainerHeight(_howManyRows, _singleRatio);
                return output;
            }
        }

        private string GetContainerWidth
        {
            get
            {
                string output;
                if (TargetHeight != "")
                {
                    output = TargetHeight.ContainerWidth(_howManyColumns, _singleRatio);
                    return output;
                }
                output = TargetWidth.ContainerWidth(_howManyColumns, _singleRatio);
                return output;
            }
        }

        private static string GetCommonRowsColumns => aa.RepeatMinimum(2);
        private static string GetThreeColumns => aa.RepeatMinimum(3);
    }
}