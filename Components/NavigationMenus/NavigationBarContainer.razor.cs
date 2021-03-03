using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.NavigationMenus
{
    public partial class NavigationBarContainer
    {
        [Parameter]
        public string Title { get; set; } = "";
        private bool _showBar = false; //anything can access it to change when necessary.
        [Parameter]
        public RenderFragment? MainContent { get; set; }

        [Parameter]
        public RenderFragment? BarContent { get; set; }

        [Parameter]
        public EventCallback BackClicked { get; set; }
        [Parameter]
        public bool ShowBack { get; set; } = true; //there can be cases where even though there is a backclick, there is a situation where it would not show it anyways.

        [Parameter]
        public EventCallback CloseClicked { get; set; }

        [Parameter]
        public string ArrowHeight { get; set; } = "70px"; //can see what makes sense for defaults.

        //i think the color of the text should be the color of the arrows and even x.
        [Parameter]
        public string CloseHeight { get; set; } = "2rem";

        [Parameter]
        public string MainBackgroundColor { get; set; } = cc.Blue.ToWebColor();
        [Parameter]
        public string MenuBackgroundColor { get; set; } = cc.Black.ToWebColor();
        [Parameter]
        public string MainTextColor { get; set; } = cc.White.ToWebColor();
        [Parameter]
        public string MenuTextColor { get; set; } = cc.White.ToWebColor();
        [Parameter]
        public string MenuFontSize { get; set; } = "1.5rem";
        [Parameter]
        public string Padding { get; set; } = "10px";
        [Parameter]
        public string MainFontSize { get; set; } = "1.5rem"; //this means its completely flexible for fontsize as well.
        [Parameter]
        public string CircleSize { get; set; } = "10px"; //so you can change this now as well.
        [Parameter]
        public string CircleColor { get; set; } = cc.White.ToWebColor();
        [Parameter]
        public string MenuHeight { get; set; } = "300px"; //default to 300 pixels but its flexible as well.
        [Parameter]
        public string MenuWidth { get; set; } = "50vmin";
        [Parameter]
        public CustomBasicList<MenuItem> MenuList { get; set; } = new (); //can still show the list even with no items.

        [Parameter]
        public bool AlwaysShowBar { get; set; } = false;

      

        //for now, no more fullpage.   did not work as expected anyways.  until further notice, has to manually specify the sizes if scrolling is needed.




        private SizeF _viewPort = new (40, 40);
        private string GetContainer
        {
            get
            {
                string output = CircleSize.ContainerHeight(4, _viewPort);
                return output;
            }
        }
        protected override void OnInitialized()
        {
            _showMenu = false;
            _showBar = AlwaysShowBar;
        }
        private bool _showMenu;
        private string GetFirstClass()
        {
            if (_showBar == false)
            {
                return "hidden";
            }
            return "";
        }
        public void ChangeBar(bool display)
        {
            _showBar = display;
            //was able to get to the root of the problem
            //lesson.  if one is using it, then can't use bind second time.  otherwise, causes problems.
            StateHasChanged();
        }
    }
}