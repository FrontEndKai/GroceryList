using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

public partial class GroceryListPage : ContentPage
{
    public GroceryListPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetRequiredService<GroceryListViewModel>();
    }
}
