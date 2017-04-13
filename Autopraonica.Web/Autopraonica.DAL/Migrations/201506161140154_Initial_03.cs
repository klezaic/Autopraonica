namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_03 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "Email");
            DropColumn("dbo.Companies", "Latitude");
            DropColumn("dbo.Companies", "Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Companies", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Companies", "Email", c => c.String(nullable: false));
        }
    }
}
