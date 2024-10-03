using Payhas.Adaptive.Navigations;
using Payhas.Adaptive.Services;
using Payhas.Adaptive.ViewModels;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace Payhas.Adaptive.Avalonia.Sample.Services;

public class AppViewModelActivationContributor : ViewModelActivationContributor<AppViewModel>
{
    public override async Task OnActivation(CompositeDisposable disposables, AppViewModel viewModel)
    {
        await viewModel.NavigationManager!
            .Navigate<AppMainViewModel>(NavigationScope.Base);
    }
}
