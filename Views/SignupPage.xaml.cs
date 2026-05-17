namespace SmartGroceryList.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
	}

    private async void OnLoginTapped(object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
