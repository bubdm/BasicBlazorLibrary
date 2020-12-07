using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.RenderHelpers
{
    public partial class FlexibleRenderComponent : IDisposable
    {
        [Parameter]
        public bool Visible { get; set; } = true;
        [Parameter]
        public bool CanRender { get; set; } = true;
        [Parameter]
        public object? Key { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public Action? ChangeDelegate { get; set; }
        protected override void OnParametersSet()
        {
            if (ChangeDelegate != null)
            {
                ChangeDelegate = ShowChange;
            }
            base.OnParametersSet();
        }
        private void ShowChange()
        {
            InvokeAsync(StateHasChanged);
        }
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            if (ChangeDelegate != null)
            {
                ChangeDelegate = null; //hopefully this simple now.
            }
        }
    }
}