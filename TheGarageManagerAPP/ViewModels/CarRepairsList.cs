using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerAPP.Models;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP.ViewModels
{
   
    [QueryProperty("TheVehicle", "TheVehicle")]
    public class CarRepairsList:ViewModelBase
    {
        private VehicleModels theVehicle;
        public VehicleModels TheVehicle
        {
            get { return theVehicle; }
            set
            {
                theVehicle = value;
                CarRepairs = new ObservableCollection<CarRepairModels>(theVehicle.CarRepairs ?? new List<CarRepairModels>());
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CarRepairModels> carRepairs;
        public ObservableCollection<CarRepairModels> CarRepairs
        {
            get => carRepairs;
            set
            {
                carRepairs = value;
                OnPropertyChanged(nameof(CarRepairs));
            }
        }

        
        private IServiceProvider serviceProvider;
        public CarRepairsList(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            NewrepairCommand = new Command(OnNewRepair);
        }

        public Command NewrepairCommand { get; set; }
        private async void OnNewRepair()
        {
            
            if (TheVehicle != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    { "TheVehicle", TheVehicle}
                };
                //Navigate to the task details page
                await Shell.Current.GoToAsync("CarRepair", navParam);
                

            }
        }

        public override void Refresh()
        {
            base.Refresh();
            if (TheVehicle != null)
            {
                CarRepairs = new ObservableCollection<CarRepairModels>(TheVehicle.CarRepairs ?? new List<CarRepairModels>());
            }
            else
            {
                CarRepairs = new ObservableCollection<CarRepairModels>();
            }
        }
    }
}
