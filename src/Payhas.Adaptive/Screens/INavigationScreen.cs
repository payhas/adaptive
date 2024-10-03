using Microsoft.Extensions.DependencyInjection;
using Payhas.Adaptive.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace Payhas.Adaptive.Screens;

public interface INavigationScreen : IScreen
{
    Task Clear();

    Task<T> Navigate<T>(Action<T>? propertySetter = null)
        where T : IRoutableViewModel;

    Task<T> NavigateAndReset<T>(Action<T>? propertySetter = null)
        where T : IRoutableViewModel;
}

public class NavigationScreen : ReactiveObject, IScreen, INavigationScreen
{
    public NavigationScreen(IServiceProvider serviceProvider)
    {
        Router = new RoutingState();
        ServiceProvider = serviceProvider;
    }

    public RoutingState Router { get; }

    public IServiceProvider ServiceProvider { get; }

    public virtual async Task<T> Navigate<T>(Action<T>? propertySetter = default)
        where T : IRoutableViewModel
    {
        var vm = GetViewModel(propertySetter);
        await Router.Navigate.Execute(vm);
        return vm;
    }

    public virtual async Task<T> NavigateAndReset<T>(Action<T>? propertySetter = default)
        where T : IRoutableViewModel
    {
        var vm = GetViewModel(propertySetter);
        await Router.NavigateAndReset.Execute(vm);
        return vm;
    }

    protected virtual T GetViewModel<T>(Action<T>? propertySetter = default)
        where T : IRoutableViewModel
    {
        var vm = ServiceProvider.GetRequiredService<T>();
        if (vm is BaseViewModel viewModel)
        {
            viewModel.HostScreen = this;
            viewModel.ServiceProvider = ServiceProvider;
        }

        if (propertySetter != default)
            propertySetter.Invoke(vm);

        return vm;
    }

    public virtual async Task Clear()
    {
        while (Router.NavigationStack.Count > 0)
        {
            await Router.NavigateBack.Execute(Unit.Default);
        }
    }
}
