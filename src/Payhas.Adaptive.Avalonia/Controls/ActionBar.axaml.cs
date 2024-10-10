using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.LogicalTree;
using Avalonia.Metadata;

namespace Payhas.Adaptive.Controls;

public class ActionBar : TemplatedControl
{
    private EventHandler<ChildIndexChangedEventArgs>? _childIndexChanged;

    /// <summary>
    /// Actions StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<IList<IAction>> ActionsProperty =
        AvaloniaProperty.Register<ActionBar, IList<IAction>>(nameof(Actions), []);

    /// <summary>
    /// Gets or sets the Actions property.
    /// </summary>
    [Content]
    public IList<IAction> Actions
    {
        get => this.GetValue(ActionsProperty);
        set
        {
            SetValue(ActionsProperty, value);
            _childIndexChanged?.Invoke(this, ChildIndexChangedEventArgs.ChildIndexesReset);
        }
    }

    //event EventHandler<ChildIndexChangedEventArgs>? IChildIndexProvider.ChildIndexChanged
    //{
    //    add => _childIndexChanged += value;
    //    remove => _childIndexChanged -= value;
    //}

    public int GetChildIndex(ILogical child)
    {
        throw new Exception(child?.ToString());

        return child is IAction action
            ? Actions.IndexOf(action)
            : -1;
    }

    public bool TryGetTotalCount(out int count)
    {
        count = Actions.Count;
        return true;
    }
}
