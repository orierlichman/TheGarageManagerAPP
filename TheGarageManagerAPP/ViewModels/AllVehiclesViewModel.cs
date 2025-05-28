using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TasksManagementApp.Services;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Models;

namespace TheGarageManagerAPP.ViewModels
{
    public class AllVehiclesViewModel : ViewModelBase
    {

        private string _searchText;

        private TheGarageManagerWebAPIProxy proxy;

        private IServiceProvider serviceProvider;

        private ObservableCollection<VehicleModels> garageVehicle;
        public ObservableCollection<VehicleModels> GarageVehicle
        {
            get => garageVehicle;
            set
            {
                garageVehicle = value;
                OnPropertyChanged(nameof(GarageVehicle));
            }
        }

        public ICommand SearchTextChangedCommand { get; }
     


        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchTextChangedCommand.Execute(null);
            }
        }


        public AllVehiclesViewModel(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllVehicles();
            SearchTextChangedCommand = new Command(async () => await SearchVehicles());
            

        }

        private VehicleModels selectedVehicle;
        public VehicleModels SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
                CarSelected(value);
            }
        }
        private async void CarSelected(VehicleModels v)
        {
            if (v != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    { "TheVehicle", v }
                };
                //Navigate to the task details page
                await Shell.Current.GoToAsync("CarRepairList", navParam);
                SelectedVehicle = null;
                
            }

        }

        public async void FillAllVehicles()
        {
            UserModels u = ((App)Application.Current).LoggedInUser;
            List<VehicleModels>? vehicle = await proxy.GetGarageVehicles();
            if (vehicle == null || vehicle.Count == 0)
            {
                GarageVehicle = new ObservableCollection<VehicleModels>();
                return;
            }
            GarageVehicle = new ObservableCollection<VehicleModels>(vehicle);
            allVehicles = vehicle;
        }

        private List<VehicleModels> allVehicles = new List<VehicleModels>();


        private async Task SearchVehicles()
        {
            var vehicles = allVehicles.Where(v=>v.LicensePlate.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            if (vehicles != null)
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    GarageVehicle = new ObservableCollection<VehicleModels>(allVehicles);
                }
                else
                {
                    GarageVehicle = new ObservableCollection<VehicleModels>(vehicles);
                }
            }
        }

    }
}
