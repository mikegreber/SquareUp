using System.Windows.Input;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;
using Application = Microsoft.Maui.Controls.Application;
using MauiView = Microsoft.Maui.Controls.View;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
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

public abstract partial class BaseContentPage<T> : BaseContentPage where T : BaseViewModel
{
    private enum Row
    {
        First
    }

    private enum Column
    {
        Back,
        Title,
        Action
    }

    protected BaseContentPage(in T viewModel, in bool shouldUseSafeArea = false) : base(shouldUseSafeArea)
    {

        base.BindingContext = viewModel;

        this.DynamicResource(BackgroundColorProperty, nameof(ThemeBase.PageBackgroundColor));
    }

#if WINDOWS
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Shell.SetTitleColor(this,Colors.OrangeRed);

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
            IsVisible = false,
        });
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        Shell.SetTitleView(this, new Grid
            {
                BackgroundColor = BackgroundColor,
                WidthRequest = width,

                HeightRequest = 40,
                Padding = new Thickness(16,0),

                ColumnDefinitions = GridRowsColumns.Columns.Define(
                    (Column.Back, GridLength.Star),
                    (Column.Title, GridLength.Star),
                    (Column.Action, GridLength.Star)
                ),

                RowDefinitions = GridRowsColumns.Rows.Define((Row.First, 40)),

                Children =
                {
                    new Label()
                        .Bind(source: BackButton)
                        .BindTapGesture(nameof(BindingContext.BackCommand))
                        .Start()
                        .CenterVertical()
                        .Column(Column.Back)
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor))
                        .BindingContext(BindingContext),

                    new Label()
                        .Font(size:16, bold: true)
                        .CenterVertical()
                        .CenterHorizontal()
                        .Bind(nameof(Title))
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor))
                        .Column(Column.Back, Column.Action),

                    new Image()
                        .End()
                        .CenterVertical()
                        .Bind(Image.SourceProperty, source: AppBarActionButtonIconSource)
                        .BindTapGesture(nameof(AppBarActionCommand))
                        .Column(Column.Action)
                        .BindingContext(this)
                }
            }
            .BindingContext(this));
    }
#endif

    protected string BackButton { get; set; } = string.Empty;

    protected string AppBarActionButtonIconSource { get; set; } = string.Empty;

    public IRelayCommand AppBarActionCommand { get; set; } = new RelayCommand(()=>{});

#if MACCATALYST || IOS || ANDROID
    private int ActionButtonSize = 96;
#else
    private int ActionButtonSize = 56;
#endif


    protected new MauiView Content
    {
        get => base.Content;
        set
        {

#if !WINDOWS
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
            {
                IsVisible = true,
                TextOverride = BackButton
            });

            if (!string.IsNullOrEmpty(AppBarActionButtonIconSource) && AppBarActionCommand != null)
            {
                ToolbarItems.Add(new ToolbarItem("Action", AppBarActionButtonIconSource, () => AppBarActionCommand.Execute(null)));
            }
#endif

            base.Content = new AbsoluteLayout
                {
                    value
                        .LayoutBounds(0, 0, 1, 1)
                        .LayoutFlags(AbsoluteLayoutFlags.All)
                        .BindingContext(BindingContext),
                    new ImageButton
                        {
                            BackgroundColor = Colors.DeepPink,
                            CornerRadius = ActionButtonSize>>1,
                            Shadow = new Shadow
                                { Offset = new Point(0, 10), Brush = new SolidColorBrush(Color.FromRgba(0, 0, 0, 50)) }
                        }
                        .Source("add.png")
                        .Size(ActionButtonSize)
                        .Padding(ActionButtonSize/3.5)
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