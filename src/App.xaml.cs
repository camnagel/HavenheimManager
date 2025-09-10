using System.Windows;

namespace HavenheimManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.MainWindow = new MainWindow(new MainWindowViewModel());
            Current.MainWindow.Show();
        }
    }
}
