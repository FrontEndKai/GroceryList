using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

public partial class AddProductPage : ContentPage
{
    public AddProductPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetRequiredService<AddProductViewModel>();
    }
}
