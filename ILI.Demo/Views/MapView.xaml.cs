using System.Windows.Controls;

namespace ILI.Demo.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        System.Timers.Timer _timer = new System.Timers.Timer(1000);

        public MapView()
        {
            InitializeComponent();
            _timer.Enabled = true;
            _timer.Start();
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                MyMap.ZoomLevel = MyMap.ZoomLevel + 0.1;
                MyMap.ZoomLevel = MyMap.ZoomLevel - 0.1;
            });

        }
    }
}
