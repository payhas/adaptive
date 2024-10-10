using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Payhas.Adaptive.Avalonia.Sample.ViewModels;
using Payhas.Adaptive.Avalonia.Sample.Views;
using Payhas.Adaptive.Controls;

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
                .AddAdaptiveAvaloniaView<MainViewModel, MainView>(true);

            // Creates a ServiceProvider containing services from the provided IServiceCollection
            var services = collection.BuildServiceProvider();
            services.UseRxViewLocator();

            var mainView = services.GetRequiredService<MainView>();
            mainView.DataContext = services.GetRequiredService<MainViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    //Content = new Page
                    //{
                    //    ToolBar = new ToolBar
                    //    {
                    //        Height = 32,
                    //    },
                        Content = mainView,
                    //},
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = mainView;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
