using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using Microsoft.Extensions.DependencyInjection;
using Payhas.Adaptive.Avalonia.Sample.Services;
using Payhas.Adaptive.Avalonia.Sample.Views;
using Payhas.Adaptive.ViewModels;
using Payhas.Adaptive.Views;
using System;

namespace Payhas.Adaptive.Avalonia.Sample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Register all the services needed for the application to run
            var collection = new ServiceCollection();
            collection
                .AddAdaptive()
                .AddAdaptiveAvalonia()
                .AddAdaptiveViewModelActivationContributor<AppViewModel, AppViewModelActivationContributor>();

            // Creates a ServiceProvider containing services from the provided IServiceCollection
            var services = collection.BuildServiceProvider();
            services.UseRxViewLocator();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new AppWindow
                {
                    Title = "Avalonia Sample",
                    Width = 1024, Height = 728,
                    Icon = new WindowIcon(new Bitmap(AssetLoader.Open(
                        new Uri("avares://Payhas.Adaptive.Avalonia.Sample/Assets/avalonia-logo.ico")))),
                    RequestedThemeVariant = ThemeVariant.Light,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    DataContext = services.GetRequiredService<AppViewModel>(),
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new AppMainView
                {
                    DataContext = services.GetRequiredService<AppMainViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}