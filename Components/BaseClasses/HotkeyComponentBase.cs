using BasicBlazorLibrary.BasicJavascriptClasses;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.BaseClasses
{
    public abstract class HotkeyComponentBase : JavascriptComponentBase
    {
        //this needs the javascript so if anything else needs javascript, might as well have that as well.
        /// <summary>
        /// this is the element used for the hotkey system.
        /// </summary>
        protected ElementReference? MainElement { get; set; }
        protected KeystrokeClass? Key;
        protected virtual bool FocusOnFirst { get; set; }

        //protected bool CanStartup { get; set; } = true;


        protected async Task InitAsync()
        {
            if (MainElement.HasValue == false)
            {
                throw new BasicBlankException("No main element was set.  The child class has to set up the main element.  Otherwise, unable to use hotkeys.  Try to make sure its not even used until needed.");
            }
            await Key!.InitAsync(MainElement);
            if (FocusOnFirst)
            {
                await MainElement.Value.FocusAsync();
            }
        }

        


        protected override void OnInitialized()
        {
            MainElement = null;
            Key = new KeystrokeClass(JS!);
            base.OnInitialized();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitAsync();
                
            }
        }
    }
}