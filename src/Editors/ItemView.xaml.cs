using System.Windows;

namespace AssetManager.Editors
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : Window
    {
        public ItemView(ItemViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
