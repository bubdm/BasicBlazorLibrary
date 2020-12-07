using System;
using System.Timers;
namespace BasicBlazorLibrary.Components.Toasts
{
    internal class CountdownTimer : IDisposable
    {
        private Timer? _timer;
        private readonly int _timeout;
        private readonly int _countdownTotal;
        private int _percentComplete;

        internal Action<int>? OnTick { get; set; }
        internal Action? OnElapsed { get; set; }

        internal CountdownTimer(int timeout)
        {
            _countdownTotal = timeout;
            _timeout = (_countdownTotal * 1000) / 100;
            _percentComplete = 0;
            SetupTimer();
        }

        internal void Start()
        {
            _timer!.Start();
        }

        private void SetupTimer()
        {
            _timer = new Timer(_timeout);
            _timer.Elapsed += HandleTick;
            _timer.AutoReset = false;
        }

        private void HandleTick(object sender, ElapsedEventArgs args)
        {
            _percentComplete++;
            OnTick?.Invoke(_percentComplete);

            if (_percentComplete == 100)
            {
                OnElapsed?.Invoke();
            }
            else
            {
                SetupTimer();
                Start();
            }
        }

        public void Dispose()
        {
            _timer!.Dispose();
            _timer = null;
        }
    }
}