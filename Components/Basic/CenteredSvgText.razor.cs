using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Basic
{
    public partial class CenteredSvgText
    {
        [Parameter]
        public float FontSize { get; set; }
        [Parameter]
        public string Text { get; set; } = "";
        [Parameter]
        public string TextColor { get; set; } = "";
        [Parameter]
        public bool IsBold { get; set; }
        private string GetOtherClass()
        {
            if (IsBold == false)
            {
                return "";
            }
            return "bold";
        }
    }
}