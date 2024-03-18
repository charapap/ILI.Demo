using ILI.Demo.Command;
using ILI.Demo.Model;
using ILI.Demo.Services;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ILI.Demo.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private ObservableCollection<Pushpin> _soldierPushpins;
        public ObservableCollection<Pushpin> SoldierPushpins
        {
            get { return _soldierPushpins; }
            set
            {
                _soldierPushpins = value;
                OnPropertyChanged(nameof(SoldierPushpins));
            }
        }

        private bool _isPushpinInfoShown;
        public bool IsPushpinInfoShown
        {
            get { return _isPushpinInfoShown; }
            set
            {
                _isPushpinInfoShown = value;
                OnPropertyChanged(nameof(IsPushpinInfoShown));
            }
        }

        private MapSoldierViewModel _selectedSoldier;
        public MapSoldierViewModel SelectedSoldier
        {
            get { return _selectedSoldier; }
            set
            {
                _selectedSoldier = value;
                OnPropertyChanged(nameof(SelectedSoldier));
            }
        }

        public ICommand MapClickCommand { get; }

        private bool _pushpinSelected;
        private IEmulationService _emulationService;

        public MapViewModel(IEmulationService emulationService)
        {
            SoldierPushpins = new ObservableCollection<Pushpin>();

            _emulationService = emulationService;
            _emulationService.LocationUpdated += EmulationService_LocationUpdated;
            _emulationService.StartEmulation();

            MapClickCommand = new RelayCommand(MapClickedCommand, CanExecuteMapClickedCommand);
        }

        private void MapClickedCommand(object args)
        {
            if (!_pushpinSelected)
            {
                IsPushpinInfoShown = false;
            }
            _pushpinSelected = false;
        }

        private bool CanExecuteMapClickedCommand(object parameter)
        {
            return true;
        }

        private void EmulationService_LocationUpdated(Model.Soldier obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                RefreshSoldierOnMap(obj);
            });
        }
        
        private void CreateSoldierPushpin(Soldier soldier)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Template = Application.Current.Resources["CustomPushpinTemplate"] as ControlTemplate;
            pushpin.DataContext = soldier;
            pushpin.Tag = soldier.Id;
            MapLayer.SetPosition(pushpin, new Microsoft.Maps.MapControl.WPF.Location(soldier.Location.Latitude, soldier.Location.Longitude));
            pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(soldier.Location.Latitude, soldier.Location.Longitude);
            SoldierPushpins.Add(pushpin);
            pushpin.MouseLeftButtonDown += Pushpin_MouseLeftButtonUp;
        }

        private void Pushpin_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Pushpin pushpin)
            {
                _pushpinSelected = true;
                IsPushpinInfoShown = true;
                Soldier soldier = _emulationService.GetSoldier(((Soldier)pushpin.DataContext).Id);
                SelectedSoldier = new MapSoldierViewModel() { Soldier = soldier };
            }
        }

        private void RefreshSoldierOnMap(Soldier soldier)
        {
            var pushpin = GetPushpinForSoldier(soldier);
            if (pushpin != null)
            {
                MapLayer.SetPosition(pushpin, new Microsoft.Maps.MapControl.WPF.Location(soldier.Location.Latitude, soldier.Location.Longitude));
                pushpin.Location = new Microsoft.Maps.MapControl.WPF.Location(soldier.Location.Latitude, soldier.Location.Longitude);
            }
            else
            {
                CreateSoldierPushpin(soldier);
            }
        }

        private Pushpin GetPushpinForSoldier(Soldier soldier)
        {
            return SoldierPushpins.FirstOrDefault(x => (int)x.Tag == soldier.Id);
        }
    }
}
