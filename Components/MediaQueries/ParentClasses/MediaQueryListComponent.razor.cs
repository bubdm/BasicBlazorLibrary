using BasicBlazorLibrary.BasicJavascriptClasses;
using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers;
using BasicBlazorLibrary.Helpers;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
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
        [Inject]
        private IJSRuntime? JS { get; set; }
        private ResizeListenerClass? _resize;
        public EnumScreenOrientation ScreenOrientation { get; private set; }
        public EnumDeviceSize DeviceSize { get; private set; }
        public EnumDeviceCategory DeviceCategory
        {
            get
            {
                return DeviceSize switch
                {
                    EnumDeviceSize.SmallPhone => EnumDeviceCategory.Phone,
                    EnumDeviceSize.LargePhone => EnumDeviceCategory.Phone,
                    EnumDeviceSize.SmallTablet => EnumDeviceCategory.Tablet,
                    EnumDeviceSize.LargeTablet => EnumDeviceCategory.Tablet,
                    EnumDeviceSize.Desktop => EnumDeviceCategory.Desktop,
                    _ => throw new CustomBasicException("Nothing found"),
                };
            }
        }
        public BrowserSize? BrowserInfo { get; private set; }
        private bool OperatingSystemContainsKeyboard { get;  set; }
        public bool HasKeyboard(EnumKeyboardCategory category)
        {
            switch (category)
            {
                case EnumKeyboardCategory.Real:
                    return OperatingSystemContainsKeyboard;
                case EnumKeyboardCategory.Emulation:
                    if (DeviceSize == EnumDeviceSize.Desktop)
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
            _resize = new ResizeListenerClass(JS!);
            _resize.Onresized = MediaQueryListComponent_OnResized;
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                OperatingSystemContainsKeyboard = await JS!.HasKeyboard();
                await _resize!.InitAsync();
            }
            catch (Exception)
            {
                return;
            }
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
            DeviceSize = largest switch
            {
                >= 1500 => EnumDeviceSize.Desktop,
                >= 1100 => EnumDeviceSize.LargeTablet,
                >= 900 => EnumDeviceSize.SmallTablet,
                >= 600 => EnumDeviceSize.LargePhone,
                _ => EnumDeviceSize.SmallPhone
            };
            _loading = false;
            StateHasChanged();
        }
        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}