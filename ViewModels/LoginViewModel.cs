using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
        LoginCommand = new Command(async () => await LoginAsync());
        GoToSignupCommand = new Command(async () => await Shell.Current.GoToAsync("signup"));
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (SetProperty(ref _errorMessage, value))
            {
                OnPropertyChanged(nameof(HasError));
            }
        }
    }

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public Command LoginCommand { get; }
    public Command GoToSignupCommand { get; }

    private async Task LoginAsync()
    {
        if (_authService.Login(Email, Password, out var message))
        {
            ErrorMessage = string.Empty;
            await Shell.Current.GoToAsync("//main");
            return;
        }

        ErrorMessage = message;
    }
}
