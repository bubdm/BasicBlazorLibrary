using System;

namespace BasicBlazorLibrary.Components.Toasts
{
    internal record ToastInstance(Guid ID, DateTime TimeStamp, ToastSettings ToastSettings);
}
