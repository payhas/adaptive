using Payhas.Adaptive.Navigations;

namespace Payhas.Adaptive.ViewModels;

public class AppMainViewModel : NavigationRootViewModel
{
    public AppMainViewModel(
        INavigationManager navigationManager)
        : base(navigationManager, NavigationScope.Base)
    {
    }

    public string Greeting => "Welcome to Avalonia! Updated!";
}
