using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers;
using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Headers;
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
        private bool OperatingSystemContainsKeyboard { get;  set; }
        public bool HasKeyboard(EnumKeyboardCategory category)
        {
            switch (category)
            {
                case EnumKeyboardCategory.Real:
                    return OperatingSystemContainsKeyboard;
                case EnumKeyboardCategory.Emulation:
                    if (DeviceCategory == EnumDeviceCategory.Desktop)
                    {
                        return true;
                    }
                    return false;
                case EnumKeyboardCategory.ManuelKeyboard:
                    return true;
                case EnumKeyboardCategory.ManuelTouchscreen:
                    return false;
                default:
                    return false;
            }
        }

        private bool _loading = true;
        protected override void OnInitialized()
        {
            _resize = new ResizeListener(JS!);
            _resize!.OnResized += MediaQueryListComponent_OnResized;
        }

        protected override async Task OnInitializedAsync()
        {
            OperatingSystemContainsKeyboard = await JS!.HasKeyboard();
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