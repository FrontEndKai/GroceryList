using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetRequiredService<LoginViewModel>();
    }
}
