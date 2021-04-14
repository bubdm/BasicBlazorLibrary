using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Divs
{
    public partial class HiddenDiv
    {
        private ElementReference? _elementUsed;
        protected override void OnInitialized()
        {
            _elementUsed = null;
        }
        [Parameter]
        public string ElementHeight { get; set; } = "";
        public async Task<int> PixelHeightAsync()
        {
            return await JS!.GetContainerHeight(_elementUsed);
        }
    }
}