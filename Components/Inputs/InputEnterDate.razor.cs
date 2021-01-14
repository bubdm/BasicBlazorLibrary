using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterDate<TValue>
    {
        private string _value = "";
        private DateTime? _dateChosen; //needs full control of when it puts to value (i prefer just doing the event notifications).
        //try to do without external component this time.

        private TextBoxHelperClass? _helps;



        public override async Task LoseFocusAsync()
        {
            _value = await _helps!.GetValueAsync(InputElement);
            if (_value == "")
            {
                CurrentValue = default;
                //no need to clear because its already done.
                return;
            }
            bool rets = _value.IsValidDate(out DateTime? date);
            if (rets == false)
            {
                ToastPlatform.ShowError("Invalid Date");
                CurrentValue = default;
                await ClearTextAsync();
                return;
            }
            _dateChosen = date;
            object temps = _dateChosen!;
            CurrentValue = (TValue)temps;
            _previousValue = CurrentValue; //try this too.
            //probably no need to do the update.
        }

        //public override void LoseFocus()
        //{
        //    if (_value == "")
        //    {
        //        ToastPlatform.ShowError("Problem");
        //    }
        //    if (_value == "")
        //    {
        //        CurrentValue = default;
        //        SetText("");
        //        return;
        //    }
        //    bool rets = _value.IsValidDate(out DateTime? date);
        //    if (rets == false)
        //    {
        //        ToastPlatform.ShowError("Invalid Date");
        //        CurrentValue = default;
        //        SetText("");
        //        return;
        //    }
        //    _dateChosen = date;
        //    object temps = _dateChosen!;
        //    CurrentValue = (TValue)temps; //hopefully this simple.
        //}
        private bool _invalid;
        private static string GetFormattedDate(DateTime date)
        {
            string dayString = date.Day.ToString("00");
            string monthString = date.Month.ToString("00");
            string yearString = date.Year.ToString("00");
            return $"{monthString}{dayString}{yearString}";
        }
        protected override void OnInitialized()
        {
            base.OnInitialized(); //has to do this first.
            _helps = new TextBoxHelperClass(TabContainer.JS!);


            KeyStrokeHelper.AddAction(ConsoleKey.C, () =>
            {
                if (_dateChosen.HasValue == false)
                {
                    _dateChosen = DateTime.Now; //has to be set.  otherwise, no popup because no date found.
                }
                _value = "";
                TabContainer.OtherScreen = true;
                StateHasChanged(); //i think.
            });
        }
        private async Task Cancelled()
        {
            _dateChosen = null;
            await ClearTextAsync();
            TabContainer.OtherScreen = false;
            await TabContainer.FocusSpecificInputAsync(this); //so tab orders are proper.
        }
        private async Task ChoseDate()
        {
            DateTime date = _dateChosen!.Value;
            _value = GetFormattedDate(date); //not sure if settext is needed or not (?)
            await _helps!.SetNewValueAloneAsync(InputElement, _value); //try this way.
            TabContainer.OtherScreen = false;
            ProcessEnter();
        }
        private TValue? _previousValue;
        protected override void OnParametersSet()
        {
            if (TabContainer.OtherScreen)
            {
                return;
            }
            if (_previousValue!.Equals(Value))
            {
                return; //no need to go through the processes if that did not change.
            }
            _previousValue = Value;
            if (Value == null || Value.Equals(default))
            {
                _dateChosen = null;
                _invalid = false;
            }
            else if (Value is DateTime aa)
            {
                _invalid = false;
                if (aa.Equals(default))
                {
                    _dateChosen = null;
                }
                else
                {
                    _dateChosen = aa;
                }
            }
            else
            {
                _dateChosen = null;
                _invalid = true;
            }
            if (_dateChosen == null)
            {
                _value = "";
            }
            else
            {
                _value = GetFormattedDate(_dateChosen.Value);
            }
            base.OnParametersSet();
        }
        private async Task ClearTextAsync()
        {
            await _helps!.ClearTextAsync(InputElement);
            _value = "";
        }
        protected override async Task OnFirstRenderAsync()
        {
            await _helps!.SetInitTextAsync(InputElement, _value);
        }
    }
}