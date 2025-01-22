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

        private GarageFinderServiceProxy proxy;

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


        public AllVehiclesViewModel(GarageFinderServiceProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllVehicles();
            SearchTextChangedCommand = new Command(async () => await SearchVehicles(((App)Application.Current).LoggedInUser.UserGarageID));

        }

        public async void FillAllVehicles()
        {
            UserModels u = ((App)Application.Current).LoggedInUser;
            List<VehicleModels> vehicle = await GetAllVehicles(u.UserGarageID);
            GarageVehicle = new ObservableCollection<VehicleModels>(vehicle);
        }

        public async Task<List<VehicleModels>> GetAllVehicles(int garageID)
        {
            List<VehicleModels> list = /*await*/ this.proxy.GetVehicles(garageID);
            return list;
        }


        private async Task SearchVehicles(int garageID)
        {
            var vehicles = /*await*/ proxy.GetVehicles(garageID);
            if (vehicles != null)
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    GarageVehicle = new ObservableCollection<VehicleModels>(vehicles);
                }
                else
                {
                    GarageVehicle = new ObservableCollection<VehicleModels>(
                        vehicles.FindAll(g => g.LicensePlate.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }

    }
}
