using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class ReaderComponent<T> : IAsyncDisposable
    {
        /// <summary>
        /// this is needed because in order for the scrollbars to work properly should have the width and height of the area.
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "80vw";
        /// <summary>
        /// this is needed because if it uses the browser scroll bars, then does not scroll exactly to the proper area.
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "80vh";

        [Parameter]
        public CustomBasicList<T>? RenderList { get; set; } //this needs to be here though.

        [Parameter]
        public RenderFragment<T>? ChildContent { get; set; }

        [Parameter]
        public ReaderModel? DataContext { get; set; } //if you change values of the readermodel, then no problem because does not run parametershaschanged.

        [Parameter]
        public EventCallback TopReached { get; set; }

        [Parameter]
        public EventCallback BottomReached { get; set; }

        //has to experiment to see if this idea works.

        private bool NeedsHighlighting => DataContext!.HighlightColor != cc.Transparent.ToWebColor();

        private KeystrokeClass? _keystroke;
        private AutoScrollClass? _autoScroll;

        private string GetColorStyle(int id)
        {
            if (NeedsHighlighting == false)
            {
                return "";
            }
            if (id != DataContext!.ElementHighlighted)
            {
                return ""; //because its not the correct one.
            }
            return $"background-color: {DataContext.HighlightColor};";
        }

        private ElementReference? _reference;
        private ElementReference? _main;
        private bool _needsToScroll = true;

        private void ElementClicked(int x)
        {
            if (NeedsHighlighting == false)
            {
                return;
            }
            //this does not make it scroll though.
            DataContext!.ElementHighlighted = x; //not sure if we need statehaschanged (?)
        }

        protected override void OnParametersSet()
        {
            if (WasSame)
            {
                ResetValues();
                return;
            }
            _needsToScroll = true; //if anything changes, then set this to true.
            if (DataContext!.ElementScrollTo > -1)
            {
                DataContext.ElementHighlighted = DataContext.ElementScrollTo;
            }
            ResetValues();
            base.OnParametersSet();
        }
        protected override void OnInitialized()
        {
            _reference = null;
            _main = null;
            _keystroke = new KeystrokeClass(JS!);
            _keystroke.ArrowUp = () => ArrowUp(false);
            _keystroke.ArrowDown = () => ArrowDown(false);
            _autoScroll = new AutoScrollClass(JS!);
        }
        private CustomBasicList<T>? _previousList;
        private int _previousHighlighted;
        private int _previousScrolled;
        private void ResetValues()
        {
            _previousList = null;
            _previousHighlighted = -1;
            _previousScrolled = -1;
        }

        private bool WasSame
        {
            get
            {
                if (_previousList == null)
                {
                    return false;
                }
                if (_previousHighlighted == -1 || _previousScrolled == -1)
                {
                    return false;
                }
                if (_previousList.Count != RenderList!.Count)
                {
                    return false;
                }
                if (_previousHighlighted != DataContext!.ElementHighlighted || _previousScrolled != DataContext!.ElementScrollTo)
                {
                    return false;
                }
                return true;
            }
        }

        private void SetValues()
        {
            _previousList = RenderList!.ToCustomBasicList();
            _previousHighlighted = DataContext!.ElementHighlighted;
            _previousScrolled = DataContext!.ElementScrollTo;
        }

        public void ArrowUp()
        {
            ArrowUp(true);
        }
        private void ArrowUp(bool manuel)
        {
            if (DataContext!.ElementHighlighted == 0)
            {
                SetValues();
                TopReached.InvokeAsync();
                return;
            }
            DataContext.ElementHighlighted--;
            DataContext.ElementScrollTo = DataContext.ElementHighlighted;
            _needsToScroll = true;
            if (manuel == false)
            {
                StateHasChanged();
            }
        }
        public void ArrowDown()
        {
            ArrowDown(true);
        }
        private void ArrowDown(bool manuel)
        {
            if (DataContext!.ElementHighlighted + 1 == RenderList!.Count)
            {
                SetValues();
                BottomReached.InvokeAsync();
                return;
            }
            DataContext!.ElementHighlighted++;
            DataContext.ElementScrollTo = DataContext.ElementHighlighted;
            _needsToScroll = true;
            if (manuel == false)
            {
                StateHasChanged();
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_needsToScroll == false || _keystroke == null)
            {
                return;
            }
            if (firstRender)
            {
                await _keystroke.InitAsync(_main); //i think
                await _main!.Value.FocusAsync();
            }
            if (_reference == null)
            {
                return; //try this way.  hopefully no never ending loop (?)
            }
            await _autoScroll!.ScrollToElementAsync(_reference);
            _needsToScroll = false;
        }

        public ValueTask DisposeAsync()
        {
            _keystroke!.ArrowUp = null;
            _keystroke.ArrowDown = null;
            return ValueTask.CompletedTask;
        }
    }
}