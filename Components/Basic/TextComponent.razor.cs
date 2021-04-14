using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
namespace BasicBlazorLibrary.Components.Basic
{
    /// <summary>
    /// this is used for cases where you want a simple text component.  however, does not have any validations.
    /// </summary>
    public partial class TextComponent
    {
        [Parameter]
        public string Value { get; set; } = "";
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        //this does not tie to any specific form.
        [Parameter]
        public string Class { get; set; } = "";
        [Parameter]
        public string Style { get; set; } = "";
        private string CurrentValue
        {
            get => Value;
            set
            {
                var hasChanged = !EqualityComparer<string>.Default.Equals(value, Value);
                if (hasChanged)
                {
                    ValueChanged.InvokeAsync(value);
                }
            }
        }
    }
}