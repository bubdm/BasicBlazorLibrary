using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.Toasts
{
    public partial class BlazoredToast : IDisposable
    {
        [CascadingParameter] private BlazoredToasts? ToastsContainer { get; set; }
        [Parameter] public Guid ToastId { get; set; }
        [Parameter] public ToastSettings? ToastSettings { get; set; }
        [Parameter] public int Timeout { get; set; }
        private CountdownTimer? _countdownTimer;
        private int _progress = 100;
        protected override void OnInitialized()
        {
            _countdownTimer = new CountdownTimer(Timeout);
            _countdownTimer.OnTick += CalculateProgress;
            _countdownTimer.OnElapsed += () => { Close(); };
            _countdownTimer.Start();
        }
        private async void CalculateProgress(int percentComplete)
        {
            _progress = 100 - percentComplete;
            await InvokeAsync(StateHasChanged);
        }
        private void Close()
        {
            ToastsContainer!.RemoveToast(ToastId);
        }
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            _countdownTimer!.Dispose();
            _countdownTimer = null;
        }
    }
}