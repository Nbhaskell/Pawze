namespace Pawze.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class implementingstripe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PawzeUsers", "StripeCustomerId", c => c.String());
            AddColumn("dbo.Subscriptions", "StripeSubscriptionId", c => c.String());
            AlterColumn("dbo.Subscriptions", "BoxId", c => c.Int(nullable: false));
            DropColumn("dbo.PawzeUsers", "StripeId");
            DropColumn("dbo.Subscriptions", "ActiveSubscription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "ActiveSubscription", c => c.Boolean(nullable: false));
            AddColumn("dbo.PawzeUsers", "StripeId", c => c.String());
            AlterColumn("dbo.Subscriptions", "BoxId", c => c.String());
            DropColumn("dbo.Subscriptions", "StripeSubscriptionId");
            DropColumn("dbo.PawzeUsers", "StripeCustomerId");
        }
    }
}
