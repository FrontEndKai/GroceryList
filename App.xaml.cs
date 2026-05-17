using Microsoft.Extensions.DependencyInjection;
using SmartGroceryList.Interfaces;

namespace SmartGroceryList;

public partial class App : Application
{
	private readonly IDatabaseService _databaseService;

	public App(IDatabaseService databaseService)
	{
		InitializeComponent();

		_databaseService = databaseService;

		// Initialize the database in background so UI can appear immediately
		Task.Run(async () =>
		{
			try
			{
				await _databaseService.Init();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Database init error: {ex}");
			}
		});
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}