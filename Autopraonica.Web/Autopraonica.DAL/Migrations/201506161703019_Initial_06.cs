namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "VrijemeOd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "Trajanje", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "Trajanje");
            DropColumn("dbo.Reservations", "VrijemeOd");
        }
    }
}
