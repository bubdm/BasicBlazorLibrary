using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Basic
{
    public partial class DateStampComponent
    {
        [Parameter]
        public string Text { get; set; } = "";
    }
}