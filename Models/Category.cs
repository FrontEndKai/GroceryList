using SQLite;

namespace SmartGroceryList.Models
{
    [Table("Category")] 
    [Microsoft.Maui.Controls.Internals.Preserve(AllMembers = true)]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
