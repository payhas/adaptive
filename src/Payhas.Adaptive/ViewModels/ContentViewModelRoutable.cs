
namespace Payhas.Adaptive.ViewModels;

public abstract class ContentViewModelRoutable : BaseViewModelRoutable
{
    protected ContentViewModelRoutable(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override PresentationScope PresentationScope => PresentationScope.Content;
}
