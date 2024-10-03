using Microsoft.Extensions.DependencyInjection;
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
    public INavigationScreen BaseScreen { get; set; }
    public INavigationScreen ContentScreen { get; set; }

    public Task<IRoutableViewModel> Navigate(Type type);

    public Task<T> Navigate<T>()
        where T : IRoutableViewModel;

    public Task<IRoutableViewModel> NavigateBase(Type type);

    public Task<T> NavigateBase<T>()
        where T : IRoutableViewModel;

    public Task<IRoutableViewModel> NavigateContent(Type type);

    public Task<T> NavigateContent<T>()
        where T : IRoutableViewModel;
}

public class NavigationManager : ReactiveObject, INavigationManager, IActivatableViewModel
{
    protected readonly CompositeDisposable Disposables = [];

    public NavigationManager(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;

        BaseScreen = ServiceProvider.GetRequiredService<INavigationScreen>();
        ContentScreen = ServiceProvider.GetRequiredService<INavigationScreen>();

        Activator = new ViewModelActivator();
        this.WhenActivated(disposables =>
        {
            Disposables.DisposeWith(disposables);

            var canGoBackBase = this
                .WhenAnyValue(x => x.BaseScreen.Router.NavigationStack.Count)
                .Select(count => count > 1);
            canGoBackBase.ToPropertyEx(this, x => x.CanGoBackBase)
                .DisposeWith(disposables);

            GoBackBaseCommand = ReactiveCommand.CreateFromObservable(
                    () => BaseScreen.Router.NavigateBack.Execute(Unit.Default),
                    canGoBackBase)
                .DisposeWith(disposables);

            var canGoBackContent = this
                .WhenAnyValue(x => x.ContentScreen.Router.NavigationStack.Count)
                .Select(count => count > 1);
            canGoBackContent.ToPropertyEx(this, x => x.CanGoBackContent)
                .DisposeWith(disposables);

            GoBackContentCommand = ReactiveCommand.CreateFromObservable(
                    () => ContentScreen.Router.NavigateBack.Execute(Unit.Default),
                    canGoBackContent)
                .DisposeWith(disposables);
        });
    }

    public ViewModelActivator Activator { get; }

    protected IServiceProvider ServiceProvider { get; }

    public INavigationManager? Parent { get; set; }
    public INavigationScreen BaseScreen { get; set; }
    public INavigationScreen ContentScreen { get; set; }

    [ObservableAsProperty]
    public virtual bool CanGoBackBase { get; set; }

    [ObservableAsProperty]
    public virtual bool CanGoBackContent { get; set; }

    public virtual ReactiveCommand<Unit, IRoutableViewModel?>? GoBackBaseCommand { get; private set; }

    public virtual ReactiveCommand<Unit, IRoutableViewModel?>? GoBackContentCommand { get; private set; }

    public async Task<IRoutableViewModel> Navigate(Type type)
    {
        var method = GetType().GetMethod(nameof(Navigate), Type.EmptyTypes)!;
        var generic = method.MakeGenericMethod(type);
        var task = (Task)generic.Invoke(this, null)!;

        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result")!;
        return (IRoutableViewModel)resultProperty.GetValue(task)!;
    }

    public Task<T> Navigate<T>()
        where T : IRoutableViewModel
    {
        return typeof(ContentViewModelRoutable).IsAssignableFrom(typeof(T))
            ? NavigateContent<T>()
            : NavigateBase<T>();
    }

    public async Task<IRoutableViewModel> NavigateBase(Type type)
    {
        var method = GetType().GetMethod(nameof(NavigateBase), Type.EmptyTypes)!;
        var generic = method.MakeGenericMethod(type);
        var task = (Task)generic.Invoke(this, null)!;

        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result")!;
        return (IRoutableViewModel)resultProperty.GetValue(task)!;
    }

    public virtual Task<T> NavigateBase<T>()
        where T : IRoutableViewModel
    {
        return BaseScreen?.Navigate<T>()
            ?? Parent!.NavigateBase<T>();
    }

    public async Task<IRoutableViewModel> NavigateContent(Type type)
    {
        var method = GetType().GetMethod(nameof(NavigateContent), Type.EmptyTypes)!;
        var generic = method.MakeGenericMethod(type);
        var task = (Task)generic.Invoke(this, null)!;

        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result")!;
        return (IRoutableViewModel)resultProperty.GetValue(task)!;
    }

    public virtual Task<T> NavigateContent<T>()
        where T : IRoutableViewModel
    {
        return ContentScreen?.Navigate<T>()
            ?? NavigateBase<T>();
    }
}
