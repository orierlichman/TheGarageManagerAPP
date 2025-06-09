using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerApp.Services;

namespace TheGarageManagerAPP.Models
{
    public class VehicleModels
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public int YearVehicle { get; set; }
        public string FuelType { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public int CurrentMileage { get; set; }
        public string ImageURL { get; set; }

        public List<CarRepairModels>? CarRepairs { get; set; }

        public string ImageDefaultURL
        {
            get
            {
                return TheGarageManagerWebAPIProxy.ImageBaseAddress + $"/carImages/{Manufacturer}.jpg";
            }
        }
    }

    
}


