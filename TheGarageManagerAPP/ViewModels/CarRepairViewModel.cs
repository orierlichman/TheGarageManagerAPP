using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerAPP.Models;

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


        #region Description
        private bool showDescriptionError;

        public bool ShowDescriptionError
        {
            get => showDescriptionError;
            set
            {
                showDescriptionError = value;
                OnPropertyChanged("ShowNameError");
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




    }
}
