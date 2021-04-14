using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class NumberComponent
    {
        [Parameter]
        public int Value { get; set; } = 0;
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }
        [Parameter]
        public string Class { get; set; } = "";
        [Parameter]
        public string Style { get; set; } = "";
        [Parameter]
        public string Width { get; set; } = "75px";
        private int CurrentValue
        {
            get => Value;
            set
            {
                var hasChanged = !EqualityComparer<int>.Default.Equals(value, Value);
                if (hasChanged)
                {
                    ValueChanged.InvokeAsync(value);
                }
            }
        }
    }
}