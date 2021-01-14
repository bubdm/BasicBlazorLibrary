using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicBlazorLibrary.Components.CalendarPopups
{
    public partial class CalendarSimpleModal<TValue>
    {
        [Parameter]
        public EventCallback Cancelled { get; set; } //this means that it was cancelled.
        [Parameter]
        public EventCallback ChoseDate { get; set; } //this means the client needs to tell it to close.  but also, can keep the date chosen.
        private static string GetColumns() => aa.RepeatSpreadOut(7);
        private static string GetRows() => aa.RepeatSpreadOut(8);
        [Parameter]
        public TValue? DateToDisplay { get; set; }
        private DateTime? _todisplay;
        [Parameter]
        public EventCallback<TValue> DateToDisplayChanged { get; set; } //by invoking the event, the other would be populated automatically.  i like this idea.
        private string GetColorStyle(DateTime date)
        {
            if (_todisplay.HasValue)
            {
                if (_todisplay.Value.Day == date.Day)
                {
                    return $"background-color: {cc.Aqua.ToWebColor()};";
                }
            }
            return "";
        }
        private string _monthLabel = "";
        private readonly CustomBasicList<string> _dayList = new CustomBasicList<string>()
        {
            DayOfWeek.Sunday.DayOfWeekShort(),
            DayOfWeek.Monday.DayOfWeekShort(),
            DayOfWeek.Tuesday.DayOfWeekShort(),
            DayOfWeek.Wednesday.DayOfWeekShort(),
            DayOfWeek.Thursday.DayOfWeekShort(),
            DayOfWeek.Friday.DayOfWeekShort(),
            DayOfWeek.Saturday.DayOfWeekShort()
        };
        private ElementReference? _text;
        private string _realValue = "";
        protected override void OnInitialized()
        {
            _text = null;
            base.OnInitialized(); //now, base has to be called first so the required objects are there
#pragma warning disable CA2012 // Use ValueTasks correctly
            Key!.AddAction(ConsoleKey.F1, () => _text!.Value.FocusAsync());
#pragma warning restore CA2012 // Use ValueTasks correctly
            //somehow worked properly for my case though.
            Key.AddAction(ConsoleKey.F2, () => DayClicked(DateTime.Now));
            Key.AddAction(ConsoleKey.C, () => ClearText());
            Key.AddAction(ConsoleKey.X, () => Cancelled.InvokeAsync());
            Key.AddArrowUpAction(() =>
            {
                _todisplay = _todisplay!.Value.AddDays(-7);
                DayClicked(_todisplay.Value);
            });
            Key.AddArrowDownAction(() =>
            {
                _todisplay = _todisplay!.Value.AddDays(7);
                DayClicked(_todisplay.Value);
            });
            Key.AddAction(ConsoleKey.LeftArrow, () =>
            {
                _todisplay = _todisplay!.Value.AddDays(-1);
                DayClicked(_todisplay.Value);
            });
            Key.AddAction(ConsoleKey.RightArrow, () =>
            {
                _todisplay = _todisplay!.Value.AddDays(1);
                DayClicked(_todisplay.Value);
            });
            Key.AddAction(ConsoleKey.Enter, () =>
            {
                if (_realValue == "")
                {
                    object temps = _todisplay!;
                    DateTime? selectedDate = (DateTime?)temps;
                    if (selectedDate.HasValue == false)
                    {
                        ToastPlatform.ShowInfo("There was no date.  Rethink"); //needs toasts afterall.
                        return;
                    }

                    DayClicked(_todisplay!.Value);
                    ChoseDate.InvokeAsync();
                    return;
                }
                bool rets = _realValue.IsValidDate(out DateTime? date);
                if (rets == false)
                {
                    ToastPlatform.ShowError("Invalid Date"); //i think.
                    ClearText(); //clear text in this case.
                    return;
                }
                Console.WriteLine("Date Clicked");
                DayClicked(date!.Value);
            });
        }
        protected override bool ShouldRender()
        {
            return DateToDisplay != null;
        }
        private async void ClearText()
        {
            await Task.Delay(10);
            _realValue = "";
            StateHasChanged(); //try this way.
        }
        private bool _invalid;
        protected override void OnParametersSet()
        {
            if (_todisplay.Equals(DateToDisplay))
            {
                return;
            }
            if (DateToDisplay == null)
            {
                return;
            }
            if (DateToDisplay is DateTime temps)
            {
                _todisplay = temps;
            }
            else
            {
                _invalid = true;
                _todisplay = null;
                return;
            }
            _realValue = ""; //always set to blank when first going to screen.
            _monthLabel = _todisplay.Value.FirstDayStringMonth();
            DateTime start = _todisplay.Value.FirstDayOfMonth();
            DateTime end = _todisplay.Value.LastDayOfMonth();
            int howMany = _todisplay.Value.DaysInMonth();
            _dates = new();
            DateTime current = start;
            howMany.Times(x =>
            {
                _dates.Add(GetSpot(current, start, howMany));
                current = current.AddDays(1);
            });
            _needsFocus = true;
            base.OnParametersSet();
        }
        private static DateSpot GetSpot(DateTime date, DateTime start, int howMany)
        {
            DateSpot output = new DateSpot();
            output.Column = date.DayOfWeek.DayOfWeekColumn();
            output.Date = date;
            int row = 3; //maybe this is correct now (?)
            int upto = 0;
            DateTime dateAt = start;
            do
            {
                upto++;
                if (upto > howMany)
                {
                    throw new BasicBlankException("To the end for dates.  Rethink");
                }
                if (dateAt.Date == date.Date)
                {
                    output.Row = row;
                    return output;
                }
                dateAt = dateAt.AddDays(1);
                if (dateAt.DayOfWeek == DayOfWeek.Sunday)
                {
                    row++;
                }

            } while (true);
        }
        private void DayClicked(DateTime day)
        {
            object ourvalue = day;
            DateToDisplayChanged.InvokeAsync((TValue?)ourvalue);
        }
        private bool _ran;
        private CustomBasicList<DateSpot> _dates = new CustomBasicList<DateSpot>();
        private bool _needsFocus = true;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (MainElement == null)
            {
                return; //can't do because this is not even there yet.
            }
            if (_ran == false)
            {
                await InitAsync();
            }
            if (_needsFocus)
            {
                await _text!.Value.FocusAsync(); //try this too.
                _needsFocus = false;
            }
            _ran = true;
        }
    }
}