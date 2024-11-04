using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGarageManagerAPP.Models
{
    public class CarRepairModels
    {
        public int RepairID { get; set; }
        public string LicensePlate { get; set; }
        public int GarageID { get; set; }
        public DateTime RepairDate { get; set; }
        public string DescriptionCar { get; set; }
        public decimal Cost { get; set; }
    }
}
