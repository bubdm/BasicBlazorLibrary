using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class HotkeyControl
    {

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public EventCallback F2 { get; set; }
        [Parameter]
        public EventCallback F4 { get; set; }
        //for now, leave as 2 options.  if i decide more are needed, can rethink.

        private async Task KeyDown(KeyboardEventArgs args)
        {
            if (args.Key == "F2" && F2.HasDelegate)
            {
                await F2.InvokeAsync();
                return;
            }
            if (args.Key == "F4" && F4.HasDelegate)
            {
                await F4.InvokeAsync();
            }
        }

    }
}