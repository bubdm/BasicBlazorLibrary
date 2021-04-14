using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.Inputs
{
    public partial class InputEnterDate<TValue>
    {
        private string _value = "";
        private DateTime? _dateChosen; 
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
                UIPlatform.ShowUserErrorToast("Invalid Date");
                CurrentValue = default;
                await ClearTextAsync();
                return;
            }
            _dateChosen = date;
            object temps = _dateChosen!;
            CurrentValue = (TValue)temps;
            _previousValue = CurrentValue;
        }
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
            base.OnInitialized();
            _helps = new TextBoxHelperClass(TabContainer.JS!);
            KeyStrokeHelper.AddAction(ConsoleKey.C, () =>
            {
                if (_dateChosen.HasValue == false)
                {
                    _dateChosen = DateTime.Now;
                }
                _value = "";
                TabContainer.OtherScreen = true;
                StateHasChanged();
            });
        }
        private async Task Cancelled()
        {
            _dateChosen = null;
            await ClearTextAsync();
            TabContainer.OtherScreen = false;
            await TabContainer.FocusSpecificInputAsync(this);
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