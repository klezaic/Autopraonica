using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopraonica.Model
{
    public class CompanyContact : EntityBase
    {
        [ForeignKey("Company")]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }

        public ContactType ContactType { get; set; }
        public string Value { get; set; }

    }

    public enum ContactType { Telefon = 1, Mobitel = 2, eMail = 3 }
}
