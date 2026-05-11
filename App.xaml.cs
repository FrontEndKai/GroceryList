using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate;

public partial class App : Application
{
	private readonly AppShell _appShell;

	public App(AppShell appShell)
	{
		_appShell = appShell;
		InitializeComponent();
	}

	public static IServiceProvider Services { get; set; } = null!;

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(_appShell);
	}
}