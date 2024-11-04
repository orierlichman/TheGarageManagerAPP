using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGarageManagerAPP.Models
{
    public class UserModels
    {
        public int UserID { get; set; }
        public string? Email { get; set; }
        public string? UserPassword { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public int UserGarageID { get; set; }
        public int? UserStatusID { get; set; }

    }
}
