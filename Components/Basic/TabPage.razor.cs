using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class TabPage
    {
        [CascadingParameter]
        protected TabControl? Parent { get; set; } //hopefully good enough.
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Text { get; set; } = "";
        protected override void OnInitialized()
        {
            if (Parent == null)
            {
                throw new BasicBlankException("TabPage must exist within a TabControl");
            }
            base.OnInitialized();
            Parent.AddPage(this);
        }
    }
}