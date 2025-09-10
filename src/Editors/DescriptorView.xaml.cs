using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HavenheimManager.Editors;

/// <summary>
///     Interaction logic for DescriptorView.xaml
/// </summary>
public partial class DescriptorView : Window
{
    public DescriptorView(DescriptorViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    private void DeselectRow(object sender, MouseButtonEventArgs e)
    {
        if (sender is DataGrid grid)
        {
            grid.UnselectAll();
        }
    }
}