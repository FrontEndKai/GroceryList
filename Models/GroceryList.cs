using SQLite;
using System;
using System.Collections.Generic;

namespace SmartGroceryList.Models
{
    public class GroceryList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Ignore]
        public List<GroceryListItem> Items { get; set; } = new List<GroceryListItem>();
    }
}
