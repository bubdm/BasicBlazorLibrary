using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class MultiLineControl
    {
        private bool HasTabs { get; set; }
        private int HowMany { get; set; }
        [Parameter]
        public string Value { get; set; } = "";
    }
}