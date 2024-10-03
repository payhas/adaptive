using Payhas.Adaptive.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payhas.Adaptive.ViewModels;

public class AppMainViewModel : BaseViewModelRoutable
{
    public AppMainViewModel(
        IServiceProvider serviceProvider,
        INavigationManager navigationManager)
        : base(serviceProvider)
    {
        NavigationManager = navigationManager;
    }

#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia! Updated!";
#pragma warning restore CA1822 // Mark members as static
}
