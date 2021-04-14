using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class CheckboxComponent
    {
        [Parameter]
        public float Zoom { get; set; } = 1.5f; //default to larger size.
        [Parameter]
        public bool Value { get; set; }
        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }
        private void OnCheckboxChanged(object wasChecked)
        {
            bool rets = Convert.ToBoolean(wasChecked);
            ValueChanged.InvokeAsync(rets);
        }
    }
}