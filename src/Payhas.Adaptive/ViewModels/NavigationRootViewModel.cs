using Payhas.Adaptive.Navigations;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Payhas.Adaptive.ViewModels;

public abstract class NavigationRootViewModel : BaseViewModel
{
    protected NavigationRootViewModel(
        INavigationManager navigationManager,
        NavigationScope navigationScope)
    {
        NavigationManager = navigationManager;
        NavigationManager.Scope = navigationScope;
    }

    protected override async Task OnActivation(CompositeDisposable disposables)
    {
        if (NavigationManager is IActivatableViewModel activatableViewModel)
        {
            activatableViewModel.Activator
                .Activate()
                .DisposeWith(disposables);
        }

        await base.OnActivation(disposables);
    }
}
