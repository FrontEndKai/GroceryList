namespace GroceryMate.Models;

public class PriceOption
{
    public string Store { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Note { get; set; } = string.Empty;
    public bool IsBest { get; set; }
}
