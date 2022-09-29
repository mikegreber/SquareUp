using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using SquareUp.ViewModel;

namespace SquareUp.View;

public abstract class BaseContentPage : ContentPage
{
    protected BaseContentPage(in bool shouldUseSafeArea = false)
    {
        On<iOS>()
            .SetUseSafeArea(shouldUseSafeArea)
            .SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}

public abstract class BaseContentPage<T> : BaseContentPage where T : BaseViewModel
{
    protected BaseContentPage(in T viewModel, in bool shouldUseSafeArea = false) : base(shouldUseSafeArea)
    {
        base.BindingContext = viewModel;
        BuildView();
    }

    private void BuildView()
    {
        // if (BindingContext is IQueryAttributable)
        //     BindingContext.OnAttributesSet = Build;
        // else 
            Build();
    }

    protected abstract void Build();

    protected new T BindingContext => (T)base.BindingContext;
}