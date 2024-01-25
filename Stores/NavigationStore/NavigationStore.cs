using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Services.NavigationService;
using CryptoViewer.ViewModels;

namespace CryptoViewer.Stores.NavigationStore;

public class NavigationStore
{
    public IPageViewModel? _currentPage;
    public IPageViewModel? CurrentPage
    {
        get => _currentPage;
        set
        {
            if (_currentPage != null)
                _currentPage.IsActive = false;

            _currentPage = value;

            if (_currentPage != null)
                _currentPage.IsActive = true;

            WeakReferenceMessenger.Default.Send(new PageChangeMessage(_currentPage));
        }
    }
}
