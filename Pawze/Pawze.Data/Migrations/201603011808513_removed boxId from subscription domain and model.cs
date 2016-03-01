namespace Pawze.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedboxIdfromsubscriptiondomainandmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subscriptions", "BoxId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "BoxId", c => c.Int(nullable: false));
        }
    }
}
