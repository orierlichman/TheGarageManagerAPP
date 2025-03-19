using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerAPP.Models;
using TheGarageManagerApp.Services;
using Microsoft.Extensions.DependencyInjection;
//using static Java.Util.Jar.Attributes;

namespace TheGarageManagerAPP.ViewModels
{
    [QueryProperty("TheVehicle", "TheVehicle")]

    public class CarRepairViewModel : ViewModelBase
    {


        private VehicleModels theVehicle;
        public VehicleModels TheVehicle
        {
            get { return theVehicle; }
            set
            {
                theVehicle = value;
                OnPropertyChanged();
            }
        }


        private TheGarageManagerWebAPIProxy proxy;
        private IServiceProvider serviceProvider;


        public CarRepairViewModel(TheGarageManagerWebAPIProxy proxy)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            SaveCommand = new Command(OnSave);
            DescriptionError = "Description is required";
            CostError = "Cost is negative";
            DateError = "the Date is future";

        }



        #region Description
        private bool showDescriptionError;

        public bool ShowDescriptionError
        {
            get => showDescriptionError;
            set
            {
                showDescriptionError = value;
                OnPropertyChanged("ShowDescriptionError");
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                ValidateDescription();
                OnPropertyChanged("Description");
            }
        }

        private string descriptionError;

        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                descriptionError = value;
                OnPropertyChanged("DescriptionError");
            }
        }

        private void ValidateDescription()
        {
            this.ShowDescriptionError = string.IsNullOrEmpty(Description);
        }
        #endregion



        #region Cost
        private bool showCostError;

        public bool ShowCostError
        {
            get => showCostError;
            set
            {
                showCostError = value;
                OnPropertyChanged("ShowCostError");
            }
        }

        private int cost;

        public int Cost
        {
            get => cost;
            set
            {
                cost = value;
                ValidateCost();
                OnPropertyChanged("Cost");
            }
        }

        private string costError;

        public string CostError
        {
            get => costError;
            set
            {
                costError = value;
                OnPropertyChanged("CostError");
            }
        }

        private void ValidateCost()
        {
            bool x;
            if (this.Cost < 0)
            {
                x = true;
            }
            else
            {
                x = false;
            }
            this.ShowCostError = x;
        }
        #endregion


        #region Date
        private bool showDateError;

        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                ValidateDate();
                OnPropertyChanged("Date");
            }
        }

        private string dateError;

        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }

        private void ValidateDate()
        {
            bool x;
            if (this.Date < DateTime.Now)
            {
                x = true;
            }
            else
            {
                x = false;
            }
            this.ShowCostError = x;
        }
        #endregion


        public Command SaveCommand { get; }

        public async void OnSave()
        {
            ValidateDescription();
            ValidateCost();
            ValidateDate();

            if (!ShowDescriptionError && !ShowCostError && !ShowDateError)
            {
                //Create a new CarRepairModels object with the data from the CarRepair form
                var newRepair = new CarRepairModels
                {
                    Cost = Cost,
                    RepairDate = Date,
                    DescriptionCar = Description,
                    GarageID = 103,
                    //LicensePlate =  ofer!!!!!
                };

                //Call the CarRepair method on the proxy to carRepair the new user
                InServerCall = true;
                newRepair = await proxy.AddCarRepairAsync(newRepair);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newRepair != null)
                {
                    InServerCall = false;
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = " failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Description", errorMsg, "ok");
                }
            }
        }




        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (selectedDate != value)
                {
                    bool readFromServer = false;
                    if (selectedDate.Month != value.Month || selectedDate.Year != value.Year)
                    {
                        readFromServer = true;
                    }
                    selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }




    }
}
