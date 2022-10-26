using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.View;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace SquareUp.Controls;

public class GroupCardTemplate : DataTemplate
{
    public GroupCardTemplate(IRelayCommand<ObservableGroupInfo>? onTap = null) :
        base(() => new GroupCard().Bind(GroupCard.GroupProperty).Bind(GroupCard.CommandProperty, source: onTap)) { }
}

public partial class GroupCard : ContentView
{
    public static readonly BindableProperty GroupProperty = BindableProperty.Create(
        propertyName: nameof(Group),
        returnType: typeof(ObservableGroupInfo),
        declaringType: typeof(GroupCard),
        defaultBindingMode: BindingMode.OneWay
    );

    public ObservableGroupInfo Group
    {
        get => (ObservableGroupInfo)GetValue(GroupProperty);
        set => SetValue(GroupProperty, value);
    }

    [RelayCommand]
    private static void OpenGroup(ObservableGroupInfo group)
    {
        GroupPage.OpenAsync(group);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        propertyName: nameof(Command),
        returnType: typeof(IRelayCommand<ObservableGroupInfo>),
        declaringType: typeof(GroupCard),
        defaultBindingMode: BindingMode.OneWay
    );

    public IRelayCommand<ObservableGroupInfo> Command
    {
        get => (IRelayCommand<ObservableGroupInfo>)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private enum Column
    {
        First,
        Second
    }

    private enum Row
    {
        First,
        Second
    }

    public GroupCard()
    {
        Content = new Border()
        {
            BackgroundColor = Colors.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = 10 },
            
            Content = new Grid()
                {
                    ColumnDefinitions = Columns.Define(
                        (Column.First, Stars(3)),
                        (Column.Second, Star)
                    ),

                    RowDefinitions = Rows.Define((Row.First, Auto), (Row.Second, 56)),

                    Children =
                    {
                        new Label{ MaxLines = 2, LineBreakMode = LineBreakMode.WordWrap }
                            .Margins(right:40)
                            .Column(Column.First, Column.Second)
                            .Row(Row.First)
                            .Font(bold: true, size: 24)
                            .Padding(0)
                            .Bind(Label.TextProperty, "Group.Name")
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),

                        new HorizontalStackLayout
                            {

                                VerticalOptions = new LayoutOptions(LayoutAlignment.Start, false),
                                Children =
                                {
                                    new Image()
                                        .Margins(top: 1)
                                        .Source("person.png")
                                        .Size(20),

                                    new Label()
                                        .Font(bold: true, size: 20)
                                        .Bind(Label.TextProperty, "Group.Participants")
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),
                                }
                            }
                            .End()
                            .Column(Column.Second)
                            .Row(Row.First),
                        
                        new VerticalStackLayout
                            {
                                VerticalOptions = LayoutOptions.End,
                                Children = {
                                    new Label()
                                        .Text("Last edited:")
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),

                                    new Label()
                                        .Padding(0)
                                        .Font(bold: false, size: 16)
                                        .Bind<Label, DateTime, string>(Label.TextProperty, "Group.LastEdit",
                                            convert: d => $"{d.ToLongDateString()}")
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),

                                    new Label()
                                        .Padding(0)
                                        .Font(bold: false, size: 14)
                                        .Bind<Label, DateTime, string>(Label.TextProperty, "Group.LastEdit",
                                            convert: d => $"{d.ToShortTimeString()}")
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                                }
                            }
                            .Column(Column.First)
                            .Row(Row.Second)
                    }
                }
                .Paddings(20, 16, 20, 20)
                .BindTapGesture(nameof(Command), parameterPath: nameof(Group))
                .Bind<Grid, string, LinearGradientBrush>(BackgroundProperty, "Group.Color", convert: Converters.ConvertBackground)
                .BindingContext(this),

            //     Content = new VerticalStackLayout
            //     {
            //         Children =
            //                 {
            //                     new FlexLayout {
            //                             AlignContent = FlexAlignContent.SpaceBetween,
            //                             AlignItems = FlexAlignItems.Center,
            //                             Direction = FlexDirection.Row,
            //                             Children =
            //                             {
            //                                 new Label()
            //                                     .Font(bold: true, size: 24)
            //                                     .Padding(0)
            //                                     .Bind(Label.TextProperty, "Group.Name")
            //                                     .Grow(1)
            //                                     .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),
            //
            //                                 new Image()
            //                                     .Margins(top:1)
            //                                     .Source("person.png")
            //                                     .Size(15)
            //                                     .CenterVertical(),
            //
            //                                 new Label()
            //                                     .Font(bold: true, size: 16)
            //                                     .Bind(Label.TextProperty, "Group.Participants")
            //                                     .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),
            //                             }
            //                         }
            //                         .Height(30)
            //                         .FillHorizontal(),
            //
            //                     new Label()
            //                         .Text("Last edited:")
            //                         .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
            //
            //                     new Label()
            //                         .Padding(0)
            //                         .Font(bold: false, size: 16)
            //                         .Bind<Label, DateTime, string>(Label.TextProperty, "Group.LastEdit", convert: d => $"{d.ToLongDateString()}")
            //                         .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
            //
            //                     new Label()
            //                         .Padding(0)
            //                         .Font(bold: false, size: 14)
            //                         .Bind<Label, DateTime, string>(Label.TextProperty, "Group.LastEdit", convert: d => $"{d.ToShortTimeString()}")
            //                         .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
            //                 }
            //     }
            // }

        };
    }
}
