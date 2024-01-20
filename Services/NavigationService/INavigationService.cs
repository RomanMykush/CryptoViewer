using CryptoViewer.ViewModels;

namespace CryptoViewer.Services.NavigationService;

public interface INavigationService
{
    IPageViewModel CurrentPage { get; }
    void NavigateTo<T>() where T : IPageViewModel;
}
