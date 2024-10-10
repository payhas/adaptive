using Avalonia;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using System.Collections;

namespace Payhas.Adaptive.Controls;

public class AssetSourceScope : AvaloniaObject
{
    /// <summary>
    /// BaseUri AttachedProperty definition.
    /// </summary>
    public static readonly AttachedProperty<Uri?> BaseUriProperty =
        AvaloniaProperty.RegisterAttached<AssetSourceScope, StyledElement, Uri?>("BaseUri");

    /// <summary>
    /// Accessor for Attached property <see cref="BaseUriProperty"/>.
    /// </summary>
    /// <param name="element">Target element</param>
    /// <param name="value">The value to set  <see cref="BaseUriProperty"/>.</param>
    public static void SetBaseUri(StyledElement element, Uri? value)
    {
        element.SetValue(BaseUriProperty, value);

        if (element is IHandleBaseUriChange handleBaseUriChange)
        {
            handleBaseUriChange.OnBaseUriChange();
        }
    }

    /// <summary>
    /// Accessor for Attached property <see cref="BaseUriProperty"/>.
    /// </summary>
    /// <param name="element">Target element</param>
    public static Uri? GetBaseUri(StyledElement element)
    {
        return GetBaseUri(element.GetSelfAndLogicalAncestors());
    }

    protected static Uri? GetBaseUri(IEnumerable? list)
    {
        if (list != null)
        {
            foreach (var item in list)
            {
                if (item is StyledElement styled)
                {
                    var baseUri = styled.GetValue(BaseUriProperty);
                    if (baseUri != null)
                    {
                        return baseUri;
                    }
                }
            }
        }

        return null;
    }
}

public interface IHandleBaseUriChange
{
    void OnBaseUriChange();
}
