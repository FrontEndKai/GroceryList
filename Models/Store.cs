using SQLite;

namespace SmartGroceryList.Models
{
    public class Store
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
