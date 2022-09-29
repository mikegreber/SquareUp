using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SquareUp.Controls;


public class Loader : ContentView
{
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
        propertyName: nameof(IndicatorColor),
		returnType:typeof(Color),
		defaultValue: Colors.Red,
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
        defaultValue: "Default Text",
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
        
		Content = new VerticalStackLayout
		{
			BindingContext = this,
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}.Bind(Label.TextProperty, nameof(Text)),
				new ActivityIndicator
                {
					IsRunning = true,
					Color = IndicatorColor,
					
					// BindingContext = new Binding(nameof(IndicatorColor), BindingMode.TwoWay, source: nameof(Loader.IndicatorColor)),
                }.Bind(ActivityIndicator.ColorProperty, nameof(IndicatorColor))
			}
		};
	}
}