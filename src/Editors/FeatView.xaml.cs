using AssetManager.Enums;
using System;
using System.Windows;
using AssetManager.Extensions;

namespace AssetManager.Editors
{
    /// <summary>
    /// Interaction logic for FeatView.xaml
    /// </summary>
    public partial class FeatView : Window
    {
        public FeatView(FeatViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();

            sourceBox.Items.Add("Select Source");
            foreach (Source item in Enum.GetValues(typeof(Source)))
            {
                sourceBox.Items.Add(item.GetEnumDescription());
            }

            sourceBox.SelectedIndex = vm.FeatSource.Length > 0 ? sourceBox.Items.IndexOf(vm.FeatSource) : 0;
        }
    }
}
