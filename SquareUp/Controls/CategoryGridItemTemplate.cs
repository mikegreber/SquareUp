using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

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

    public static double ItemHeight => _height + _margin.VerticalThickness;
    private static double _height = 50;
    private static Thickness _margin = new Thickness(0, 8);

    private enum Row { First, Second }

    public CategoryGridItem()
    {
        Content = new Grid()
        {
            RowDefinitions = Rows.Define((Row.First, 30), (Row.Second, 20)),

            Children =
                {
                    new Label() {
                        VerticalOptions = LayoutOptions.End }
                        .Bind<Label, string, string>(convert: s => s != null ? s.Split('*').FirstOrDefault() : string.Empty)
                        .TextCenterHorizontal()
                        .Font(size: 18)
                        .Row(Row.First)
                        .FillHorizontal(),

                    new Label() { VerticalOptions = LayoutOptions.Start }
                        .Bind<Label, string, string>(convert: s => s != null ? s.Split('*').LastOrDefault() : string.Empty)
                        .TextCenterHorizontal()
                        .Font(size: 10)
                        .FillHorizontal()
                        .Row(Row.Second)
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
                }
        }
        .Height(_height)
        .Margin(_margin);


        //Content = new VerticalStackLayout
        //    {
                
        //        Children =
        //        {
        //            new Label() { BackgroundColor = Colors.Red }  
        //                .Bind<Label, string, string>(convert: s => s != null ? s.Split('*').FirstOrDefault() : string.Empty)
        //                .TextCenterHorizontal()
        //                .Font(size: 18)
        //                .Margins(top: 4)
        //                .Padding(0)
        //                .Start()
        //                .FillHorizontal(),

        //            new Label() { BackgroundColor = Colors.Pink }
        //                .Bind<Label, string, string>(convert: s => s != null ? s.Split('*').LastOrDefault() : string.Empty)
        //                .TextCenterHorizontal()
        //                .Font(size: 10)
        //                .Margin(0, 0)
        //                .Padding(0)
        //                .Start()
        //                .FillHorizontal()
        //                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
        //        }
        //    }
        //    .Height(_height)
        //    .Margin(_margin);
    }

    public string Category
    {
        get => (string)GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    }
}