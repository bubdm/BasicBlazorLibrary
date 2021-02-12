using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;

namespace BasicBlazorLibrary.Components.Basic
{
    public partial class MobileListBox<TValue>
    {
        [Parameter]
        public CustomBasicList<TValue> Data { get; set; } = new();

        //for now, don't worry about styles too much.
        [Parameter]
        public string FontSize { get; set; } = "7vmin"; //to make it easy to choose something.

        //since its designed for mobile, then no need for hovering since mobile does not do hovering.
        
        [Parameter]
        public EventCallback<TValue> OnItemSelected { get; set; }

        [Parameter]
        public RenderFragment<TValue>? ChildContent { get; set; }
        //using the browsers scrolling is fine.  if something else handles scrolling, fine too.
        //this will not have any autoscrolling.
        //its intended to use for mobile.

    }
}