using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.RenderHelpers
{
    public partial class VisibleHiddenComponent
    {

        //for now, has to just show without div.  because most of the time, its being used with svg.
        //bad news is svg combined with div may not even work.
        //i was right by assuming that the visible component does not work under an svg tag.
        //this means for the deck version, has to do something different.
        //because most of the time, it would already be wrapped under a svg tag.
        //also means i need the visible attribute to determine whether to consider as well again for refreshing now.
        //has to use the visible part i already had in the svg.  would dispose of parts of it and i can't do anything about that part.

        //leave until i fix more of the issues.


        [Parameter]
        public bool Visible { get; set; } = true;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private string GetClass()
        {
            if (Visible == false)
            {
                return "hiddendiv";
            }
            return "visiblediv";
        }
        private string GetDisplay => Visible ? "" : "none";
    }
}