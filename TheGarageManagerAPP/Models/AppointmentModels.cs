    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGarageManagerAPP.Models
{
    public class AppointmentModels
    {
        public int AppointmentID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? GarageID { get; set; }
        public string? LicensePlate { get; set; }
        public int? AppointmentStatusId { get; set; }
        public DateTime? ConfirmDate { get; set; }

    }
}
