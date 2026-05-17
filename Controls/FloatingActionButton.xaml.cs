using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace SmartGroceryList.Controls;

public partial class FloatingActionButton : ContentView
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(FloatingActionButton));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public FloatingActionButton()
    {
        InitializeComponent();
    }

    private void OnClicked(object sender, EventArgs e)
    {
        if (Command?.CanExecute(null) ?? false)
            Command.Execute(null);
    }
}
