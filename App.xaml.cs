using System;
using System.Threading.Tasks;
using SmartGroceryList.Interfaces;

namespace SmartGroceryList;

public partial class App : Application
{
    private readonly IDatabaseService _databaseService;

    public App(IDatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;

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
    
    protected override void OnStart()
    {
        base.OnStart();
    }
}