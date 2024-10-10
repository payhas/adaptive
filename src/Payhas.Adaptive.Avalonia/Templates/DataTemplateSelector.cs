using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Payhas.Adaptive.Templates;

public class DataTemplateSelector : AvaloniaDictionary<Type, IDataTemplate>, IDataTemplate
{
    public Control? Build(object? param)
    {
        var type = param?.GetType();
        return type != null && ContainsKey(type)
            ? this[type].Build(param) ?? param as Control
            : param as Control;
    }

    public bool Match(object? data)
    {
        return data != null
            && ContainsKey(data.GetType());
    }
}
