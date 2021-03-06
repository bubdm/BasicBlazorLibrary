using BasicBlazorLibrary.BasicJavascriptClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.AutoCompleteHelpers
{
    //has to be public now.
    //this will allow other experiments to be done.
    public class AutoCompleteService
    {
        private readonly AutoScrollClass _scrollHelper;

        //looks like this can't do anything with the textbox part now.  that now has to be completely separate.
        private readonly KeystrokeClass _keystroke;
        public event Action? ArrowUp;
        public event Action? ArrowDown;
        public event Action? BackspacePressed;
        public int ElementHighlighted { get; private set; } = -1;
        public int ElementScrollTo { get; private set; } = -1;
        public bool NeedsToScroll { get; private set; }
        private int Previoushighlight { get; set; } = -1;
        private int TotalElements { get; set; } //needs to be private now.
        public AutoCompleteService(IJSRuntime js)
        {
            _scrollHelper = new AutoScrollClass(js);
            _keystroke = new KeystrokeClass(js);
            _keystroke.AddAction(ConsoleKey.Backspace, () =>
            {
                ElementHighlighted = -1; //leave the previous alone.
                NeedsToScroll = false; //did not work but should do anyways.
                ElementScrollTo = -1;
                BackspacePressed?.Invoke();
            });
            _keystroke.AddArrowUpAction(() => ArrowUp?.Invoke());
            _keystroke.AddArrowDownAction(() => ArrowDown?.Invoke());
        }
        public void DoHighlight(int value, bool alsoscroll)
        {
            NeedsToScroll = alsoscroll;
            if (alsoscroll)
            {
                ElementScrollTo = value;
            }
            ElementHighlighted = value;
            Previoushighlight = value;
        }
        public void Unhighlight(bool alsoscroll)
        {
            NeedsToScroll = alsoscroll;
            ElementHighlighted = -1;
            Previoushighlight = -1;
            ElementScrollTo = -1; //i think this too.
        }
        public void MoveDown()
        {
            NeedsToScroll = false;
            if (Previoushighlight > -1 && ElementHighlighted == -1)
            {
                NeedsToScroll = true;
                ElementHighlighted = Previoushighlight;
                ElementScrollTo = ElementHighlighted;
                return;
            }
            if (ElementHighlighted + 1 == TotalElements)
            {
                return;
            }
            ElementHighlighted++;
            Previoushighlight = ElementHighlighted;
            ElementScrollTo = ElementHighlighted;
            NeedsToScroll = true;
        }
        public void MoveUp()
        {
            NeedsToScroll = false; //default to false.
            if (Previoushighlight > -1 && ElementHighlighted == -1)
            {
                NeedsToScroll = true;
                ElementHighlighted = Previoushighlight;
                ElementScrollTo = ElementHighlighted;
                return;
            }
            if (ElementHighlighted == 0)
            {
                return;
            }
            ElementHighlighted--;
            Previoushighlight = ElementHighlighted;
            ElementScrollTo = ElementHighlighted;
            NeedsToScroll = true;
        }
        //has to be update now.  because its not just for combos anymore.
        public void Update(int elements)
        {
            TotalElements = elements; //may not need to scroll to begin with (?)
        }
        public async Task ScrollToElementAsync(ElementReference? element)
        {
            if (element == null)
            {
                return;
            }
            NeedsToScroll = false; //i think.  hopefully needs nothing else here (?)
            await _scrollHelper.ScrollToElementAsync(element);
        }
        public async Task InitializeAsync(ElementReference? element) //still needed for the k
        {
            if (element == null)
            {
                return; //just in case.
            }
            await _keystroke.InitAsync(element);
        }
    }
}
