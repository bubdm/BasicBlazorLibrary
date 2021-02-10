using BasicBlazorLibrary.BasicJavascriptClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasicBlazorLibrary.Components.Basic
{
    public partial class ImageButtonComponent
    {
        [Parameter]
        public string MainSource { get; set; } = "";
        [Parameter]
        public string HoverSource { get; set; } = "";
        [Parameter]
        public string MarginLeft { get; set; } = "2px";
        [Parameter]
        public EventCallback OnClick { get; set; }

        private ElementReference _element;
        private HoverClass? _hover;


        protected override void OnInitialized()
        {
            _element = default;
            _hover = new (JS!);
            base.OnInitialized();
        }
        //for this version, will hard code.

        private async Task HoverAsync()
        {
            await _hover!.HoverAsync(_element, HoverSource);
        }

        private async Task UnhoverAsync()
        {
            await _hover!.UnHoverAsync(_element, MainSource);
        }

    }
}