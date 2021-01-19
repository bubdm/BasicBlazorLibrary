using BasicBlazorLibrary.Components.MediaQueries.ParentClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Data;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.MediaQueries.MediaListUseClasses
{
    public partial class FlexibleOrientationComponent
    {
        [CascadingParameter]
        private MediaQueryListComponent? MediaList { get; set; } //anybody can use it if needed anyways.
        [Parameter]
        public RenderFragment? MainContent { get; set; }
        [Parameter]
        public RenderFragment? SideContent { get; set; }
        [Parameter]
        public RenderFragment? HeaderContent { get; set; }

        
        //i think always the same.
        private static string GetColumns => aa.RepeatMaximum(2);

        //private static string GetColumns => $"{aa.RepeatMaximum(1)}{aa.RepeatSpreadOut(1)}";


        //private string GetColumns()
        //{
        //    if (HeaderContent == null)
        //    {
        //        return aa.RepeatMaximum(2);
        //    }
        //    //this means 3 columns but content will span across 2 of them.
        //    //return $"{aa.RepeatMinimum(2)}{aa.RepeatSpreadOut(1)}";
        //    return aa.RepeatMaximum(2);
        //}



        private string GetRows(bool horizontal)
        {
            if(horizontal == false)
            {
                if (HeaderContent == null)
                {
                    return aa.RepeatAuto(2);
                }
                return aa.RepeatAuto(3);
            }
            if (HeaderContent == null)
            {
                return "auto"; //i think
            }
            return aa.RepeatMaximum(2); //i think
        }

    }
}