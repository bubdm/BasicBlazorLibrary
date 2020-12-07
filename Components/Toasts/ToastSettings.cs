using Microsoft.AspNetCore.Components;
namespace BasicBlazorLibrary.Components.Toasts
{
    public record ToastSettings(string Heading, RenderFragment Message, string BaseClass = "", string AdditionalClasses= "", bool ShowProgressBar = false );
}