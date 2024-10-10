using Payhas.Adaptive.Services;
using ReactiveUI;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionAdaptiveAvaloniaExtensions
{
    public static IServiceCollection AddAdaptiveAvalonia(this IServiceCollection collection)
    {
        return collection
            .AddSingleton<IViewLocator, AppViewLocator>();
    }
}
