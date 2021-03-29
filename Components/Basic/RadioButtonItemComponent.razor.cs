using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using System.Globalization;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class RadioButtonItemComponent<TValue>
    {
        [CascadingParameter]
        private RadioButtonGroupComponent? Group { get; set; }
        private string GetName => Group!.Name;
        [Parameter]
        public TValue? SelectedValue { get; set; }
        [Parameter]
        public float Zoom { get; set; } = 1.5f;
        [Parameter]
        public TValue? Value { get; set; }
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }
        private bool Checked => SelectedValue!.Equals(Value);
        private void OnChange(ChangeEventArgs args)
        {
            var value = args.Value;
            bool rets = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var outs);
            if (rets == false)
            {
                throw new BasicBlankException("Unable to convert to expected variable type.  Rethink");
            }
            ValueChanged.InvokeAsync(outs);
        }
    }
}