using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Services.NavigationService;
using System.Windows;

namespace CryptoViewer.ViewModels;

public partial class MainViewModel : ObservableRecipient, IRecipient<PageChangeMessage>
{
    private readonly INavigationService _navigationService;
    public IPageViewModel CurrentPage => _navigationService.CurrentPage;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        IsActive = true;
    }

    [RelayCommand]
    private void NavigateTo(Type vmType)
    {
        if (!typeof(IPageViewModel).IsAssignableFrom(vmType))
            throw new ArgumentException($"Passed type variable isn't assignable to {nameof(IPageViewModel)} interface");

        var mi = typeof(INavigationService).GetMethod(nameof(NavigateTo));
        var metRef = mi!.MakeGenericMethod(vmType);
        metRef.Invoke(_navigationService, null);
    }

    public void Receive(PageChangeMessage message)
    {
        OnPropertyChanged(nameof(CurrentPage));
    }
}
