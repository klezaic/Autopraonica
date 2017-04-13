namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Autopraonica.Model;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Autopraonica.DAL.AutopraonicaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Autopraonica.DAL.AutopraonicaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Cities', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Clients', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Companies', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('CompanyContacts', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Reservations', RESEED, 0)");

            var CityList = new List<City>
            {
                new City { Name="Zagreb", PostalCode="10000", DateCreated = DateTime.Now},
                new City { Name="Split", PostalCode="21000", DateCreated = DateTime.Now},
                new City { Name="Rijeka", PostalCode="51000", DateCreated = DateTime.Now},
                new City { Name="Karlovac", PostalCode="47000", DateCreated = DateTime.Now},
            };
            foreach(City c in CityList)
            {
                context.Cities.AddOrUpdate(c);
            }

            var ContactList = new List<CompanyContact>
            {
                new CompanyContact(){ContactType=ContactType.Mobitel, Value="091 555 4646", DateCreated = DateTime.Now},
                new CompanyContact(){ContactType=ContactType.Mobitel, Value="099 555 1111", DateCreated = DateTime.Now}
            };

            Company newCompany = new Company()
            {
                Address = "Zagrebačka 1",
                City = CityList[0],
                CityID = CityList[0].ID,
                Name = "Poslovnica alfa",
                DateCreated = DateTime.Now,
                DateFrom = DateTime.Now,
                Contacts = ContactList,         
            };

            var ClientList = new List<Client>
            {
                new Client(){ Name="Ivica", Surname="Ivanović", Email="ivo@ivic.hr", DateCreated = DateTime.Now},
                new Client(){ Name="Marko", Surname="Marković", Email="pero@peric.hr", DateCreated = DateTime.Now}
            };

            var reservationList = new List<Reservation>
            {
                new Reservation { Length=4, ClientID=ClientList[0].ID, Client=ClientList[0], 
                    Company=newCompany, VehicleType=VehicleType.Kombi, DateCreated=DateTime.Now, CompanyID=newCompany.ID },
                new Reservation { Length=2, ClientID=ClientList[1].ID, Client=ClientList[1], 
                    Company=newCompany, VehicleType=VehicleType.Motocikl, DateCreated=DateTime.Now, CompanyID=newCompany.ID }
            };

            newCompany.Reservations = reservationList;
            context.Companies.AddOrUpdate(newCompany);

            foreach(Reservation r in reservationList)
            {
                context.Reservations.AddOrUpdate(r);
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
           
        }
    }
}
