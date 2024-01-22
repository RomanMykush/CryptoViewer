using CommunityToolkit.Mvvm.Messaging.Messages;
using CryptoViewer.Models;

namespace CryptoViewer.Stores.CoinStore;

public class CoinUpdatedMessage : ValueChangedMessage<Coin>
{
    public CoinUpdatedMessage(Coin value) : base(value) { }
}
