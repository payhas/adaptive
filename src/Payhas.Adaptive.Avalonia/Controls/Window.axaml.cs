using Avalonia;

namespace Payhas.Adaptive.Controls;

public class Window : Avalonia.Controls.Window
{
    protected override Type StyleKeyOverride => typeof(Window);

    /// <summary>
    /// ToolBar StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<ToolBar?> ToolBarProperty =
        AvaloniaProperty.Register<Window, ToolBar?>(nameof(ToolBar));

    /// <summary>
    /// Gets or sets the ToolBar property.
    /// </summary>
    public ToolBar? ToolBar
    {
        get => this.GetValue(ToolBarProperty);
        set => SetValue(ToolBarProperty, value);
    }
}
