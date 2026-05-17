using SQLite;
using System;

namespace SmartGroceryList.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
