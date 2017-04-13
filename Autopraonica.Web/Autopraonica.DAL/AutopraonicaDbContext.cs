using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autopraonica.Model;

namespace Autopraonica.DAL
{
    public class AutopraonicaDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContact> Contacts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities { get; set; }

        public AutopraonicaDbContext()
        {

        }

        public static AutopraonicaDbContext Create()
        {
            return new AutopraonicaDbContext();
        }

    }
}
