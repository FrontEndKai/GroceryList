using SQLite;

namespace SmartGroceryList.Models
{
    public class GroceryListItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GroceryListId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsPurchased { get; set; }
    }
}
