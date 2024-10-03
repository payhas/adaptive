using ReactiveUI;

namespace Payhas.Adaptive.Services;

public class AppViewLocator : IViewLocator
{
    public AppViewLocator(
        IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }

    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        var viewType = typeof(IViewFor<>).MakeGenericType(viewModel?.GetType() ?? typeof(T));

        var view = ServiceProvider.GetService(viewType) as IViewFor;
        //if (view != null)
        //{
        //    view.ViewModel = viewModel;
        //}

        return view;
    }
}
