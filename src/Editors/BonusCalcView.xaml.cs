using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HavenheimManager.Editors;

/// <summary>
///     Interaction logic for BonusCalcView.xaml
/// </summary>
public partial class BonusCalcView : Window
{
    public BonusCalcView(BonusCalcViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    private void DeselectRow(object sender, MouseButtonEventArgs e)
    {
        if (sender is DataGrid grid) grid.UnselectAll();
    }
}