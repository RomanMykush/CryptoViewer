using CommunityToolkit.Mvvm.Messaging.Messages;
using CryptoViewer.ViewModels;

namespace CryptoViewer.Stores.NavigationStore;

public class PageChangeMessage : ValueChangedMessage<IPageViewModel?>
{
    public PageChangeMessage(IPageViewModel? value) : base(value) { }
}
