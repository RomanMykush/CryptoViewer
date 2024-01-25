using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Services.NavigationService;
using CryptoViewer.Stores.NavigationStore;

namespace CryptoViewer.ViewModels;

public partial class MainViewModel : ObservableRecipient, IRecipient<PageChangeMessage>
{
    private readonly INavigationService _navigationService;
    private readonly NavigationStore _navigationStore;
    public IPageViewModel? CurrentPage => _navigationStore.CurrentPage;

    public MainViewModel(INavigationService navigationService, NavigationStore navigationStore)
    {
        _navigationService = navigationService;
        _navigationStore = navigationStore;

        IsActive = true;
        _navigationService.NavigateTo<HomeViewModel>();
    }

    [RelayCommand]
    private void NavigateTo(Type vmType)
    {
        if (!typeof(IPageViewModel).IsAssignableFrom(vmType))
            throw new ArgumentException($"Passed type variable isn't assignable to {nameof(IPageViewModel)} interface");

        var mi = typeof(INavigationService).GetMethod(nameof(NavigateTo));
        var metRef = mi!.MakeGenericMethod(vmType);
        object?[] parametersArray = [new object?[] { null }];
        metRef.Invoke(_navigationService, parametersArray);
    }

    public void Receive(PageChangeMessage message)
    {
        OnPropertyChanged(nameof(CurrentPage));
    }
}
