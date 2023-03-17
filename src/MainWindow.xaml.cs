using System.Windows;
using System.Windows.Input;

namespace AssetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
            //CustomExpander.DataContext = vm.CustomTraitFilterList;
        }
    }
}
