using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GroceryMate.Services;

public static class UserFeedbackService
{
    public static Task ShowSuccessAsync(string message) =>
        Toast.Make(message, ToastDuration.Short, 14).Show();
}
