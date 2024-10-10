using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive;
using System.Windows.Input;

namespace Payhas.Adaptive.Controls;

public interface IAction
{
    Uri? IconSource { get; }

    string? Label { get; }

    ICommand? OnClickCommand { get; }
}

public partial class Action : ReactiveObject, IAction
{
    [Reactive]
    private Uri? _iconSource;

    [Reactive]
    private string? _label;

    [Reactive]
    private ICommand? _onClickCommand;
}

public abstract partial class ActionBase : Action
{
    [Reactive]
    private Uri? _iconSource;

    [Reactive]
    private string? _label;

    protected abstract Task OnClick();

    private ReactiveCommand<Unit, Unit>? _onClickCommand;
    public new ICommand? OnClickCommand =>
        _onClickCommand ??= ReactiveCommand.CreateFromTask(OnClick);
}
