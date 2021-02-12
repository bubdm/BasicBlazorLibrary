using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Layouts.Simple
{
    public partial class LeftOverLayoutComponent
    {
        public enum EnumDetectionCategory
        {
            Browser, ParentContainer
        }
        [Parameter]
        public EnumDetectionCategory DetectionCategory { get; set; } = EnumDetectionCategory.Browser; //most of the time, just use the browser.
        [CascadingParameter]
        private MediaQueryListComponent? Media { get; set; }
        [Parameter]
        public RenderFragment? TopContent { get; set; }
        [Parameter]
        public RenderFragment? MainContent { get; set; }
        [Parameter]
        public bool SupportAutoScroll { get; set; } = false; //if you need to support autoscroll, then set this to true.  there is some tradeoffs (no width set for the component).  its the risk i will have to take though.
        [Parameter]
        public string MainBackgroundColor { get; set; } = "transparent";
        [Parameter]
        public string FullBackgroundColor { get; set; } = "transparent";
        [Parameter]
        public RenderFragment? BottomContent { get; set; }
        [Parameter]
        public int RightMargin { get; set; } = 10;
        [Parameter]
        public int LeftMargin { get; set; } = 5;
        [Parameter]
        public int BottomMargin { get; set; } = 5;
        [Parameter]
        public int TopMargin { get; set; } = 5;
        [Parameter]
        public int RowGap { get; set; } = 5; //can change as needed

        [Parameter]
        public bool BuiltInScrollMain { get; set; } = true;

        public ElementReference? MainElement { get; private set; }
        private string GetFirstTop => $"{TopMargin}px";
        private string GetFirstLeft => $"{LeftMargin}px";
        private string GetFirstBottomTop
        {
            get
            {
                if (TopContent == null)
                {
                    return "0px";
                }
                return $"{RowGap}px";
            }
        }
        private string GetLastBottomTop => $"{RowGap}px";
        private string GetMainWidth => $"{_mainWidth}px";
        private string GetMainHeight => $"{_mainHeight}px";
        private string GetLeftOverHeight => $"{_leftOverHeight}px";
        private int _mainWidth;
        private int _mainHeight;
        private int _firstHeight;
        private int _lastHeight;
        private int _leftOverHeight;
        private ElementReference? _firstElement;
        private ElementReference? _lastElement;
        private bool _did;
        private int _top; //looks like i need top afterall.
        protected override void OnInitialized()
        {
            _firstElement = null;
            _lastElement = null;
            base.OnInitialized();
        }
        protected override async Task OnParametersSetAsync()
        {
            if (MainElement != null)
            {
                await CalculateOtherAsync();
            }
        }
        private async Task CalculateOtherAsync()
        {
            if (MainElement == null)
            {
                return;
            }
            //int tempHeight = _mainHeight;
            int totalHeight;

            //if (SupportAutoScroll)
            //{
            //    _top = await JS!.GetContainerTop(MainElement);
            //    totalHeight = await JS!.GetParentHeight(MainElement); //hopefully this won't cause a problem (?)
            //    _mainHeight = totalHeight - _top - BottomMargin;

            //    //_mainHeight = 600; //try this way for now.  can experiment further later.
            //    return;
            //}
            _did = true;
            _top = await JS!.GetContainerTop(MainElement);

            

            int firstWidth = Media!.BrowserInfo!.Width;
            int firstHeight = Media.BrowserInfo.Height;

           

            int totalWidth;
            totalWidth = await JS!.GetParentWidth(MainElement);
            //throw new BasicBlankException(totalWidth.ToString());
            totalHeight = await JS!.GetParentHeight(MainElement);
            int bottomMargin = BottomMargin;
            if (totalWidth == LeftMargin || totalWidth <= 0 || totalWidth > firstWidth)
            {
                if (_top > 0 && BottomMargin == 5) //if you set manually, needs to respect that.
                {
                    bottomMargin -= 7;
                }
                totalWidth = firstWidth;
            }
            if (totalHeight == TopMargin || totalHeight <= 0 || totalHeight > firstHeight || DetectionCategory == EnumDetectionCategory.Browser)
            {
                totalHeight = firstHeight;
                if (_top > 0 && BottomMargin == 5)
                {
                    bottomMargin -= 7;
                }
            }
            else
            {
                _top = 0;
            }
            if (_mainHeight == 0 && SupportAutoScroll || SupportAutoScroll == false)
            {
                _mainHeight = totalHeight - _top - BottomMargin; //if autoscroll is needed, then do this way.  means after set, will never consider again.
            }

            _mainWidth = totalWidth - LeftMargin - RightMargin;
            int firstBottom;
            int lastBottom;
            if (_firstElement != null)
            {
                _firstHeight = await JS!.GetContainerHeight(_firstElement);
                firstBottom = BottomMargin;
            }
            else
            {
                _firstHeight = 0;
                firstBottom = 0;
            }
            if (_lastElement != null)
            {
                _lastHeight = await JS!.GetContainerHeight(_lastElement);
                lastBottom = BottomMargin;
            }
            else
            {
                _lastHeight = 0;
                lastBottom = 0;
            }
            _leftOverHeight = _mainHeight - _firstHeight - firstBottom - _lastHeight - lastBottom - bottomMargin;

            if (SupportAutoScroll)
            {

                //this means can be smaller but never larger (try that way).
                //if (tempHeight > 0 && )

                //Console.WriteLine(_mainHeight); //this part could be a little hosed.
                //_mainHeight = 600;
                return;
            }

        }
        private bool _seconds;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _seconds = true;
                await CalculateOtherAsync();
                StateHasChanged();
            }
            else if (_seconds)
            {
                await CalculateOtherAsync(); //looks like sometimes it needs a second time.
                StateHasChanged();
                _seconds = false;
            }
        }
        private string GetDisplay
        {
            get
            {
                //return "1"; //for now even though will have flickering.
                if (_did == true)
                {
                    return "1";
                }
                return "0";
            }
        }
    }
}