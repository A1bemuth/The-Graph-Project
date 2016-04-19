using System;
using System.Windows;
using Microsoft.Win32;
using UI.ViewModels;

namespace UI.Infrastructure
{
    public static class Navigator
    {
        private static readonly MainWindow mainWindow;
        private static CycleModalView cycleModal;
        private static PathModalView pathModal;

        static Navigator()
        {
            mainWindow = new MainWindow();
            mainWindow.Closed +=ShutdownApplication;
        }

        private static void ShutdownApplication(object sender, EventArgs eventArgs)
        {
            Application.Current.Shutdown();
        }

        public static string OpenFile()
        {
            var fileDailog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Filter = "Файлы Excel(*.xl;*.xlsx;*.xlsm;*.xlsb;*.xlam;*.xltx;*.xltm;*.xls;*.xla;*.xlt;*.xlm;*.xlw)|" +
                        "*.xl;*.xlsx;*.xlsm;*.xlsb;*.xlam;*.xltx;*.xltm;*.xls;*.xla;*.xlt;*.xlm;*.xlw"
            };
            if (fileDailog.ShowDialog(mainWindow) == true)
            {
                return fileDailog.FileName;
            }
            return null;
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
            cycleModal.Closed += (sender, args) => CommandEventBinder.CloseCyclesModalCommand.Execute();
            cycleModal.ShowDialog();
        }

        public static void CloseCycleModal()
        {
            cycleModal.Close();
            cycleModal = null;
        }

        public static void OpenPathModal(PathSelectionViewModel modal)
        {
            pathModal = new PathModalView
            {
                Owner = mainWindow,
                DataContext = modal
            };
            pathModal.Closed += (sender, args) => CommandEventBinder.ClosePathModalCommand.Execute();
            pathModal.ShowDialog();
        }

        public static void ClosePathModal()
        {
            pathModal.Close();
            pathModal = null;
        }
    }
}