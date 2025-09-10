using System;
using System.Windows;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Editors
{
    /// <summary>
    /// Interaction logic for TraitView.xaml
    /// </summary>
    public partial class TraitView : Window
    {
        public TraitView(TraitViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();

            sourceBox.Items.Add("Select Source");
            foreach (Source item in Enum.GetValues(typeof(Source)))
            {
                sourceBox.Items.Add(item.GetEnumDescription());
            }

            sourceBox.SelectedIndex = vm.TraitSource.Length > 0 ? sourceBox.Items.IndexOf(vm.TraitSource) : 0;
        }
    }
}
