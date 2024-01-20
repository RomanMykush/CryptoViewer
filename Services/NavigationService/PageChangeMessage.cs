using CommunityToolkit.Mvvm.Messaging.Messages;
using CryptoViewer.ViewModels;

namespace CryptoViewer.Services.NavigationService;

public class PageChangeMessage : ValueChangedMessage<IPageViewModel>
{
    public PageChangeMessage(IPageViewModel value) : base(value) { }
}
