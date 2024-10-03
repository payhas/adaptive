using Payhas.Adaptive.Screens;
using Payhas.Adaptive.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Payhas.Adaptive.Navigations;

public interface INavigationManager
{
    public INavigationManager? Parent { get; set; }
    public INavigationScreen Screen { get; set; }
    public NavigationScope Scope { get; set; }

    public Task<T> Navigate<T>(NavigationScope? scope, Action<T>? propertySetter = default)
        where T : IRoutableViewModel;
}

public class NavigationManager : ReactiveObject, INavigationManager, IActivatableViewModel
{
    protected readonly CompositeDisposable Disposables = [];

    public NavigationManager(
        INavigationScreen screen)
    {
        Screen = screen;

        Activator = new ViewModelActivator();
        this.WhenActivated(disposables =>
        {
            Disposables.DisposeWith(disposables);

            var canGoBack = this
                .WhenAnyValue(x => x.Screen.Router.NavigationStack.Count)
                .Select(count => count > 1);
            canGoBack.ToPropertyEx(this, x => x.CanGoBack)
                .DisposeWith(disposables);

            GoBackCommand = ReactiveCommand.CreateFromObservable(
                    () => Screen.Router.NavigateBack.Execute(Unit.Default),
                    canGoBack)
                .DisposeWith(disposables);
        });
    }

    public ViewModelActivator Activator { get; }

    public INavigationManager? Parent { get; set; }
    public INavigationScreen Screen { get; set; }
    public NavigationScope Scope { get; set; }
        = NavigationScope.Base;

    [ObservableAsProperty]
    public virtual bool CanGoBack { get; set; }

    public virtual ReactiveCommand<Unit, IRoutableViewModel?>? GoBackCommand { get; private set; }

    public Task<T> Navigate<T>(NavigationScope? scope, Action<T>? propertySetter = default)
        where T : IRoutableViewModel
    {
        return scope == null || scope == Scope
            ? Screen.Navigate<T>(GetMergerPropertySetter(propertySetter))
            : Parent!.Navigate<T>(scope, propertySetter);
    }

    protected virtual Action<T> GetMergerPropertySetter<T>(Action<T>? propertySetter = default)
        where T : IRoutableViewModel
    {
        return vm =>
        {
            if (vm is BaseViewModel baseViewModel)
            {
                if (baseViewModel.NavigationManager != null)
                {
                    baseViewModel.NavigationManager.Parent = this;
                }
                else
                {
                    baseViewModel.NavigationManager = this;
                }
            }

            propertySetter?.Invoke(vm);
        };
    }
}
