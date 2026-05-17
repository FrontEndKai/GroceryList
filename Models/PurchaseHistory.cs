using SQLite;
using System;

namespace SmartGroceryList.Models
{
    public class PurchaseHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}
