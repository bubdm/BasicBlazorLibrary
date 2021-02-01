using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BasicBlazorLibrary.Components.Layouts.Simple
{
    public partial class StackLayout
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EnumOrientation Orientation { get; set; } = EnumOrientation.Vertical;
        public ElementReference? MainElement { get; set; }
        [Parameter]
        public EnumOverflowCategory Overflow { get; set; } = EnumOverflowCategory.Clip; //i think default should be being cut off if necessary (to give hints of problems)
        [Parameter]
        public string Width { get; set; } = "100%";
        [Parameter]
        public string Height { get; set; } = "100%";
        [Parameter]
        public string Style { get; set; } = "";

        /// <summary>
        /// If set to true then the layout of the grid will be "inline" instead of stretching to fill the container.
        /// </summary>
        /// <value>Default is false</value>
        [Parameter]
        public bool Inline { get; set; }

        [Parameter]
        public string ItemSpacing { get; set; } = "3px"; //i think itemspacing needs to be in pixels this time.  could eventually allow for more flexibility.
        public void Refresh()
        {
            StateHasChanged();
        }
        private readonly CustomBasicList<StackItem> _stackList = new();
        public int AddChild(StackItem child)
        {
            _stackList.Add(child);
            int output = _stackList.Count;
            return output;

        }

        private string GetOverflow => Overflow switch
        {
            EnumOverflowCategory.Auto => "",
            EnumOverflowCategory.Clip => "hidden",
            EnumOverflowCategory.Scrollable => "auto",
            _ => "hidden"
        };

        private string GetStyle()
        {
            StringBuilder sb = new StringBuilder();
            if (Height != "")
            {
                sb.Append($"height: {Height};");
            }

            if (Width != "")
            {
                sb.Append($"width: {Width};");
            }

            if (Inline)
            {
                sb.Append("display: inline-grid;");
            }
            else
            {
                sb.Append("display: grid;");
            }

            StrCat cats = new StrCat();
            _stackList.ForEach(xxx =>
            {
                cats.AddToString(xxx.Length, " ");
            });

            if (Orientation == EnumOrientation.Horizontal && ItemSpacing != "")
            {
                sb.Append($"grid-column-gap: {ItemSpacing};");
            }
            if (Orientation == EnumOrientation.Vertical && ItemSpacing != "")
            {
                sb.Append($"grid-row-gap: {ItemSpacing};");
            }
            if (Orientation == EnumOrientation.Horizontal)
            {
                sb.Append($"grid-template-columns: {cats.GetInfo()};");
            }

            else
            {
                sb.Append($"grid-template-rows: {cats.GetInfo()};");
            }
            sb.Append($"overflow: {GetOverflow};");
            if (Style != "")
            {
                sb.Append(Style);
            }

            return sb.ToString();
        }
    }
}