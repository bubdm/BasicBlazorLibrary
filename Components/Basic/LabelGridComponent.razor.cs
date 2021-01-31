using System.Reflection;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class LabelGridComponent
    {

        //this can be a good place to use code generators in place of reflection.

        [Parameter]
        public CustomBasicList<LabelGridModel> Labels { get; set; } = new CustomBasicList<LabelGridModel>();
        [Parameter]
        public object? DataContext { get; set; } //may have a better way of doing this.

        [Parameter]
        public string Width { get; set; } = "";

        [Parameter]
        public string Height { get; set; } = "";

        [Parameter]
        public int DecimalPlaces { get; set; } = 2; //so if 0, then can do as well
        private string GetValue(LabelGridModel label)
        {
            PropertyInfo? p = DataContext!.GetType().GetProperty(label.Name);
            object temps = p!.GetValue(DataContext)!;
            if (temps == null)
            {
                return "";
            }
            if (p.PropertyType.Name == "Decimal")
            {
                decimal amount = decimal.Parse(temps.ToString()!);
                return amount.ToCurrency(DecimalPlaces);
            }
            if (p.Name.ToLower().StartsWith("roll"))
            {
                if (temps is int nexts)
                {
                    int incrs = nexts - 1; //try this way for now.
                    if (incrs == -1)
                    {
                        incrs = 0; //i don't think that -1 is ever valid.
                    }
                    return incrs.ToString(); //to accomodate a game package.  unless i find a better way of doing this.
                }
            }
            return temps.ToString()!; //unless rethinking is required.
        }
    }
}