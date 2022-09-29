using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Model;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class AddPersonPage : BaseContentPage<AddPersonViewModel>
{
    public AddPersonPage(AddPersonViewModel viewModel) : base(viewModel)
    {
		
	}

    protected override void Build()
    {
        Content = new VerticalStackLayout
        {
            Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Add Person"
                },

                new Label().Text("Name"),
                new Entry()
                    .Bind(Entry.TextProperty, nameof(BindingContext.Name)),

                new Button()
                    .Text("Add")
                    .BindCommand(nameof(BindingContext.AddPersonCommand)),
            }
        };
    }
}