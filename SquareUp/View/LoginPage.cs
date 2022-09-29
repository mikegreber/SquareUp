using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquareUp.ViewModel;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
namespace SquareUp.View
{
    public class LoginPage : BaseContentPage<LoginViewModel>
    {
        public LoginPage(LoginViewModel viewModel) : base(viewModel)
        {
            
        }

        protected override void Build()
        {
            

            Content = new StackLayout
            {
                BindingContext = BindingContext,
                Margin=30,
                Children = {
                    new Label().Text("Login").Font(size:36),
                    new Label { Text = "Email" }.Text("Email"),
                    new Entry{}.Bind(Entry.TextProperty, "LoginRequest.Email"),
                    new Label { Text = "Password" },
                    new Entry{}.Bind(Entry.TextProperty, "LoginRequest.Password"),
                    new Button().Text("Login").BindCommand(nameof(BindingContext.LoginCommand)),

                    new Label().Text("Register").Font(size:36),
                    new Label { Text = "Name" },
                    new Entry{}.Bind(Entry.TextProperty, "RegisterRequest.Name"),
                    new Label { Text = "Email" }.Text("Email"),
                    new Entry{}.Bind(Entry.TextProperty, "RegisterRequest.Email"),
                    new Label { Text = "Password" },
                    new Entry{}.Bind(Entry.TextProperty, "RegisterRequest.Password"),
                    new Label { Text = "ConfirmPassword"},
                    new Entry{}.Bind(Entry.TextProperty, "RegisterRequest.ConfirmPassword"),
                    new BoxView().Size(10, 10),
                    new Button().Text("Register").BindCommand(nameof(BindingContext.RegisterCommand)),

                    new Label().Bind(Label.TextProperty, "Message"),
                }
            };
        }
    }
}