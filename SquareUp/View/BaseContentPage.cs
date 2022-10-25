using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;
using MauiView = Microsoft.Maui.Controls.View;
using VisualElement = Microsoft.Maui.Controls.VisualElement;

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

        this.DynamicResource(BackgroundColorProperty, nameof(ThemeBase.PageBackgroundColor));

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior{IsVisible = false});
    }

    protected MauiView ActionView { get; set; } = new Label();
    protected MauiView TitleView { get; set; } = new Label();
    protected MauiView BackView { get; set; } = new Label();
    protected bool ShowAppBar { get; set; } = true;

    protected new MauiView Content
    {
        get => base.Content;
        set
        {
            BackView
                .CenterVertical()
                .Start()
                .BindTapGesture(nameof(BindingContext.BackCommand))
                .BindingContext(BindingContext);
            TitleView
                .CenterVertical()
                .CenterHorizontal();
            ActionView
                .CenterVertical()
                .End();
            
            InnerContent = value;
            if (ShowAppBar) InnerContent.Margin += new Thickness(0, 56, 0, 0);
            InnerContent
                .LayoutBounds(0, 0, 1, 1)
                .LayoutFlags(AbsoluteLayoutFlags.All)
                .BindingContext(BindingContext);
            
            base.Content = new AbsoluteLayout
                {
                    new AppBar(BackView, TitleView, ActionView) { IsVisible = ShowAppBar }
                        .Bind(AppBar.TitleViewProperty, nameof(TitleView))
                        .Bind(AppBar.BackViewProperty, nameof(BackView))
                        .Bind(AppBar.ActionViewProperty, nameof(ActionView))
                        .LayoutBounds(0, 0, 1, 56).LayoutFlags(AbsoluteLayoutFlags.WidthProportional)
                        .BindingContext(this),
                    InnerContent,
                    new ImageButton
                        {
                            BackgroundColor = Colors.DeepPink,
                            CornerRadius = 28,
                            Shadow = new Shadow
                                { Offset = new Point(0, 10), Brush = new SolidColorBrush(Color.FromRgba(0, 0, 0, 50)) }
                        }
                        .Source("add.png")
                        .Size(56)
                        .Padding(16)
                        .Margin(16)
                        .CenterVertical()
                        .CenterHorizontal()
                        .Bind<ImageButton, Func<Task>, bool>(IsVisibleProperty, nameof(BindingContext.OnActionButtonClicked), convert: func => func != null)
                        .BindCommand(nameof(BindingContext.ActionButtonCommand))
                        .BindingContext(BindingContext)
                        .LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
                        .LayoutBounds(1, 1),
                    new Loader()
                        .Bind(IsVisibleProperty, "Session.IsLoading")
                        .BindingContext(BindingContext)
                        .LayoutBounds(0.5,0.5).LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
                }
                .FillHorizontal()
                .FillVertical()
                .BindingContext(this);
        }
    }

    protected MauiView InnerContent { get; set; }

    protected new T BindingContext => (T)base.BindingContext;
}

public abstract class BaseContentView<T> : ContentView where T : BaseViewModel
{
    protected BaseContentView(in T viewModel) { }
    protected new T BindingContext => (T) base.BindingContext;
}

public static class Extensions
{
    public static TBindable BindingContext<TBindable>(this TBindable element, object context) where TBindable : BindableObject
    {
        element.BindingContext = context;
        return element;
    }

    public static TBindable Trigger<TBindable>(this TBindable element, TriggerBase trigger) where TBindable : VisualElement
    {
        element.Triggers.Add(trigger);
        return element;
    }

    public static TBindable StyleToggle<TBindable>(this TBindable element, string path, Style enabledStyle, Style disabledStyle, bool toggleEnabled = true) where TBindable : VisualElement
    {
        var trigger = new DataTrigger(typeof(TBindable))
        {
            BindingContext = element.BindingContext,
            Binding = new Binding(path),
            Value = true,
            EnterActions = { new LerpStyle(disabledStyle, enabledStyle), },
            ExitActions = { new LerpStyle(enabledStyle, disabledStyle), }
        };

        if (toggleEnabled) trigger.Setters.Add(new Setter { Property = VisualElement.IsEnabledProperty, Value = true });

        element.Triggers.Add(trigger);
        return element;
    }
}