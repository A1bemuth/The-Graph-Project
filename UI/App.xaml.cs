using System.Windows;
using System.Windows.Threading;
using UI.Infrastructure;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Navigator.OpenStartWindow();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            CommandEventBinder.CathedExcaptionCommand.Execute(e.Exception);
            MessageBox.Show(e.Exception.Message, e.Exception.Source, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
