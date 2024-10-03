using Payhas.Adaptive.ViewModels;
using System.Reactive.Disposables;

namespace Payhas.Adaptive.Services;

public interface IViewModelActivationContributor<T> : IViewModelActivationContributor
        where T : BaseViewModel
{
}

public interface IViewModelActivationContributor
{
    Task OnActivation(CompositeDisposable disposables, object viewModel);

    Task OnDeactivation();
}

public abstract class ViewModelActivationContributor<T> : ViewModelActivationContributor, IViewModelActivationContributor<T>
        where T : BaseViewModel
{
    public override Task OnActivation(CompositeDisposable disposables, object viewModel)
    {
        return viewModel is T baseViewModel
            ? OnActivation(disposables, baseViewModel)
            : Task.CompletedTask;
    }

    public abstract Task OnActivation(CompositeDisposable disposables, T viewModel);
}

public abstract class ViewModelActivationContributor : IViewModelActivationContributor
{
    public abstract Task OnActivation(CompositeDisposable disposables, object viewModel);
    public virtual Task OnDeactivation()
    {
        return Task.CompletedTask;
    }
}
