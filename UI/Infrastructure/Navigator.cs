using System;
using System.Windows;
using UI.ViewModels;

namespace UI.Infrastructure
{
    public static class Navigator
    {
        private static readonly MainWindow mainWindow;
        private static CycleModalView cycleModal;

        static Navigator()
        {
            mainWindow = new MainWindow();
            mainWindow.Closed +=ShutdownApplication;
        }

        private static void ShutdownApplication(object sender, EventArgs eventArgs)
        {
            Application.Current.Shutdown();
        }

        public static void OpenStartWindow()
        {
            var model = new AppViewModel();
            model.BindEvent();
            mainWindow.DataContext = model;
            mainWindow.Show();
        }

        public static void OpenCycleModal(CycleSelectionViewModel modal)
        {
            cycleModal = new CycleModalView
            {
                Owner = mainWindow,
                DataContext = modal
            };
            cycleModal.ShowDialog();
        }

        public static void CloseCycleModal()
        {
            cycleModal.Close();
            cycleModal = null;
        }
    }
}