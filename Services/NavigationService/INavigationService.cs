using CryptoViewer.ViewModels;

namespace CryptoViewer.Services.NavigationService;

public interface INavigationService
{
    void NavigateTo<T>(object? state = null) where T : IPageViewModel;
}
