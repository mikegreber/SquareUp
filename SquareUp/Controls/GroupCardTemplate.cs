using CommunityToolkit.Maui.Markup;
using SquareUp.Model;
using SquareUp.View;

namespace SquareUp.Controls;

public partial class GroupCardTemplate : DataTemplate
{
    public delegate Task OnTapDelegate(GroupClient group);

    public GroupCardTemplate(OnTapDelegate? onTap = null) : base(() =>
        new GroupCard(onTap).Bind(GroupCard.GroupProperty))
    {
    }

    public class GroupCard : ContentView
    {
        public static readonly BindableProperty GroupProperty = BindableProperty.Create(
            nameof(Group),
            typeof(GroupClient),
            typeof(GroupCard),
            defaultBindingMode: BindingMode.OneWay
        );

        public GroupClient Group
        {
            get => (GroupClient)GetValue(GroupProperty);
            set => SetValue(GroupProperty, value);
        }

        public GroupCard(OnTapDelegate? onTap = null)
        {
            
            Content = new Frame
            {
                BindingContext = this,
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = new Command(() => onTap?.Invoke(Group))
                    }
                },

                HeightRequest = 60,
                Margin = 10,
                Content = new Label().Text("Text").Height(30).Bind(Label.TextProperty, "Group.Name")
            };
        }

        

        private void OnTap()
        {
            Shell.Current.GoToAsync(nameof(GroupPage),
                new Dictionary<string, object> { [nameof(Group.Id)] = Group.Id });
        }
    }
}