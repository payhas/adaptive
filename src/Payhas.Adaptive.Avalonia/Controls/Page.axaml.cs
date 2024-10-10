using Avalonia;
using Avalonia.Controls;

namespace Payhas.Adaptive.Controls;

public class Page : ContentControl
{
    protected override Type StyleKeyOverride => typeof(Page);

    /// <summary>
    /// Defines the <see cref="ToolBar"/> property.
    /// </summary>
    public static readonly StyledProperty<object?> ToolBarProperty =
        AvaloniaProperty.Register<Page, object?>(nameof(ToolBar));

    /// <summary>
    /// Gets or sets the ToolBar to display.
    /// </summary>
    public object? ToolBar
    {
        get { return GetValue(ToolBarProperty); }
        set { SetValue(ToolBarProperty, value); }
    }

    /// <summary>
    /// ToolBarPadding StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<Thickness> ToolBarPaddingProperty =
        AvaloniaProperty.Register<Page, Thickness>(nameof(ToolBarPadding), new Thickness(0));

    /// <summary>
    /// Gets or sets the ToolBarPadding property.
    /// </summary>
    public Thickness ToolBarPadding
    {
        get => this.GetValue(ToolBarPaddingProperty);
        set => SetValue(ToolBarPaddingProperty, value);
    }

    /// <summary>
    /// ToolBarHeight StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<int> ToolBarHeightProperty =
        AvaloniaProperty.Register<Page, int>(nameof(ToolBarHeight));

    /// <summary>
    /// Gets or sets the ToolBarHeight property.
    /// </summary>
    public int ToolBarHeight
    {
        get => this.GetValue(ToolBarHeightProperty);
        set => SetValue(ToolBarHeightProperty, value);
    }

}
