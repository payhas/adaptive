using Payhas.Adaptive.Services;
using Payhas.Adaptive.ViewModels;
using Payhas.Adaptive.Views;
using ReactiveUI;
using Splat;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionAdaptiveAvaloniaExtensions
{
    public static IServiceCollection AddAdaptiveAvalonia(this IServiceCollection collection)
    {
        return collection
            .AddTransient<AppViewModel>()
            .AddTransient<AppMainViewModel>()
            .AddSingleton<IViewLocator, AppViewLocator>()
            .AddAdaptiveAvaloniaView<AppViewModel, AppWindow>()
            .AddAdaptiveAvaloniaView<AppMainViewModel, AppMainView>();
    }

    public static IServiceCollection AddAdaptiveAvaloniaView<TViewModel, TView>(this IServiceCollection collection)
        where TView : class, IViewFor, IViewFor<TViewModel>
        where TViewModel : class
    {
        return collection
            .AddTransient<TView>()
            .AddTransient<IViewFor, TView>()
            .AddTransient<IViewFor<TViewModel>, TView>();
    }
}

public static class ServiceProviderAdaptiveAvaloniaExtensions
{
    public static void UseRxViewLocator(this IServiceProvider services)
    {
        Locator.CurrentMutable.RegisterLazySingleton(services.GetRequiredService<IViewLocator>);
    }
}
