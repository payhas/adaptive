using Payhas.Adaptive.Navigations;

namespace Payhas.Adaptive.ViewModels;

public class AppViewModel : NavigationRootViewModel
{
    public AppViewModel(
        INavigationManager navigationManager)
        : base(navigationManager, NavigationScope.Base)
    {
    }
}
