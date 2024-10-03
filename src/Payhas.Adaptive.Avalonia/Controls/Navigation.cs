using Avalonia;
using Avalonia.Controls;
using Payhas.Adaptive.Navigations;

namespace Payhas.Adaptive.Controls;

public class Navigation : ContentControl
{
    /// <summary>
    /// ManagerStyledProperty definition
    /// </summary>
    public static readonly StyledProperty<INavigationManager> ManagerProperty =
        AvaloniaProperty.Register<Navigation, INavigationManager>(nameof(Manager));

    /// <summary>
    /// Gets or sets the Manager property. This StyledProperty
    /// indicates navigation manager.
    /// </summary>
    public INavigationManager Manager
    {
        get => this.GetValue(ManagerProperty);
        set => SetValue(ManagerProperty, value);
    }
}
