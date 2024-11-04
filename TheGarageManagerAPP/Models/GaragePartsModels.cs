using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGarageManagerAPP.Models
{
    public class GaragePartsModels
    {
        public int PartID { get; set; }
        public int GarageID { get; set; }
        public string PartName { get; set; }
        public int PartNumber { get; set; }
        public int Cost { get; set; }
        public string ImageURL { get; set; }

    }
}
