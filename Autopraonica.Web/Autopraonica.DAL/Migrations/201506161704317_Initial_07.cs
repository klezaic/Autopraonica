namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "DateFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "Length", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "VrijemeOd");
            DropColumn("dbo.Reservations", "Trajanje");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Trajanje", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "VrijemeOd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservations", "Length");
            DropColumn("dbo.Reservations", "DateFrom");
        }
    }
}
