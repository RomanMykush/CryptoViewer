using CryptoViewer.Services.NavigationService;
using CryptoViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CryptoViewer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((hostBuilder, services) =>
            {
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<HomeViewModel>();

                services.AddSingleton<Func<Type, IPageViewModel>>(s => vmType => (IPageViewModel)s.GetRequiredService(vmType));

                services.AddSingleton<INavigationService, NavigationService>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();

        base.OnExit(e);
    }
}
