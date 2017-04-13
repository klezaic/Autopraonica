namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "VehicleType", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "ContactType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "ContactType", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "VehicleType");
        }
    }
}
