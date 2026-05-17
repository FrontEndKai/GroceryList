using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Views;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        public LoginViewModel()
        {
            Title = "Login";
        }

        [RelayCommand]
        async Task Login()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // TODO: Add authentication logic here
                // For now, we'll just simulate a delay and navigate to the main app page
                await Task.Delay(2000);

                // Navigate to the main application page after successful login
                await Shell.Current.GoToAsync($"//{nameof(ProductsPage)}");
            }
            catch (System.Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToSignup()
        {
            await Shell.Current.GoToAsync(nameof(SignupPage));
        }
    }
}
