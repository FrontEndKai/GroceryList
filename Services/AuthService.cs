namespace GroceryMate.Services;

public class AuthService
{
    private readonly Dictionary<string, string> _users = new(StringComparer.OrdinalIgnoreCase)
    {
        { "demo@grocerymate.com", "password123" }
    };

    public bool Login(string email, string password, out string message)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            message = "Email and password are required.";
            return false;
        }

        if (_users.TryGetValue(email.Trim(), out var stored) && stored == password)
        {
            message = string.Empty;
            return true;
        }

        message = "Invalid login. Try demo@grocerymate.com / password123.";
        return false;
    }

    public bool SignUp(string email, string password, string confirm, out string message)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            message = "Email and password are required.";
            return false;
        }

        if (password.Length < 6)
        {
            message = "Password must be at least 6 characters.";
            return false;
        }

        if (!string.Equals(password, confirm, StringComparison.Ordinal))
        {
            message = "Passwords do not match.";
            return false;
        }

        if (_users.ContainsKey(email.Trim()))
        {
            message = "Email is already registered.";
            return false;
        }

        _users[email.Trim()] = password;
        message = string.Empty;
        return true;
    }
}
