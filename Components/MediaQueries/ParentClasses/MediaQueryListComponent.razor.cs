using BasicBlazorLibrary.BasicJavascriptClasses;
using BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers;
using BasicBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.Exceptions;
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

        private ResizeListenerClass? _resize;

        //private ResizeListener? _resize;

        //[Inject]
        //private IResizeListener? ResizeListener { get; set; }
        public EnumScreenOrientation ScreenOrientation { get; private set; } //only this can set it though.  others is read only.
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
                    _ => throw new BasicBlankException("Nothing found"),
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
            //_resize!.OnResized += MediaQueryListComponent_OnResized;
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

                //can change allocations.  however, for now, decided to have in 5 categories.


                >= 1500 => EnumDeviceSize.Desktop,
                >= 1100 => EnumDeviceSize.LargeTablet, //otherwise, portrait for my tablet thinks its 
                >= 900 => EnumDeviceSize.SmallTablet,
                >= 600 => EnumDeviceSize.LargePhone,
                _ => EnumDeviceSize.SmallPhone
                //>= 1500 => EnumDeviceCategory.Desktop,
                //>= 900 => EnumDeviceCategory.Tablet,
                //_ => EnumDeviceCategory.Phone
            };
            _loading = false;
            StateHasChanged();
        }
        public ValueTask DisposeAsync()
        {
            //hopefully not needed (?)
            //_resize!.OnResized -= MediaQueryListComponent_OnResized;
            return ValueTask.CompletedTask;
        }
    }
}