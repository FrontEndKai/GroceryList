using System.Globalization;

namespace GroceryMate.Helpers;

public static class CurrencyFormatter
{
    public const string DisplayFormat = "₱{0:N2}";

    private static readonly CultureInfo PhilippineCulture = CultureInfo.GetCultureInfo("en-PH");

    public static string Format(decimal amount) =>
        amount.ToString("C", PhilippineCulture);
}
