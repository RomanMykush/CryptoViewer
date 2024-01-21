namespace CryptoViewer.Models;

public interface IExpirable
{
    DateTime ExpirationDate { get; }
}
