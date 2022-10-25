using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;

namespace SquareUp.Controls;

public class CategoryGridItemTemplate : DataTemplate
{
    public CategoryGridItemTemplate() :
        base(() => new CategoryGridItem().Bind(CategoryGridItem.CategoryProperty))
    { }
}

public partial class CategoryGridItem : ContentView
{
    public static readonly BindableProperty CategoryProperty = BindableProperty.Create(
        propertyName: nameof(Category),
        returnType: typeof(string),
        declaringType: typeof(CategoryGridItem)
    );

    public CategoryGridItem()
    {
        Content = new VerticalStackLayout
            {
                new Label()
                        .Bind<Label, string, string>(convert: s => s != null ? s.Split('*').FirstOrDefault() : string.Empty)
                        .TextCenterHorizontal()
                        .Font(size: 18)
                        .Margin(0, 0)
                        .Padding(0)
                        .Start()
                        .FillHorizontal(),

                    new Label()
                        .Bind<Label, string, string>(convert: s => s != null ? s.Split('*').LastOrDefault() : string.Empty)
                        .TextCenterHorizontal()
                        .Font(size: 10)
                        .Margin(0, 0)
                        .Padding(0)
                        .Start()
                        .FillHorizontal()
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
            }
            .Height(50)
            .Margins(top: 8);
    }

    public string Category
    {
        get => (string)GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    }
}