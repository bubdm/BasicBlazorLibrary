using BasicBlazorLibrary.BasicJavascriptClasses;
using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class VirtualSimpleComponent<TItem> : IAsyncDisposable
    {
        //this one is not responsible for highlighting.  anything that uses can highlight though.
        //if methods are needed, needs to use the @ref and run methods on it.
        [Parameter]
        public CustomBasicList<TItem> Items { get; set; } = new CustomBasicList<TItem>(); //default to new list.  that is easist way to handle this.
        [Parameter]
        public RenderFragment<TItem>? ChildContent { get; set; }
        [Parameter]
        public string ContainerHeight { get; set; } = "50vh"; //defaults to 50 percent.  however, you can set as you please for that part.
        [Parameter]
        public string LineHeight { get; set; } = "1.5rem";
        [Parameter]
        public string ContainerWidth { get; set; } = "100vw";
        [Parameter]
        public bool HasSolidBlackBorders { get; set; } = false;
        [Inject]
        private IJSRuntime? Js { get; set; } //still needs this because needs javascript
        private ScrollListenerClass? _listen;
        private AutoScrollClass? _autoScroll;
        private ElementReference? _mainScroll;
        private ElementReference? _sampleElement;
        private ElementReference? _sampleContainer;
        private int _currentScrollValue;
        private int _clientHeight;
        private int _containerHeight;
        private float _scrollAmount;
        private bool _needsToScroll;
        private int _itemHeight;
        protected override void OnInitialized()
        {
            _listen = new ScrollListenerClass(Js!);
            _autoScroll = new AutoScrollClass(Js!);
            _listen.Scrolled += Listen_Scroll;
            _mainScroll = null;
            _sampleElement = null;
            _sampleContainer = null;
            base.OnInitialized();
        }
        private void Listen_Scroll(int obj)
        {
            _currentScrollValue = obj;
            StateHasChanged(); //because what you see will change.
        }
        private string GetBorders()
        {
            if (HasSolidBlackBorders == false)
            {
                return "";
            }
            return "border: 1px solid;";
        }
        private int GetNextItem()
        {
            int _nextUp;
            _nextUp = _itemHeight;
            int count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (_currentScrollValue < _nextUp)
                {
                    if (_currentScrollValue + _clientHeight == _containerHeight)
                    {
                        return count - ElementsFit();
                    }
                    return i; //0 based now.
                }
                _nextUp += _itemHeight;
            }
            return count;
        }
        private string GetUnitMeasure()
        {
            CustomBasicList<string> units = new CustomBasicList<string>() { "rem", "em", "px", "vw", "vh", "vmin", "vmax", "%" };
            foreach (var unit in units)
            {
                if (LineHeight.EndsWith(unit))
                {
                    return unit;
                }
            }
            throw new BasicBlankException("No unit measure found");
        }
        private string GetContainerSize()
        {
            string extraText;
            string leftovers;
            float firsts;
            extraText = GetUnitMeasure();
            leftovers = LineHeight.Replace(extraText, "");
            firsts = float.Parse(leftovers);
            var seconds = firsts * Items.Count;
            return $"{seconds}{extraText}";
        }
        private int ElementsFit()
        {
            int partialheight = _clientHeight;
            float singlepixel = _itemHeight;
            int output = partialheight / (int)singlepixel;
            if (output > Items.Count)
            {
                output = Items.Count; //because you can't have more than the elements allowed.
            }
            else if (output + 5 > Items.Count)
            {
                output = Items.Count;
            }
            else
            {
                output +=5; //to make for more smooth scrolling.
            }
            
            return output;
        }
        private string GetPosition(int element)
        {
            int item = element * _itemHeight;
            return $"{item}px;";
        }
        public void ScrollToSpecificElement(int whichOne) //has to be 0 based.
        {
            _needsToScroll = true;
            _scrollAmount = _itemHeight * whichOne;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _clientHeight = await Js!.GetContainerHeight(_mainScroll);
            _itemHeight = await Js!.GetContainerHeight(_sampleElement);
            _containerHeight = await Js!.GetContainerHeight(_sampleContainer);
            if (firstRender)
            {
                await _listen!.InitAsync(_mainScroll);
                StateHasChanged();
                return;
            }
            if (_needsToScroll)
            {
                await _autoScroll!.SetScrollPosition(_mainScroll, _scrollAmount); //i think
                _needsToScroll = false;
            }
        }
        public ValueTask DisposeAsync()
        {

            _listen!.Scrolled -= Listen_Scroll;
            return ValueTask.CompletedTask;
        }
    }
}