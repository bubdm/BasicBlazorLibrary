using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.BasicJavascriptClasses
{
    /// <summary>
    /// this class handles the parts that had to be done via javascript for keydown.
    /// other classes are responsible for figuring out what to do with arrow up/down and the backspace.
    /// </summary>
    public class KeystrokeClass : BaseLibraryJavascriptClass
    {
        public KeystrokeClass(IJSRuntime js) : base(js) { }
        protected override string JavascriptFileName => "keyup";
        public async Task InitAsync(ElementReference? element)
        {
            if (element == null)
            {
                return;
            }
            await ModuleTask.InvokeVoidAsync("start", DotNetObjectReference.Create(this), element);
        }
        public Action? ArrowUp;
        public Action? ArrowDown;
        public Action? ProcessBackSpace { get; set; } //can be null.
        [JSInvokable]
        public void KeyUp(int key)
        {
            ConsoleKey consoleKey;
            bool found;
            try
            {
                consoleKey = (ConsoleKey)key;
                found = true;
            }
            catch (Exception)
            {
                return;
            }
            if (found)
            {
                if (consoleKey == ConsoleKey.UpArrow)
                {
                    ArrowUp?.Invoke();
                    return;
                }
                if (consoleKey == ConsoleKey.DownArrow)
                {
                    ArrowDown?.Invoke();
                    return;
                }
                if (consoleKey == ConsoleKey.Backspace)
                {
                    ProcessBackSpace?.Invoke(); //i think if there is nothing, then maybe won't do anything.
                                                //means another class can do something.  if there is a listener, something else has to handle it.
                    return;
                }
            }
        }
    }
}