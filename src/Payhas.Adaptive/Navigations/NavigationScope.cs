namespace Payhas.Adaptive.Navigations;

[Flags]
public enum NavigationScope
{
    None = 0,
    Content = 1 << None,
    Root = 1 << Content,
}
