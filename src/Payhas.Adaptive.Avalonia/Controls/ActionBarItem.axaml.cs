using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Windows.Input;

namespace Payhas.Adaptive.Controls;

public class ActionBarItem : TemplatedControl, IAction, IHandleBaseUriChange
{
    public ActionBarItem()
    {
        IconSourceProperty.Changed.AddClassHandler<ActionBarItem>((_, __) => OnBaseUriChange());
    }

    /// <summary>
    /// IconSource StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<Uri?> IconSourceProperty =
        AvaloniaProperty.Register<ActionBarItem, Uri?>(nameof(IconSource));

    /// <summary>
    /// Gets or sets the IconSource property.
    /// </summary>
    public Uri? IconSource
    {
        get => this.GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    /// <summary>
    /// Icon StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<ActionBarItem, object?>(nameof(Icon));

    /// <summary>
    /// Gets or sets the Icon property.
    /// </summary>
    public object? Icon
    {
        get => this.GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Label StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<string?> LabelProperty =
        AvaloniaProperty.Register<ActionBarItem, string?>(nameof(Label));

    /// <summary>
    /// Gets or sets the Label property.
    /// </summary>
    public string? Label
    {
        get => this.GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// OnClickCommand StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<ICommand?> OnClickCommandProperty =
        AvaloniaProperty.Register<ActionBarItem, ICommand?>(nameof(OnClickCommand));

    /// <summary>
    /// Gets or sets the OnClickCommand property.
    /// </summary>
    public ICommand? OnClickCommand
    {
        get => this.GetValue(OnClickCommandProperty);
        set => SetValue(OnClickCommandProperty, value);
    }

    public void OnBaseUriChange()
    {
        if (IconSource != null)
        {
            try
            {
                var baseUri = AssetSourceScope.GetBaseUri(this);
                Icon = new Image { Source = new Bitmap(AssetLoader.Open(IconSource, baseUri)) };
            }
            catch (Exception)
            {
                Icon = new Image { Source = new Bitmap(AssetLoader.Open(new Uri("avares://Payhas.Adaptive.Avalonia/Assets/no-asset.ico"))) };
            }
        }
    }
}
