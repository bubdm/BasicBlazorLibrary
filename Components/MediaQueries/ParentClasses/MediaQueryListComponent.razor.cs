using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.MediaQueries.ParentClasses
{
    public partial class MediaQueryListComponent : IAsyncDisposable
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        //this will also show other data that any component can use like screenorientation, etc.

        [Inject]
        private IJSRuntime? JS { get; set; }

        private ResizeListener? _resize;

        //[Inject]
        //private IResizeListener? ResizeListener { get; set; }
        public EnumScreenOrientation ScreenOrientation { get; private set; } //only this can set it though.  others is read only.
        public EnumDeviceCategory DeviceCategory { get; private set; }
        public BrowserSize? BrowserInfo { get; private set; }
        private bool _loading = true;
        protected override void OnInitialized()
        {
            _resize = new ResizeListener(JS!);
            _resize!.OnResized += MediaQueryListComponent_OnResized;
        }

        private void MediaQueryListComponent_OnResized(BrowserSize obj)
        {
            BrowserInfo = obj;
            int largest;
            if (BrowserInfo.Height > BrowserInfo.Width)
            {
                ScreenOrientation = EnumScreenOrientation.Portrait;
                largest = BrowserInfo.Height;
            }
            else
            {
                ScreenOrientation = EnumScreenOrientation.Landscape;
                largest = BrowserInfo.Width;
            }
            DeviceCategory = largest switch
            {
                >= 1500 => EnumDeviceCategory.Desktop,
                >= 900 => EnumDeviceCategory.Tablet,
                _ => EnumDeviceCategory.Phone
            };
            _loading = false;
            StateHasChanged();
        }
        public ValueTask DisposeAsync()
        {
            _resize!.OnResized -= MediaQueryListComponent_OnResized;
            return ValueTask.CompletedTask;
        }
    }
}