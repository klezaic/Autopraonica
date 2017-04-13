using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopraonica.Model
{
    public abstract class EntityBase
    {
        [Key]
        public int ID { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }
    }
}
