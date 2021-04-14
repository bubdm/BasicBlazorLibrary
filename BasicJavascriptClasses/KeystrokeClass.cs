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
        internal enum EnumOtherKeyCategory
        {
            None = 0,
            Shift = 16,
            Control = 17,
            Alt = 18
        }
        public KeystrokeClass(IJSRuntime js) : base(js) { }
        protected override string JavascriptFileName => "keystroke";
        private bool _didInit = false;
        public async Task InitAsync(ElementReference? element)
        {
            if (element == null || _didInit == true)
            {
                return;
            }
            _didInit = true; //has to be before.  otherwise, can have bad timing (causing issues).
            await ModuleTask.InvokeVoidFromClassAsync("start", DotNetObjectReference.Create(this), element);
        }
        public void AddAction(ConsoleKey key, Action action)
        {
            _simpleActions.Add(key, action);
        }
        public void AddShiftTab(Action action)
        {
            _shiftActions.Add(ConsoleKey.Tab, action);
        }
        public void AddShiftAction(ConsoleKey key, Action action)
        {
            _shiftActions.Add(key, action);
        }
        public void AddControlAction(ConsoleKey key, Action action)
        {
            _controlActions.Add(key, action);
        }
        public void AddAltAction(ConsoleKey key, Action action)
        {
            _altActions.Add(key, action);
        }
        public void AddArrowUpAction(Action action)
        {
            AddAction(ConsoleKey.UpArrow, action);
        }
        public void AddArrowDownAction(Action action)
        {
            AddAction(ConsoleKey.DownArrow, action);
        }
        public void RemoveAllActions()
        {
            _simpleActions.Clear();
            _shiftActions.Clear();
            _altActions.Clear();
            _controlActions.Clear();
        }
        private readonly Dictionary<ConsoleKey, Action> _simpleActions = new ();
        private readonly Dictionary<ConsoleKey, Action> _shiftActions = new ();
        private readonly Dictionary<ConsoleKey, Action> _altActions = new ();
        private readonly Dictionary<ConsoleKey, Action> _controlActions = new ();
        private EnumOtherKeyCategory _oldKey = EnumOtherKeyCategory.None;
        private bool _needsreleased;
        [JSInvokable]
        public void KeyDown(int key)
        {
            if (_needsreleased)
            {
                return;
            }
            _oldKey = key switch
            {
                16 => EnumOtherKeyCategory.Shift,
                17 => EnumOtherKeyCategory.Control,
                18 => EnumOtherKeyCategory.Alt,
                _ => EnumOtherKeyCategory.None
            };
            _needsreleased = true;
        }
        [JSInvokable]
        public void KeyUp(int key) //shift is 16.
        {
            //16 is one.  well see about control or right shift.
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
                _needsreleased = false;
                if (_oldKey == EnumOtherKeyCategory.Shift)
                {
                    if (_shiftActions.ContainsKey(consoleKey))
                    {
                        _shiftActions[consoleKey].Invoke();
                        return;
                    }
                }
                if (_oldKey == EnumOtherKeyCategory.Control)
                {
                    if (_controlActions.ContainsKey(consoleKey))
                    {
                        _controlActions[consoleKey].Invoke();
                        return;
                    }
                }
                if (_oldKey == EnumOtherKeyCategory.Alt)
                {
                    if (_altActions.ContainsKey(consoleKey))
                    {
                        _altActions[consoleKey].Invoke();
                        return;
                    }
                }
                if (_simpleActions.ContainsKey(consoleKey) == false)
                {
                    return;
                }
                _simpleActions[consoleKey].Invoke();

            }
        }
    }
}