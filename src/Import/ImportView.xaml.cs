using System;
using System.Windows;

namespace HavenheimManager.Import;

/// <summary>
///     Interaction logic for ImportView.xaml
/// </summary>
public partial class ImportView : Window
{
    public ImportView(ImportViewModel vm)
    {
        DataContext = vm;

        InitializeComponent();

        typeBox.Items.Add("Select Source Type");
        foreach (object? item in Enum.GetValues(typeof(SourceType)))
        {
            typeBox.Items.Add(item);
        }
    }
}