using System;
using System.Windows;
using System.Windows.Controls;

namespace AssetManager.Import
{
    /// <summary>
    /// Interaction logic for ImportView.xaml
    /// </summary>
    public partial class ImportView : Window
    {
        public ImportView(ImportViewModel vm)
        {
            this.DataContext = vm;

            InitializeComponent();

            typeBox.Items.Add("Select Source Type");
            foreach (var item in Enum.GetValues(typeof(SourceType)))
            {
                typeBox.Items.Add(item);
            }
        }
    }
}
