using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoViewer.ViewModels;

public class CoinDetailsViewModel : ObservableRecipient, IPageViewModel
{
    public required string CoinId { get; set; }
}
