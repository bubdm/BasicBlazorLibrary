using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Tabs
{
    public partial class InternalBarHelper<T>
        where T : TabPage
    {
        [CascadingParameter]
        private IBarContainer<T>? Container { get; set; }
        [Parameter]
        public BasicList<T> Pages { get; set; } = new();
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