using Avalonia.Controls;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Payhas.Adaptive.Views;

public class BaseView<TViewModel> : UserControl, IViewFor<TViewModel>, IViewFor, IActivatableView
    where TViewModel : class
{
    public BaseView()
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
    }

    public TViewModel? ViewModel
    {
        get => DataContext as TViewModel;
        set => DataContext = value;
    }

    object? IViewFor.ViewModel
    {
        get => DataContext;
        set => DataContext = value;
    }
}
