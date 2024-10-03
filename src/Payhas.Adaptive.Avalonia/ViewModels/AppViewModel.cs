using Payhas.Adaptive.Navigations;

namespace Payhas.Adaptive.ViewModels;

public class AppViewModel : BaseViewModel
{
    public AppViewModel(
        IServiceProvider serviceProvider,
        INavigationManager navigationManager)
        : base(serviceProvider)
    {
        NavigationManager = navigationManager;
    }

    public virtual INavigationManager NavigationManager { get; }
}
