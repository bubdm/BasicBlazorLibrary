using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Tabs
{
    public partial class InternalBarHelper<T>
        where T: TabPage
    {
        [CascadingParameter]
        private IBarContainer<T>? Container { get; set; }

        [Parameter]
        public CustomBasicList<T> Pages { get; set; } = new CustomBasicList<T>(); //has to set as parameter since using the helper is not working.  hopefully i am not forced to send the entire object (?)


        private string GetMainStyle()
        {
            return $"background-color: {Container!.BackgroundColor};";
        }
        private string GetLabelColorStyle(TabPage page)
        {
            if (page == Container!.ActivePage)
            {
                return Container!.ActiveColor;
            }
            return Container!.NormalColor;
        }
        private string GetOtherStyles()
        {
            return $"font-size: {Container!.FontSize}; padding: {Container.Padding};";
        }

    }
}