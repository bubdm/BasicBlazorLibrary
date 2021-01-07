using System;
namespace BasicBlazorLibrary.Components.NavigationMenus
{
    public class MenuItem
    {
        public string Text { get; set; } = "";
        public Action Clicked { get; set; } = () => { }; //default to empty.
    }
}