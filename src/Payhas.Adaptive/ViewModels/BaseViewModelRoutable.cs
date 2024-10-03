using Payhas.Adaptive.Navigations;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace Payhas.Adaptive.ViewModels;

public abstract class BaseViewModelRoutable : BaseViewModel, IRoutableViewModel
{
    protected BaseViewModelRoutable(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        ExceptionHandler = new Interaction<Exception, Unit>();
        CloseCommand = ReactiveCommand.CreateFromTask(Close);
    }

    public virtual string? UrlPathSegment => GetType().Name;

    public virtual PresentationScope PresentationScope => PresentationScope.Base;

    public virtual IScreen? HostScreen { get; set; }

    public virtual INavigationManager? NavigationManager { get; set; }

    public Interaction<Exception, Unit> ExceptionHandler { get; }

    public ReactiveCommand<Unit, IRoutableViewModel?> CloseCommand { get; }

    protected virtual async Task<IRoutableViewModel?> Close()
    {
        return await HostScreen?.Router.NavigateBack.Execute(Unit.Default)
            ?? default;
    }
}
