using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
namespace BasicBlazorLibrary.Components.Basic
{
    public abstract class AttributeBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
        /// properties. Derived components should typically use this value for the primary HTML element's
        /// 'class' attribute.
        /// </summary>
        protected string CssClass => StandardText("class");
        protected string ID => StandardText("id");
        protected string PlaceHolder => StandardText("placeholder");
        protected string StandardText(string text)
        {
            if (AdditionalAttributes != null &&
                    AdditionalAttributes.TryGetValue(text, out var id) &&
                    !string.IsNullOrEmpty(Convert.ToString(id)))
            {
                return id.ToString()!;
            }

            return "";
        }


    }
}
