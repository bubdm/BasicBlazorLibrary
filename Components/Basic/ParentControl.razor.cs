using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.EventModels;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.Helpers;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class ParentControl<VM> : IHandle<OpenEventModel>, IHandle<CloseEventModel>
        where VM : BlazorScreenViewModel
    {
        private readonly IEventAggregator _aggregator;
        public ParentControl()
        {
            IEventAggregator aggregator = aa.cons!.Resolve<IEventAggregator>();
            _aggregator = aggregator;
            Type type = typeof(VM);
            aggregator.Subscribe(this, type.Name);
        }
        public VM? DataContext { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; } //maybe i don't need a generic version (?)
        protected override void OnInitialized()
        {
            _aggregator.Ask(typeof(VM)); //maybe this too now.
            base.OnInitialized();
        }
        void IHandle<OpenEventModel>.Handle(OpenEventModel message)
        {
            DataContext = message.ViewModelUsed as VM;
            //hopefully this simple.   may be forced to be client side unfortunately (?)
            //could use this as a case to show serious problem.
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        void IHandle<CloseEventModel>.Handle(CloseEventModel message)
        {
            DataContext = null;
            StateHasChanged();
        }
    }
}