using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.InputNavigations
{
    public interface IFocusInput
    {
        int TabIndex { get; set; }
        Task FocusAsync();
        void LoseFocus(); //because if somebody manually clicks on something, then needs to show it lost focus.
        //the date picker when doing the popup does not count.
        //the date picker will not use the system either.
    }
}