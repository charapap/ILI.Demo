using ILI.Demo.Services;
using ILI.Demo.ViewModels;
using System.Windows;

namespace ILI.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IEmulationService _emulationService;

        public App()
        {
            _emulationService = new EmulationService();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(
                new MapViewModel(_emulationService)),
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
