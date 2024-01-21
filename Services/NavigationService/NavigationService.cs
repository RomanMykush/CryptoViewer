using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.ViewModels;
using System.Windows;

namespace CryptoViewer.Services.NavigationService;

public class NavigationService : INavigationService
{
    public IPageViewModel _currentPage;
    public IPageViewModel CurrentPage
    {
        get => _currentPage;
        private set
        {
            if (_currentPage != null)
                _currentPage.IsActive = false;

            _currentPage = value;
            _currentPage.IsActive = true;

            WeakReferenceMessenger.Default.Send(new PageChangeMessage(_currentPage));
        }
    }

    private readonly Func<Type, IPageViewModel> _viewModelFactory;

    public NavigationService(Func<Type, IPageViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
        _currentPage = _viewModelFactory.Invoke(typeof(HomeViewModel));
    }

    public void NavigateTo<T>() where T : IPageViewModel
    {
        CurrentPage = _viewModelFactory.Invoke(typeof(T));
    }
}
