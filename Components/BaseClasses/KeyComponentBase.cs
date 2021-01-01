using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class KeyComponentBase : ComponentBase
    {
        protected static string GetKey => Guid.NewGuid().ToString(); //if needed, then do this way.  maybe not needed but you never know.
    }
}