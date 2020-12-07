using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Repeaters
{
    /// <summary>
    /// this is intended to loop through a set number of times.
    /// works around a problem where if i do a for next and use a component, it only renders the last value and not each item.
    /// this supports just looping once.
    /// </summary>
    public partial class SimpleRepeater
    {
        [Parameter]
        public int HowMany { get; set; }

        [Parameter]
        public RenderFragment<int>? ChildContent { get; set; }
    }
}