using Avalonia;
using Avalonia.Controls.Primitives;

namespace Payhas.Adaptive.Controls;

public class ToolBar : TemplatedControl
{
    protected override Type StyleKeyOverride => typeof(ToolBar);

    /// <summary>
    /// Title StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<ToolBar, string?>(nameof(Title));

    /// <summary>
    /// Gets or sets the Title property.
    /// </summary>
    public string? Title
    {
        get => this.GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// NavBar StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<object?> NavBarProperty =
        AvaloniaProperty.Register<ToolBar, object?>(nameof(NavBar));

    /// <summary>
    /// Gets or sets the NavBar property.
    /// </summary>
    public object? NavBar
    {
        get => this.GetValue(NavBarProperty);
        set => SetValue(NavBarProperty, value);
    }

    /// <summary>
    /// ActionBar StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<object?> ActionBarProperty =
        AvaloniaProperty.Register<ToolBar, object?>(nameof(ActionBar));

    /// <summary>
    /// Gets or sets the ActionBar property.
    /// </summary>
    public object? ActionBar
    {
        get => this.GetValue(ActionBarProperty);
        set => SetValue(ActionBarProperty, value);
    }
}
