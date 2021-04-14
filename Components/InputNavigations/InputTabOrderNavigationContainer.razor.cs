using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicLibraries.CollectionClasses;
using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.InputNavigations
{
    public partial class InputTabOrderNavigationContainer
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        //i think this will go ahead and handle the click part.

        //if i need a way to tab back into something again after somebody manually clicks away, then they have to create a div and listen for the tab.
        //then they can call into the method to focusnext, or focusfirst, etc.
        //the moment they click away from it, this will not know where its at anymore.
        public bool OtherScreen { get; set; } = false; //if you are on other screen, then can ignore some things.
        private ClickInputHelperClass? _clicker;
        private FocusClass? _focusjs;
        private readonly BasicList<IFocusInput> _inputs = new ();
        //i like the idea of this one having the class needed for selecting items.
        private int _currentTab;
        private int _max;
        private int _min;
        protected override void OnInitialized()
        {
            _clicker = new ClickInputHelperClass(JS!);
            _focusjs = new FocusClass(JS!);
            _clicker.InputClicked = (tabindex) =>
            {
                if (tabindex != _currentTab)
                {
                    LoseFocus();
                }
                _currentTab = tabindex;
            };

            _clicker.OtherClicked = LoseFocus;
            base.OnInitialized();
        }
        private async void LoseFocus()
        {
            if (OtherScreen == true)
            {
                return;
            }    
            if (_currentTab <= 0)
            {
                return;
            }
            var input = _inputs.FirstOrDefault(thisitem => thisitem.TabIndex == _currentTab);
            if (input != null)
            {
                await input.LoseFocusAsync();
            }
            _currentTab = 0;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _clicker!.InitAsync();
            }
        }
        public void RemoveFocusItem(IFocusInput input)
        {
            _inputs.RemoveSpecificItem(input);
            if (_inputs.Count == 0)
            {
                _max = 0;
                _min = 0;
            }
            else
            {
                _max = _inputs.Max(thisitem => thisitem.TabIndex);
                _min = _inputs.Min(thisitem => thisitem.TabIndex);
            }  
        }
        public void ClearAllItems() //have the ability to clear all items again.  can help with performance.  give that as an option as well.
        {
            _inputs.Clear();
            _max = 0;
            _min = 0;
        }
        public void AddFocusItem(IFocusInput input)
        {
            //i think this should keep track of them.  may save on memory later.
            //the hardest part here.
            //i think <=0 means automated.
            if (input.TabIndex > 0)
            {
                if (_inputs.Any(thisitem => thisitem.TabIndex == input.TabIndex))
                {
                    throw new CustomBasicException("Duplicate tab indexes.  May eventually require rethinking");
                }
                _inputs.Add(input);
                AnalyzeMinMax(input.TabIndex);
                return;
            }
            //has to see what is maxed.  then add one to it.  maybe that is the best.
            //since we know what is max, maybe easy.
            input.TabIndex = _max + 1;
            AnalyzeMinMax(input.TabIndex); //hopefully this simple.
            _inputs.Add(input);
        }
        //go ahead and have this method just in case i need help with debugging.
        public void ShowTabItems()
        {
            foreach (var item in _inputs)
            {
                Console.WriteLine(item.TabIndex);
            }
        }
        private void AnalyzeMinMax(int tabindex)
        {
            if (tabindex > _max)
            {
                _max = tabindex;
            }
            if (tabindex < _min)
            {
                _min = tabindex;
            }
        }
        private async Task StartFocusAsync(Func<Task> action)
        {
            if (_inputs.Count == 0)
            {
                return;
            }
            if (_inputs.Count == 1)
            {
                _currentTab = _inputs.Single().TabIndex;
                await FocusCurrentAsync();
                return;
            }
            await action.Invoke();
        }
        public async Task FocusFirstAsync()
        {
            await StartFocusAsync(async () =>
            {
                _currentTab = _inputs.Min(thisitem => thisitem.TabIndex); //this is fine.
                await FocusCurrentAsync();
            });
        }
        public async Task FocusAndSelectAsync(ElementReference? element) //this needs the element reference.  that is all this cares about this time.  this provides a way to implement it.
        {
            if (element == null)
            {
                return;
            }
            await _focusjs!.FocusAsync(element);
        }
        public void ResetFocus(IFocusInput input)
        {
            _currentTab = input.TabIndex; //you already set focus otherwise.
        }
        public async Task FocusSpecificInputAsync(IFocusInput input)
        {
            if (input.TabIndex == 0)
            {
                throw new CustomBasicException("TabIndex cannot be 0.  Find out what happened");
            }
            await input.FocusAsync();
            _currentTab = input.TabIndex;
        }
        public async Task FocusLastAsync()
        {
            await StartFocusAsync(async () =>
            {
                //based on tabindex.
                _currentTab = _inputs.Max(thisitem => thisitem.TabIndex); //this is fine.
                await FocusCurrentAsync();
            });
        }
        public async Task FocusCurrentAsync() //good news is if i exited, no runtime error.  the bad news is no setting focus.
        {
            await _inputs.First(thisitem => thisitem.TabIndex == _currentTab).FocusAsync();
        }
        public async Task FocusNextAsync()
        {
            await StartFocusAsync(async () =>
            {
                int next = _currentTab + 1;
                do
                {
                    if (_inputs.Exists(thisitem => thisitem.TabIndex == next) == true)
                    {
                        break; //can break out now.
                    }

                    next++;
                    if (next > _max)
                    {
                        return; //this means there is no more left.  therefore, can return and do noting.
                    }

                } while (true);
                _currentTab = next;
                await FocusCurrentAsync();
            });
        }
        public async Task FocusPreviousAsync()
        {
            await StartFocusAsync(async () =>
            {
                int previous = _currentTab - 1;
                do
                {
                    if (_inputs.Exists(thisitem => thisitem.TabIndex == previous) == true)
                    {
                        break; //can break out now.
                    }
                    previous--;
                    if (previous < _min)
                    {
                        return; //this means there is no more left.  therefore, can return and do noting.
                    }

                } while (true);
                _currentTab = previous;
                await FocusCurrentAsync();
            });
        }
    }
}