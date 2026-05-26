using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SmartGroceryList.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class GroceryListsViewModel : BaseViewModel
    {
        private readonly IGroceryListService _service;

        public ObservableCollection<GroceryList> Lists { get; } = new();

        [ObservableProperty] private string? newListName;
        [ObservableProperty] private int listsCount;

        public GroceryListsViewModel(IGroceryListService service)
        {
            Title = "My Lists";
            _service = service;
        }

        [RelayCommand]
        public async Task ReloadAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Lists.Clear();
                foreach (var list in await _service.GetListsAsync())
                    Lists.Add(list);
                ListsCount = Lists.Count;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddListAsync()
        {
            if (string.IsNullOrWhiteSpace(NewListName))
            {
                await Shell.Current.DisplayAlert("Validation", "Enter a name for the list.", "OK");
                return;
            }

            await _service.AddListAsync(new GroceryList { Name = NewListName!.Trim() });
            NewListName = string.Empty;
            await ReloadAsync();
        }

        [RelayCommand]
        private async Task DeleteListAsync(GroceryList list)
        {
            if (list == null) return;

            var ok = await Shell.Current.DisplayAlert(
                "Delete list",
                $"Remove '{list.Name}' and its items?",
                "Delete",
                "Cancel");

            if (!ok) return;

            await _service.DeleteListAsync(list);
            await ReloadAsync();
        }

        [RelayCommand]
        private Task OpenListAsync(GroceryList list)
        {
            if (list == null) return Task.CompletedTask;
            return Shell.Current.GoToAsync($"{nameof(GroceryListDetailPage)}?listId={list.Id}");
        }
    }
}
