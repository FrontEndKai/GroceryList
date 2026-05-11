using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class SignupViewModel : BaseViewModel
{
    private readonly AuthService _authService;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _errorMessage = string.Empty;

    public SignupViewModel(AuthService authService)
    {
        _authService = authService;
        SignUpCommand = new Command(async () => await SignUpAsync());
        GoToLoginCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
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

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
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

    public Command SignUpCommand { get; }
    public Command GoToLoginCommand { get; }

    private async Task SignUpAsync()
    {
        if (_authService.SignUp(Email, Password, ConfirmPassword, out var message))
        {
            ErrorMessage = string.Empty;
            await Shell.Current.GoToAsync("//main");
            return;
        }

        ErrorMessage = message;
    }
}
