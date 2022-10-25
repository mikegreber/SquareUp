using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using MauiView = Microsoft.Maui.Controls.View;

namespace SquareUp.Controls;

public class AppBar : ContentView
{
    public static BindableProperty BackViewProperty = BindableProperty.Create(
        propertyName: nameof(BackView),
        returnType: typeof(MauiView),
        declaringType: typeof(AppBar),
        defaultValue: new Label().Text("DefaultBack").Column(Column.Back)
    );
    
    public MauiView BackView
    {
        get => (MauiView)GetValue(BackViewProperty);
        set => SetValue(BackViewProperty, value);
    }
    
    public static BindableProperty TitleViewProperty = BindableProperty.Create(
        propertyName: nameof(TitleView),
        returnType: typeof(MauiView),
        declaringType: typeof(AppBar),
        defaultValue: new Label().Text("DefaultTitle").CenterHorizontal().Column(Column.Title)
    );
    
    public MauiView TitleView
    {
        get => (MauiView)GetValue(TitleViewProperty);
        set => SetValue(TitleViewProperty, value);
    }

    public static BindableProperty ActionViewProperty = BindableProperty.Create(
        propertyName: nameof(ActionView),
        returnType: typeof(MauiView),
        declaringType: typeof(AppBar),
        defaultValue: new Label().Text("DefaultAction").Column(Column.Action)
    );

    public MauiView ActionView
    {
        get => (MauiView)GetValue(ActionViewProperty);
        set => SetValue(ActionViewProperty, value);
    }

    // public static BindableProperty BackCommandProperty = BindableProperty.Create(
    //     propertyName: nameof(BackCommand),
    //     returnType: typeof(IRelayCommand),
    //     declaringType: typeof(AppBar),
    //     defaultValue: new RelayCommand(() => { Shell.Current.GoToAsync(".."); })
    //     );
    //
    // public IRelayCommand BackCommand
    // {
    //     get => (IRelayCommand)GetValue(BackCommandProperty);
    //     set => SetValue(BackCommandProperty, value);
    // }
    //
    // public static BindableProperty ActionCommandProperty = BindableProperty.Create(
    //     propertyName: nameof(ActionCommand),
    //     returnType: typeof(IRelayCommand),
    //     declaringType: typeof(AppBar),
    //     defaultValue: new RelayCommand(() => { })
    // );
    //
    // public IRelayCommand ActionCommand
    // {
    //     get => (IRelayCommand)GetValue(ActionCommandProperty);
    //     set => SetValue(ActionCommandProperty, value);
    // }

    enum Row { First }

    enum Column { Back, Title, Action }
    
    public AppBar(MauiView backView = null, MauiView titleView = null, MauiView actionView = null)
    {
        this.DynamicResource(BackgroundColorProperty, nameof(ThemeBase.PageBackgroundColor));

        if (backView != null) BackView = backView;
        if (titleView != null) TitleView = titleView;
        if (actionView != null) ActionView = actionView;

        ZIndex = 10;
        HorizontalOptions = LayoutOptions.Fill;
        HeightRequest = 56;
        
        Content = new Grid()
        {
            ColumnDefinitions = Columns.Define(
                (Column.Back, GridLength.Star),
                (Column.Title, GridLength.Star),
                (Column.Action, GridLength.Star)
            ),

            RowDefinitions = Rows.Define((Row.First, 56)),

            Children =
            {
                BackView
                    .Column(Column.Back),
                
                TitleView
                    .Column(Column.Title),

                ActionView
                    .Column(Column.Action),
            },
            BindingContext = this,
        }.FillHorizontal().Margin(0).Padding(16,0);
    }
}