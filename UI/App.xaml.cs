using System.Windows;
using UI.ViewModels;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var appModel = new AppViewModel();
            mainWindow.DataContext = appModel;
            mainWindow.Show();
        }
    }
}
