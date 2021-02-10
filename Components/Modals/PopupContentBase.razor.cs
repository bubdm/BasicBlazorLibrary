using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Modals
{
    public abstract partial class PopupContentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; } //this is all there is now.
        //everything that is needed for content will be here.  maybe that could work.

        //for now, has to be style since css isolation is not working properly.

        [Parameter]
        public string HeaderTitle { get; set; } = "";

        [Parameter]
        public string Style { get; set; } = ""; //for now, has to be style since css isolation is not working when sending to components.


        [Parameter]
        public EventCallback CloseButtonClick { get; set; }

        //was going to have a saveclick.  the only problem is could require validations though.  not standard enough to put into the basic functions.

        //this may be enough for now.  can always add on if i find more repeating stuff.

        //bad news is not sure if css isolation works even for inherited classes or not.  has to do an experiment to see.
        //if so, then can do some basics here.



        //decided to not bother with background.  if that is needed, do as part of style.  anything using it can do it as well.
        //the reason why not for backgroundcolor is because would be bad with title and then the main screen.
        //this means the main screen modal would usually be white if no style is set.
        //refer to what i did for the simple modal.  because that one showed white properly somehow or another.

        //cannot have the one for full screen.  because there is one that would obviously use full screen.

        //[Parameter]
        //public bool FullScreen { get; set; } = true;

        [Parameter]
        public bool DisableBackgroundClick { get; set; } = true; //default needs to disable it.  however, could enable it if needed.

        //i like the idea of a line to separate header from content.



        //i think in order to keep this simple what needs to happen is as follows:

        //1.  if headertemplate is specified, then ignores headertitle because you have to populate it anyways.
        //2.  i like the idea of specifying 

        [Parameter]
        public bool ShowHeader { get; set; } = true; //i think default is to show header.

        [Parameter]
        public RenderFragment? HeaderTemplate { get; set; }
        [Parameter]
        public RenderFragment? FooterTemplate { get; set; }
        [Parameter]
        public RenderFragment? Content { get; set; }

        [Parameter]
        public bool ShowCloseButton { get; set; }

        [Parameter]
        public bool Scrollable { get; set; } = true; //i like the idea that has to be true or false.
        //should default to true.  but if you need to tune for performance, you can set to false.  but it means no possibility for scrollbars.  if it overflows, then would get cut off alot.


        //height may not be as important anymore.
        //the fullpage one would do it automatically though.

        //i propose doing lots of copy/paste for the modals to save on performance (less controls).

        //the height/width  plus positioning does not fit here.

        [Parameter]
        public string Height { get; set; } = "";


        protected abstract string GetWidth { get; }

        [Parameter]
        public bool DisableParentClickThrough { get; set; } = true; //this means if the modal is up, can't do anything else.
        [Parameter]
        public bool FullScreen { get; set; } = true; //can't be in the base class because there is one that is not full screen.

        [Parameter]
        public string BackgroundColor { get; set; } = "white"; //needs to allow any color for background color.  otherwise, shows white borders which is wrong.

        [Parameter]
        public string BackgroundImage { get; set; } = ""; //allows the possibility of showing background image.

        [Parameter]
        public string HeaderColor { get; set; } = "black"; //can change color if necessary since you have a choice for background color.

        //if you choose black but has headers will be hosed unless you redo the border processes.


        private string GetBackgrounds()
        {
            if (BackgroundImage != "")
            {
                return $"background-image: url('{BackgroundImage}'); background-size: 100% 100%;";
            }
            return $"background-color: {BackgroundColor};";
        }


        //protected virtual string BackgroundColor => "white";

        protected string GetHiddenStyle() //still needs to be protected so if not full screen, then can decide what it will do.
        {
            if (Visible == true)
            {
                return "";
            }
            return "display: none;"; //for now, no intellisense.  i like the idea of anything that uses it being able to use the function for that part of the style.
        }

        protected virtual bool ProtectedHiddenFull => DisableParentClickThrough;

        protected string TopModalStyle() //this means the full screen one can use the normal one.  in that case, if its taking full screen, then can't interact with the stuff anyways.
        {
            if (FullScreen)
            {
                return $"display: flex;  top: 0; left: 0; width: 100%; height: 100%; position: fixed; z-index: {ZIndex}; background-color: rgba(0,0,0,0.5); {GetHiddenStyle()}";
            }
            if (ProtectedHiddenFull)
            {
                return $"display: flex;  top: 0; left: 0; width: 100%; height: 100%; position: fixed; z-index: {ZIndex}; background-color: transparent; {GetHiddenStyle()}";
            }
            return $"display: flex;  z-index: {ZIndex};  {GetHiddenStyle()}";
            //looks like has to be on your own this time.
        }

        //relative is simple enough that not worth doing a method for that part.

        protected string ScrollStyle()
        {
            if (Scrollable == false)
            {
                return "";
            }
            return "overflow: auto;";
        }

        protected override void ClosePopup()
        {
            if (CloseButtonClick.HasDelegate)
            {
                CloseButtonClick.InvokeAsync();
                return;
            }
            base.ClosePopup();
        }

        protected void PrivateBackgroundClicked()
        {
            if (DisableBackgroundClick)
            {
                return;
            }
            ClosePopup(); //do this instead.
        }
    }
}