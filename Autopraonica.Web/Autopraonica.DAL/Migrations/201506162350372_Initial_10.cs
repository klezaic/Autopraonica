namespace Autopraonica.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false));
        }
    }
}
