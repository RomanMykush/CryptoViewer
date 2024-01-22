using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CryptoViewer.Stores.ReferenceCurrencyStore;

public class CurrentRefCurrencyChangedMessage : ValueChangedMessage<string>
{
    public CurrentRefCurrencyChangedMessage(string value) : base(value) { }
}
