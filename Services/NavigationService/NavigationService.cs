using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Stores.NavigationStore;
using CryptoViewer.ViewModels;
using System.Windows;

namespace CryptoViewer.Services.NavigationService;

public class NavigationService : INavigationService
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<Type, object?, IPageViewModel> _viewModelFactory;

    public NavigationService(NavigationStore navigationStore, Func<Type, object?, IPageViewModel> viewModelFactory)
    {
        _navigationStore = navigationStore;
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<T>(object? state) where T : IPageViewModel
    {
        _navigationStore.CurrentPage = _viewModelFactory.Invoke(typeof(T), state);
    }
}
