using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;

namespace SquareUp.Controls;


public class Loader : ContentView
{
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
        propertyName: nameof(IndicatorColor),
		returnType:typeof(Color),
		defaultValue: ThemeBase.Blue100Accent,
		defaultBindingMode: BindingMode.TwoWay,
		declaringType:typeof(Loader)
    );
    public Color IndicatorColor
    {
		get => (Color)GetValue(IndicatorColorProperty);
		set => SetValue(IndicatorColorProperty, value);
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        defaultValue: "",
        defaultBindingMode: BindingMode.TwoWay,
        declaringType: typeof(Loader)
    );

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Loader()
    {
        BindingContext = this;
        ZIndex = 20;
		Content = new VerticalStackLayout
		{
			BindingContext = this,
			Children = {
				
				new ActivityIndicator
                    {
					    IsRunning = true,
					    Color = IndicatorColor,
                    }
                    .Bind(ActivityIndicator.ColorProperty, nameof(IndicatorColor)),
                new Label() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
                }.Bind(Label.TextProperty, nameof(Text)),
            }
		};
	}
}