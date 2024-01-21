using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.ViewModels;

namespace CryptoViewer.Services.NavigationService;

public class NavigationService : INavigationService
{
    public IPageViewModel _currentPage;
    public IPageViewModel CurrentPage
    {
        get => _currentPage;
        private set
        {
            _currentPage = value;
            WeakReferenceMessenger.Default.Send(new PageChangeMessage(_currentPage));
        }
    }

    private readonly Func<Type, IPageViewModel> _viewModelFactory;

    public NavigationService(Func<Type, IPageViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<T>() where T : IPageViewModel
    {
        CurrentPage = _viewModelFactory.Invoke(typeof(T));
    }
}
