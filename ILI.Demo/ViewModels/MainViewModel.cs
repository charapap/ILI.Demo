namespace ILI.Demo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MapViewModel MapViewModel { get; }

        public MainViewModel(MapViewModel mapViewModel)
        {
            MapViewModel = mapViewModel;
        }
    }
}
