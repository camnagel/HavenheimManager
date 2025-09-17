using System;
using System.Windows;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Editors;

/// <summary>
///     Interaction logic for FeatView.xaml
/// </summary>
public partial class FeatView : Window
{
    public FeatView(FeatViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();

        SourceBox.Items.Add("Select Source");
        foreach (Source item in Enum.GetValues(typeof(Source)))
        {
            SourceBox.Items.Add(item.GetEnumDescription());
        }

        SourceBox.SelectedIndex = vm.FeatSource.Length > 0 ? SourceBox.Items.IndexOf(vm.FeatSource) : 0;
    }
}