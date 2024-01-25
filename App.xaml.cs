using CryptoViewer.Services.CoinProvider;
using CryptoViewer.Services.CoinService;
using CryptoViewer.Services.NavigationService;
using CryptoViewer.Services.ReferenceCurrencyProvider;
using CryptoViewer.Stores.CoinStore;
using CryptoViewer.Stores.NavigationStore;
using CryptoViewer.Stores.ReferenceCurrencyStore;
using CryptoViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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

            .ConfigureServices((builder, services) =>
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.Configure<ApplicationOptions>(
                    builder.Configuration.GetSection(nameof(ApplicationOptions)));

                services.AddSingleton<Func<Type, object?, IPageViewModel>>(s => (vmType, state) =>
                {
                    var page = (IPageViewModel)s.GetRequiredService(vmType);

                    if (page is CoinDetailsViewModel vm && state is string id)
                        vm.CoinId = id;

                    return page;
                });

                services.AddScoped(s =>
                {
                    var httpClient = new HttpClient(new SocketsHttpHandler()
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(1)
                    });

                    var options = s.GetRequiredService<IOptions<ApplicationOptions>>();
                    var userAgent = options.Value.UserAgent;
                    if (!string.IsNullOrEmpty(userAgent))
                        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                    return httpClient;
                });

                services.AddSingleton<NavigationStore>();
                services.AddScoped<INavigationService, NavigationService>();
                services.AddSingleton<IReferenceCurrencyProvider, ReferenceCurrencyProvider>();
                services.AddSingleton<ReferenceCurrencyStore>();
                services.AddSingleton<ICoinProvider, CoinProvider>();
                services.AddSingleton<CoinStore>();
                services.AddScoped<ICoinService, CoinService>();

                services.AddSingleton<MainViewModel>();
                services.AddScoped<CoinDetailsViewModel>();
                services.AddTransient<HomeViewModel>();
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
