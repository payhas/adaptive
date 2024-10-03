using Avalonia.Controls;
using Payhas.Adaptive.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Payhas.Adaptive.Views;

public partial class AppWindow : Window, IViewFor<AppViewModel>, IViewFor, IActivatableView
{
    public AppWindow()
    {
        this.WhenActivated(disposable =>
        {
            if (DataContext is IActivatableViewModel activatableViewModel)
            {
                activatableViewModel.Activator
                    .Activate()
                    .DisposeWith(disposable);
            }
        });

        InitializeComponent();
    }

    public AppViewModel? ViewModel
    {
        get => DataContext as AppViewModel;
        set => DataContext = value;
    }

    object? IViewFor.ViewModel
    {
        get => DataContext;
        set => DataContext = value;
    }
}