using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BasicBlazorLibrary.Components.Layouts.Simple
{
    public partial class StackItem
    {
        [CascadingParameter]
        private StackLayout? Stack { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public string Style { get; set; } = "";

        /// <summary>
        /// Horizontal alignement (Example: "@Justify.Center")
        /// </summary>
        [Parameter]
        public string HorizontalAlignment { get; set; } = "";

        /// <summary>
        /// Vertical alignemtn (Example:"@Justify.Center")
        /// </summary>
        [Parameter]
        public string VerticalAlignment { get; set; } = "";

        /// <summary>
        /// Control horizontal scrollbar- None, Show, Auto
        /// </summary>
        [Parameter]
        public string HorizontalScrollbar { get; set; } = "";

        /// <summary>
        /// Control vertical scrollbar- None, Show, Auto
        /// </summary>
        [Parameter]
        public string VerticalScrollbar { get; set; } = "";

        [Parameter]
        public string Length { get; set; } = "max-content"; //this is used for the parent.  i think it should default to the maximum content.  can change as needed though.  but default needs to be that.
        [Parameter]
        public bool AlignEnd { get; set; } //if true, will do what is necessary to align to the end.
        private int GetVisibleStyle
        {
            get
            {
                if (Visible)
                {
                    return 1;
                }
                return 0;
            }
        }


        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public string BackgroundColor { get; set; } = "transparent";

        private int _column;
        private int _row;


        protected override void OnInitialized()
        {
            int index = Stack!.AddChild(this);
            if (Stack.Orientation == EnumOrientation.Horizontal)
            {
                _column = index;
            }
            else
            {
                _row = index;
            }
            Stack.Refresh(); //has to set the row or column first.
        }

        private string GetStyle()
        {
            StringBuilder sb = new ();

            if (_column > 0)
            {

                sb.Append($"grid-column: {_column};");

            }

            if (_row > 0)
            {
                sb.Append($"grid-row: {_row};");
            }

            if (HorizontalAlignment != null)
            {
                sb.Append($"justify-content: {HorizontalAlignment}");
            }

            if (VerticalAlignment != null)
            {
                sb.Append($"align-content: {VerticalAlignment}");
            }

            //since this part worked so well, go ahead and do this.  the advantage of having repeating code for this would be for performance.

            sb.Append($"overflow-x: {HorizontalScrollbar ?? "hidden"};"); //i guess its okay this time.
            sb.Append($"overflow-y: {VerticalScrollbar ?? "hidden"};");
            sb.Append($"opacity: {GetVisibleStyle};");

            if (BackgroundColor != "transparent")
            {
                sb.Append($"background-color: {BackgroundColor};");
            }
            if (AlignEnd)
            {
                sb.Append("display: flex; justify-content: flex-end;");
            }

            if (Style != "")
            {
                sb.Append(Style);
            }

            return sb.ToString();
        }
    }
}