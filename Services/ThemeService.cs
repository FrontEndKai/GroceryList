using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SmartGroceryList.Services;

public interface IThemeService
{
    void SetLightTheme();
    void SetDarkTheme();
    void ToggleTheme();
}

public class ThemeService : IThemeService
{
    public void SetLightTheme()
    {
        Application.Current.UserAppTheme = AppTheme.Light;
    }

    public void SetDarkTheme()
    {
        Application.Current.UserAppTheme = AppTheme.Dark;
    }

    public void ToggleTheme()
    {
        var current = Application.Current.UserAppTheme;
        if (current == AppTheme.Dark)
            SetLightTheme();
        else
            SetDarkTheme();
    }
}
