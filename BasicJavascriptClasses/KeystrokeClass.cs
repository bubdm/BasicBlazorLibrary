using BasicBlazorLibrary.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
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
            await ModuleTask.InvokeVoidFromClassAsync("start", DotNetObjectReference.Create(this), element);
        }



        public void AddAction(ConsoleKey key, Action action)
        {
            _actions.Add(key, action);
        }

        //was going to do fkey but i think its not necessary because you can just use consolekey.f anyways.
        
        public void AddArrowUpAction(Action action)
        {
            AddAction(ConsoleKey.UpArrow, action);
        }
        public void AddArrowDownAction(Action action)
        {
            AddAction(ConsoleKey.DownArrow, action);
        }
        //backspace is not common enough eiher.



        private readonly Dictionary<ConsoleKey, Action> _actions = new Dictionary<ConsoleKey, Action>();


        //public Action? ArrowUp;
        //public Action? ArrowDown;
        //public Action? ProcessBackSpace { get; set; } //can be null.
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
                if (_actions.ContainsKey(consoleKey) == false)
                {
                    return;
                }
                _actions[consoleKey].Invoke();
                //if (consoleKey == ConsoleKey.UpArrow)
                //{
                //    ArrowUp?.Invoke();
                //    return;
                //}
                //if (consoleKey == ConsoleKey.DownArrow)
                //{
                //    ArrowDown?.Invoke();
                //    return;
                //}
                //if (consoleKey == ConsoleKey.Backspace)
                //{
                //    ProcessBackSpace?.Invoke(); //i think if there is nothing, then maybe won't do anything.
                //                                //means another class can do something.  if there is a listener, something else has to handle it.
                //    return;
                //}
            }
        }
    }
}