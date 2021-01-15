using System;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterCheckbox
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            KeyStrokeHelper.AddArrowDownAction(ToggleCheck);
            KeyStrokeHelper.AddArrowUpAction(ToggleCheck);
            KeyStrokeHelper.AddAction(ConsoleKey.LeftArrow, ToggleCheck);
            KeyStrokeHelper.AddAction(ConsoleKey.RightArrow, ToggleCheck);
        }

        private void ToggleCheck()
        {
            CurrentValue = !CurrentValue;
            StateHasChanged(); //i think.
        }

    }
}