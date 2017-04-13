namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Companies", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.CompanyContacts", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Reservations", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Clients", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CompanyContacts", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Companies", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cities", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
