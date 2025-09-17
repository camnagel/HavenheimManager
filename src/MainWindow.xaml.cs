using System;
using System.Windows;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();

        // Initialize Mode
        foreach (AppMode item in Enum.GetValues(typeof(AppMode)))
        {
            ModeBox.Items.Add(item.GetEnumDescription());
        }

        ModeBox.SelectedIndex = vm.AppMode.Length > 0 ? ModeBox.Items.IndexOf(vm.AppMode) : 0;
    }
}