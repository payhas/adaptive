using Payhas.Adaptive.Navigations;
using Payhas.Adaptive.Services;
using Payhas.Adaptive.ViewModels;
using ReactiveUI;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionAdaptiveExtensions
{
    public static IServiceCollection AddAdaptive(this IServiceCollection collection)
    {
        return collection
            .AddTransient<INavigationScreen, NavigationScreen>()
            .AddTransient<INavigationManager, NavigationManager>();
    }

    public static IServiceCollection AddAdaptiveViewModelActivationContributor<TViewModel, T>(this IServiceCollection collection)
        where T : class, IViewModelActivationContributor, IViewModelActivationContributor<TViewModel>
        where TViewModel : BaseViewModel
    {
        return collection
            .AddTransient<IViewModelActivationContributor, T>()
            .AddTransient<IViewModelActivationContributor<TViewModel>, T>();
    }

    public static IServiceCollection AddAdaptiveAvaloniaView<TViewModel, TView>(this IServiceCollection collection, bool registerViewModel = false)
        where TView : class, IViewFor, IViewFor<TViewModel>
        where TViewModel : class
    {
        if (registerViewModel)
        {
            collection.AddTransient<TViewModel>();
        }

        return collection
            .AddTransient<TView>()
            .AddTransient<IViewFor, TView>()
            .AddTransient<IViewFor<TViewModel>, TView>();
    }
}
