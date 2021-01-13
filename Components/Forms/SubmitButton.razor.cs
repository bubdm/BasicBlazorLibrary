using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

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