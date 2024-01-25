using CryptoViewer.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoViewer.Views;

/// <summary>
/// Interaction logic for HomeView.xaml
/// </summary>
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var row = sender as DataGridRow;
        var index = row!.GetIndex();

        var veiwModel = (HomeViewModel)DataContext;
        veiwModel?.RowSelectedCommand.Execute(index);
    }

    private void ComboBox_KeyUp(object sender, KeyEventArgs e)
    {
        var textBox = sender as TextBox;
        if (e.Key == Key.Enter)
            textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
    }
}
