using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopraonica.Model
{
    public class Company : EntityBase
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime DateFrom { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<CompanyContact> Contacts { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
