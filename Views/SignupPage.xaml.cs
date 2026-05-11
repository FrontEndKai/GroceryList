using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetRequiredService<SignupViewModel>();
    }
}
