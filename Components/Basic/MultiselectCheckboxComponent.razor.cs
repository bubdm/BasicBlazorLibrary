using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Reflection.Metadata;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace BasicBlazorLibrary.Components.Basic
{
    public partial class MultiselectCheckboxComponent<TValue>
    {
        private static string GetColumns => aa.RepeatMaximum(2);
        [Parameter]
        public CustomBasicList<TValue> Data { get; set; } = new();
        [Parameter]
        public CustomBasicList<TValue> Values { get; set; } = new();
        [Parameter]
        public Func<TValue, string>? RetrieveValue { get; set; }
        [Parameter]
        public EventCallback<CustomBasicList<TValue>> ValuesChanged { get; set; } //this is for data binding.
        private bool IsSelected(TValue info)
        {
            return Values.Any(xxx => xxx!.Equals(info));
        }
        public void OnCheckboxChanged(TValue item, object wasChecked)
        {
            bool rets = Convert.ToBoolean(wasChecked);
            if (rets)
            {
                AddSelectedItem(item);
                return;
            }
            RemoveSelectedItem(item);
        }
        private void AddSelectedItem(TValue item, bool doCheck = true)
        {
            if (doCheck)
            {
                if (IsSelected(item))
                {
                    return; //because already added.  can't have duplicates.
                }
            }
            CustomBasicList<TValue> temps = Values.ToCustomBasicList();
            temps.Add(item);
            ValuesChanged.InvokeAsync(temps); //they will send back again because of bindings.
        }
        private void RemoveSelectedItem(TValue item, bool doCheck = true)
        {
            if (doCheck)
            {
                if (IsSelected(item) == false)
                {
                    return; //because already not there
                }
            }
            CustomBasicList<TValue> temps = Values.ToCustomBasicList();
            temps.RemoveSpecificItem(item);
            ValuesChanged.InvokeAsync(temps);
        }
        private void SelectUnselectItem(TValue item)
        {
            if (IsSelected(item))
            {
                RemoveSelectedItem(item, false);
                return;
            }
            AddSelectedItem(item, false);
        }
    }
}