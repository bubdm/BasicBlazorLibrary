using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.Basic
{
    public abstract class KeyComponentBase : ComponentBase
    {
        protected static string GetKey => Guid.NewGuid().ToString(); //i like this idea.  since its static, it means even if does not inherit from keycomponentbase, i can still access it anyway from anywhere
    }
}