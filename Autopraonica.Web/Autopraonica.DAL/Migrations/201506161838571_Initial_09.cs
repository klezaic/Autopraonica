namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_09 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "DateFrom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "DateFrom", c => c.DateTime(nullable: false));
        }
    }
}
