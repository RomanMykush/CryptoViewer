using System.ComponentModel;

namespace CryptoViewer.ViewModels;

public interface IPageViewModel : INotifyPropertyChanged
{
    bool IsActive { get; set; }
}
