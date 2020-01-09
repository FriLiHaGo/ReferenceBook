using System.Windows;

namespace ReferenceBook
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();
            var vm = new MainWindowVM();
            window.DataContext = vm;

            Current.MainWindow = window;
            Current.MainWindow.Show();
        }
    }
}
