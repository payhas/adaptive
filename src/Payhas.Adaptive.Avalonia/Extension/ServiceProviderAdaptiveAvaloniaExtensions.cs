using ReactiveUI;
using Splat;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProviderAdaptiveAvaloniaExtensions
{
    public static void UseRxViewLocator(this IServiceProvider services)
    {
        Locator.CurrentMutable.RegisterLazySingleton(services.GetRequiredService<IViewLocator>);
    }
}
