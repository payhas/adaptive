using Payhas.Adaptive.Navigations;
using Payhas.Adaptive.Services;
using Payhas.Adaptive.ViewModels;

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
}
