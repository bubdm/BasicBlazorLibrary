using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class BoldLabel
    {
        [Parameter]
        public string Text { get; set; } = "";

        [Parameter]
        public string Style { get; set; } = "";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }



        ////can use off the shelf part for class.
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; } =
        new Dictionary<string, object>()
        {
            { "class", "" }
        };

        [Parameter]
        public bool StartsOnNextLine { get; set; }

        private string GetStyle
        {
            get
            {
                string starts = "font-weight: bold;";

                if (Style != "")
                {
                    string otherText = Style;
                    if (otherText.EndsWith(";") == false)
                    {
                        otherText = $"{otherText};";
                    }
                    return $"{starts} {otherText}";
                }
                return starts;
            }
        }

        //private string GetStyle => $"font-weight: bold; {Style};"; 

    }
}