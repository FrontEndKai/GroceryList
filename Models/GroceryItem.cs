using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroceryMate.Models;

public class GroceryItem : INotifyPropertyChanged
{
    private int _quantity = 1;
    private bool _isChecked;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Product Product { get; set; } = new Product();

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (_quantity != value)
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
            }
        }
    }

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked != value)
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal Total => Quantity * Product.Price;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
