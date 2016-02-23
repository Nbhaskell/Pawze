namespace Pawze.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boxes",
                c => new
                    {
                        BoxId = c.Int(nullable: false, identity: true),
                        SubscriptionId = c.Int(),
                        PawzeUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BoxId)
                .ForeignKey("dbo.PawzeUsers", t => t.PawzeUserId, cascadeDelete: true)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionId)
                .Index(t => t.SubscriptionId)
                .Index(t => t.PawzeUserId);
            
            CreateTable(
                "dbo.BoxItems",
                c => new
                    {
                        BoxItemId = c.Int(nullable: false, identity: true),
                        BoxId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        BoxItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BoxItemId)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .ForeignKey("dbo.Boxes", t => t.BoxId, cascadeDelete: true)
                .Index(t => t.BoxId)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        QuantityOnHand = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId);
            
            CreateTable(
                "dbo.PawzeUsers",
                c => new
                    {
                        PawzeUserId = c.String(nullable: false, maxLength: 128),
                        StripeId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Address4 = c.String(),
                        Address5 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostCode = c.String(),
                        International = c.Boolean(),
                        Telephone = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.PawzeUserId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        PawzeUserId = c.String(nullable: false, maxLength: 128),
                        BoxId = c.String(),
                        ActiveSubscription = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriptionId)
                .ForeignKey("dbo.PawzeUsers", t => t.PawzeUserId)
                .Index(t => t.PawzeUserId);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        ShipmentId = c.Int(nullable: false, identity: true),
                        Tracking = c.String(),
                        PawzeUserId = c.Int(nullable: false),
                        ShipmentDate = c.DateTime(nullable: false),
                        Subscription_SubscriptionId = c.Int(),
                    })
                .PrimaryKey(t => t.ShipmentId)
                .ForeignKey("dbo.Subscriptions", t => t.Subscription_SubscriptionId)
                .Index(t => t.Subscription_SubscriptionId);
            
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        ConfigurationId = c.Int(nullable: false, identity: true),
                        CurrentBoxItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ConfigurationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "PawzeUserId", "dbo.PawzeUsers");
            DropForeignKey("dbo.Shipments", "Subscription_SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.Boxes", "SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.Boxes", "PawzeUserId", "dbo.PawzeUsers");
            DropForeignKey("dbo.BoxItems", "BoxId", "dbo.Boxes");
            DropForeignKey("dbo.BoxItems", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Shipments", new[] { "Subscription_SubscriptionId" });
            DropIndex("dbo.Subscriptions", new[] { "PawzeUserId" });
            DropIndex("dbo.BoxItems", new[] { "InventoryId" });
            DropIndex("dbo.BoxItems", new[] { "BoxId" });
            DropIndex("dbo.Boxes", new[] { "PawzeUserId" });
            DropIndex("dbo.Boxes", new[] { "SubscriptionId" });
            DropTable("dbo.Configurations");
            DropTable("dbo.Shipments");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.PawzeUsers");
            DropTable("dbo.Inventories");
            DropTable("dbo.BoxItems");
            DropTable("dbo.Boxes");
        }
    }
}
