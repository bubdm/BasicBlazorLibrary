using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Modals
{
    public partial class PopupCenteredStandard
    {
        protected override string GetWidth
        {
            get
            {
                if (Media == null)
                {
                    return "400px"; //if not provided, then cannot be adaptive.
                }
                if (Media.BrowserInfo!.Width <= 396)
                {
                    int maxs = Media.BrowserInfo.Width - 4;
                    return $"{maxs}px";
                }
                return "400px";
            }
        }
        [CascadingParameter]
        private MediaQueryListComponent? Media { get; set; }
    }
}