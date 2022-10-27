using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.View;
using SquareUp.ViewModel;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace SquareUp.Controls;

public class ParticipantListItemTemplate : DataTemplate
{
    public ParticipantListItemTemplate(GroupDetailsViewModel viewModel) :
        base(() => new ParticipantListItem(viewModel)
            .Bind(ParticipantListItem.ParticipantProperty)
            .Bind(ParticipantListItem.CommandProperty, source: viewModel.InviteParticipantCommand)
            .Bind(ParticipantListItem.UserIdProperty, source: viewModel.Session.User.Id)
        )
    {
    }
}

public class ParticipantListItem : BaseContentView<GroupDetailsViewModel>
{
    public static readonly BindableProperty ParticipantProperty = BindableProperty.Create(
        nameof(Participant),
        typeof(ObservableParticipant),
        typeof(ParticipantListItem),
        new ObservableParticipant { Name = "DefaultName" }
    );

    public ObservableParticipant Participant
    {
        get => (ObservableParticipant)GetValue(ParticipantProperty);
        set => SetValue(ParticipantProperty, value);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(IRelayCommand<ObservableParticipant>),
        typeof(ParticipantListItem),
        new RelayCommand<ObservableParticipant>(p =>
        {
            Application.Current.MainPage.DisplayAlert("Title", $"Message {p.Name}", "Cancel");
        })
    );

    public IRelayCommand<ObservableParticipant> Command
    {
        get => (IRelayCommand<ObservableParticipant>)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty UserIdProperty = BindableProperty.Create(
        nameof(UserId),
        typeof(int),
        typeof(ParticipantListItem),
        -1
    );

    public int UserId
    {
        get => (int)GetValue(UserIdProperty);
        set => SetValue(UserIdProperty, value);
    }

    public static double ItemHeight = 50;
    
    public ParticipantListItem(GroupDetailsViewModel viewModel) : base(viewModel)
    {
        Content = new Grid
                {
                    ColumnDefinitions = Columns.Define((Column.First, Stars(3)), (Column.Second, Star)),
                    RowDefinitions = Rows.Define((Row.First, ItemHeight)),

                    Children =
                    {
                        new Label()
                            .FillHorizontal()
                            .CenterVertical()
                            .Column(Column.First)
                            .Bind("Participant.Name")
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),

                        new Button()
                            .Text("Invite")
                            .CenterVertical()
                            .FillHorizontal()
                            .BindCommand(nameof(Command), parameterPath: nameof(Participant))
                            .Bind<Button, int, bool>(IsVisibleProperty, "Participant.UserId", convert: i => i == 0)
                            .Bind<Button, int, bool>(IsEnabledProperty, "Participant.UserId", convert: i => i == 0)
                            .DynamicResource(StyleProperty, nameof(ThemeBase.AltButtonStyle))
                            .Column(Column.Second),

                        new Label()
                            .Text("Connected")
                            .TextCenterVertical()
                            .TextCenterHorizontal()
                            .Bind<Label, int, bool>(IsVisibleProperty, "Participant.UserId", convert: i => i != 0 && i != UserId)
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.Blue100Accent))
                            .Column(Column.Second),

                        new Label()
                            .Text("Me")
                            .TextCenterVertical()
                            .TextCenterHorizontal()
                            .Bind<Label, int, bool>(IsVisibleProperty, "Participant.UserId",
                                convert: i => i == UserId)
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.Blue100Accent))
                            .Column(Column.Second),

                        new BoxView() { VerticalOptions = LayoutOptions.End }
                            .Column(Column.First, Column.Second)
                            .Height(1)
                            .FillHorizontal()
                            .DynamicResource(BoxView.ColorProperty, nameof(ThemeBase.DividerColor))
                    }
        }.Padding(20,0).BindingContext(this);
    }

    private enum Column
    {
        First,
        Second
    }

    private enum Row
    {
        First
    }
}