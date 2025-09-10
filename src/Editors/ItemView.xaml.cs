using System.Windows;

namespace HavenheimManager.Editors
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
