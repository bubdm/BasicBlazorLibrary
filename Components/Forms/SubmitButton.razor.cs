using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Forms
{
    public partial class SubmitButton
    {
        [CascadingParameter]
        public SubmitForm? Form { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}