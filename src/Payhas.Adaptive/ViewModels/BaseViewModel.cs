using Microsoft.Extensions.DependencyInjection;
using Payhas.Adaptive.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Payhas.Adaptive.ViewModels;

public abstract class BaseViewModel : ReactiveObject, IActivatableViewModel
{
    protected readonly CompositeDisposable Disposables = [];

    protected BaseViewModel(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;

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

    public ViewModelActivator Activator { get; }

    public IServiceProvider ServiceProvider { get; set; }

    [ObservableAsProperty]
    public bool IsBusy { get; set; }

    protected virtual IEnumerable<IViewModelActivationContributor> GetActivationContributors()
    {
        var activatorType = typeof(IEnumerable<>).MakeGenericType(
            typeof(IViewModelActivationContributor<>).MakeGenericType(GetType()));

        var activators = ServiceProvider.GetRequiredService(activatorType)
            ?? Array.Empty<IViewModelActivationContributor>();

        return ((IEnumerable)activators).Cast<IViewModelActivationContributor>();
    }

    protected virtual Task OnActivation(CompositeDisposable disposables)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnDeactivation()
    {
        return Task.CompletedTask;
    }
}
