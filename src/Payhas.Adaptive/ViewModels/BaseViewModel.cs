using Microsoft.Extensions.DependencyInjection;
using Payhas.Adaptive.Navigations;
using Payhas.Adaptive.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Payhas.Adaptive.ViewModels;

public abstract class BaseViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel
{
    private INavigationManager? _navigationManager;
    protected readonly CompositeDisposable Disposables = [];

    protected BaseViewModel()
    {
        ExceptionHandler = new Interaction<Exception, Unit>();
        CloseCommand = ReactiveCommand.CreateFromTask(Close);

        Activator = new ViewModelActivator();
        this.WhenActivated(async disposables =>
        {
            Disposables.DisposeWith(disposables);

            await OnActivation(disposables);

            Disposable
                .Create(async () => await OnDeactivation())
                .DisposeWith(disposables);

            foreach (var activation in GetActivationContributors())
            {
                await activation.OnActivation(disposables, this);

                Disposable
                    .Create(async () => await activation.OnDeactivation())
                    .DisposeWith(disposables);
            }
        });
    }

    [ObservableAsProperty]
    public bool IsBusy { get; set; }

    public ViewModelActivator Activator { get; }

    public IServiceProvider? ServiceProvider { get; set; }

    public INavigationManager? NavigationManager => _navigationManager ??= ServiceProvider?.GetRequiredService<INavigationManager>();

    public Interaction<Exception, Unit> ExceptionHandler { get; }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public virtual IScreen? HostScreen { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

    public virtual string? UrlPathSegment => GetType().Name;

    public ReactiveCommand<Unit, IRoutableViewModel?> CloseCommand { get; }

    protected virtual IEnumerable<IViewModelActivationContributor> GetActivationContributors()
    {
        var activatorType = typeof(IEnumerable<>).MakeGenericType(
            typeof(IViewModelActivationContributor<>).MakeGenericType(GetType()));

        var activators = ServiceProvider?.GetRequiredService(activatorType)
            ?? Array.Empty<IViewModelActivationContributor>();

        return ((IEnumerable)activators).Cast<IViewModelActivationContributor>();
    }

    protected virtual Task OnActivation(CompositeDisposable disposables)
    {
        if (NavigationManager is IActivatableViewModel activatableViewModel)
        {
            activatableViewModel.Activator
                .Activate()
                .DisposeWith(disposables);
        }

        return Task.CompletedTask;
    }

    protected virtual Task OnDeactivation()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task<IRoutableViewModel?> Close()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return await HostScreen?.Router.NavigateBack.Execute(Unit.Default)
            ?? default;
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public virtual BaseViewModel WithServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        return this;
    }
}
