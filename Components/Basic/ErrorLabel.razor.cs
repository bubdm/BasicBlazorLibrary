using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class ErrorLabel
    {
        [Parameter]
        public string ErrorMessage { get; set; } = "";
    }
}