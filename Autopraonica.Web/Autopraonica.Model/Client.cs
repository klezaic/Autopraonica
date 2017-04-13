using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopraonica.Model
{
    public class Client : EntityBase
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Surname { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
