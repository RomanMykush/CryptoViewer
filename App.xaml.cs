using CryptoViewer.Dtos;
using CryptoViewer.Services.CoinProvider;
using CryptoViewer.Services.NavigationService;
using CryptoViewer.Stores;
using CryptoViewer.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
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
                services.AddSingleton<Func<Type, IPageViewModel>>(s => vmType => (IPageViewModel)s.GetRequiredService(vmType));

                string? userAgent = hostBuilder.Configuration.GetValue<string>("UserAgent");
                services.AddScoped(s =>
                {
                    var httpClient = new HttpClient(new SocketsHttpHandler()
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(1)
                    });
                    if (userAgent != null)
                        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                    return httpClient;
                });

                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ICoinProvider, CoinProvider>();

                services.AddSingleton<MainViewModel>();
                services.AddScoped<HomeViewModel>();
                services.AddScoped<SettingsViewModel>();

                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
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
