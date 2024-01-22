namespace CryptoViewer.Services.ReferenceCurrencyProvider;

public interface IReferenceCurrencyProvider
{
    Task<IEnumerable<string>> GetReferenceCurrencies();
}
