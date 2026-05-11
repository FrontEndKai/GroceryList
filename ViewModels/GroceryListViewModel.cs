using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GroceryMate.Models;
using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class GroceryListViewModel : BaseViewModel
{
    private readonly GroceryListService _groceryListService;
    private decimal _totalCost;

    public GroceryListViewModel(GroceryListService groceryListService)
    {
        _groceryListService = groceryListService;
        Items = groceryListService.Items;
        ToggleCommand = new Command<GroceryItem>(Toggle);
        IncreaseCommand = new Command<GroceryItem>(Increase);
        DecreaseCommand = new Command<GroceryItem>(Decrease);
        RemoveCommand = new Command<GroceryItem>(Remove);

        Items.CollectionChanged += OnItemsChanged;
        RefreshTotal();
    }

    public ObservableCollection<GroceryItem> Items { get; }

    public decimal TotalCost
    {
        get => _totalCost;
        set => SetProperty(ref _totalCost, value);
    }

    public Command<GroceryItem> ToggleCommand { get; }
    public Command<GroceryItem> IncreaseCommand { get; }
    public Command<GroceryItem> DecreaseCommand { get; }
    public Command<GroceryItem> RemoveCommand { get; }

    private void OnItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RefreshTotal();
    }

    private void Toggle(GroceryItem? item)
    {
        if (item is null)
        {
            return;
        }

        _groceryListService.Toggle(item);
        RefreshTotal();
    }

    private void Increase(GroceryItem? item)
    {
        if (item is null)
        {
            return;
        }

        _groceryListService.Increase(item);
        RefreshTotal();
    }

    private void Decrease(GroceryItem? item)
    {
        if (item is null)
        {
            return;
        }

        _groceryListService.Decrease(item);
        RefreshTotal();
    }

    private void Remove(GroceryItem? item)
    {
        if (item is null)
        {
            return;
        }

        _groceryListService.Remove(item);
        RefreshTotal();
    }

    private void RefreshTotal()
    {
        TotalCost = Items.Sum(item => item.Total);
    }
}
