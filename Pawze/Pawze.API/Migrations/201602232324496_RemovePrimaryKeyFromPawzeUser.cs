namespace Pawze.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePrimaryKeyFromPawzeUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "PawzeUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PawzeUserId", c => c.String());
        }
    }
}
