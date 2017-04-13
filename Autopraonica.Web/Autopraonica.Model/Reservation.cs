using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopraonica.Model
{
    public class Reservation : EntityBase
    {
        public VehicleType VehicleType { get; set; }

        [ForeignKey("Company")]
        public int? CompanyID { get; set; }

        public virtual Company Company { get; set; }

        public virtual int Length { get; set; }

        [ForeignKey("Client")]
        public int? ClientID { get; set; }

        public virtual Client Client { get; set; }

    }

    public enum VehicleType { Motocikl = 1, Automobil = 2, Kombi = 3, Kamion = 4 }
}
