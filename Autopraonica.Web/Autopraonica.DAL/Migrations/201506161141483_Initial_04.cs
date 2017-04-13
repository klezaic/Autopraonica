namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_04 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cities", "DateModified");
            DropColumn("dbo.Companies", "DateModified");
            DropColumn("dbo.CompanyContacts", "DateModified");
            DropColumn("dbo.Reservations", "DateModified");
            DropColumn("dbo.Clients", "DateModified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "DateModified", c => c.DateTime());
            AddColumn("dbo.Reservations", "DateModified", c => c.DateTime());
            AddColumn("dbo.CompanyContacts", "DateModified", c => c.DateTime());
            AddColumn("dbo.Companies", "DateModified", c => c.DateTime());
            AddColumn("dbo.Cities", "DateModified", c => c.DateTime());
        }
    }
}
