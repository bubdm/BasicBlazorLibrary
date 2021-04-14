using System.Threading.Tasks;
namespace BasicBlazorLibrary.Components.InputNavigations
{
    public interface IFocusInput
    {
        int TabIndex { get; set; }
        Task FocusAsync();
        Task LoseFocusAsync(); //because if somebody manually clicks on something, then needs to show it lost focus.
    }
}