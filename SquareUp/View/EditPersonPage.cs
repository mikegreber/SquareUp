using SquareUp.ViewModel;

namespace SquareUp.View;

public class EditPersonPage : BaseContentPage<MainViewModel>
{
	public EditPersonPage(MainViewModel mainViewModel) : base(mainViewModel)
    {
		
	}

    protected override void Build()
    {
        Content = new VerticalStackLayout
        {
            Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
                }
            }
        };
    }
}