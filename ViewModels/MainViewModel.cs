using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Services.NavigationService;
using System.Windows;

namespace CryptoViewer.ViewModels;

public partial class MainViewModel : ObservableRecipient, IRecipient<PageChangeMessage>
{
    private readonly NavigationService _navigationService;
    public IPageViewModel CurrentPage => _navigationService.CurrentPage;

    public MainViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;

        IsActive = true;
    }

    public void Receive(PageChangeMessage message)
    {
        OnPropertyChanged(nameof(CurrentPage));
    }
}
