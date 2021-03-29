using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Basic
{
    public partial class RadioButtonGroupComponent
    {
        //this can work even if doing forms.
        //because this does not care about any form stuff.  its intended to group everything together.
        //for now, calling as radiobuttongroup is fine.  since its intended for radio buttons.
        [Parameter]
        public string Name { get; set; } = "";

        //if in a table, then before doing the row tags, do the grouping.
        //if i need a form item, will do later.


        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}