using CommonBasicLibraries.CollectionClasses;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class WorkProgressComponent<TValue>
    {
        private enum EnumStatus
        {
            NoneToBegin,
            Progress,
            Completed
        }

        [Parameter]
        public RenderFragment<TValue>? WorkContent { get; set; }
        [Parameter]
        public RenderFragment? CompletedContent { get; set; }
        [Parameter]
        public string WorkTitle { get; set; } = "Processing";
        [Parameter]
        public RenderFragment? NoneContent { get; set; } //this means there was nothing to begin with.  its the content that would appear if any.
        //if there is none, will do something else.
        [Parameter]
        public EventCallback OnNoneToBeginWith { get; set; }
        [Parameter]
        public EventCallback<TValue> OnContinueOn { get; set; } //this means it can run code to continue.
        [Parameter]
        public EventCallback<TValue> OnBeginningOfProcess { get; set; }
        [Parameter]
        public EventCallback OnCompleted { get; set; }
        private bool _didChange;
        private bool _loading = true;
        private int _index;
        private EnumStatus _status = EnumStatus.NoneToBegin; //start here.
        private BasicList<TValue> _itemList = new();
        [Parameter]
        public BasicList<TValue> ItemList
        {
            get => _itemList;
            set
            {
                if (value != _itemList)
                {
                    //do change now.
                    _didChange = true;
                    _itemList = value;
                }
            }
        }
        protected override void OnInitialized()
        {
            _didChange = true; //has to be this way to begin with.
            base.OnInitialized();
        }
        protected override async Task OnParametersSetAsync()
        {
            if (_didChange == false)
            {
                _loading = false;
                return;
            }
            if (ItemList.Count == 0 && OnNoneToBeginWith.HasDelegate)
            {
                await OnNoneToBeginWith.InvokeAsync();
                _loading = false;
                _didChange = false;
                return;
            }
            _status = EnumStatus.Progress; //has to be here.
            _index = 0;
            _loading = false;
            _didChange = false;
            if (OnContinueOn.HasDelegate)
            {
                await OnContinueOn.InvokeAsync(ItemList.First());   
            }
        }
        public async Task NextOneAsync()
        {
            if (_index == ItemList.Count - 1)
            {
                await PrivateCompletedAsync();
                return;
            }
            _index++;
            await PrivateContinueAsync();
        }
        private int UpTo => _index + 1;
        private async Task PrivateContinueAsync()
        {
            if (OnContinueOn.HasDelegate)
            {
                await OnContinueOn.InvokeAsync(ItemList[_index]);
                return;
            }
        }
        private async Task PrivateCompletedAsync()
        {
            _status = EnumStatus.Completed;
            _index = -1; //because its completed now.
            if (OnCompleted.HasDelegate)
            {
                await OnCompleted.InvokeAsync();
            }
        }
        public async Task PreviousOneAsync()
        {
            if (_index < 1)
            {
                UIPlatform.ShowUserErrorToast("Cannot go to previous one because you are already at the beginning");
                return;
            }
            _index--;
            await PrivateContinueAsync();
        }
        public async Task SkipSeveralAsync(int howMany)
        {
            if (_index + howMany + 1 >= ItemList.Count)
            {
                await PrivateCompletedAsync();
                return;
            }
            _index += howMany;
            await PrivateContinueAsync();
        }
    }
}