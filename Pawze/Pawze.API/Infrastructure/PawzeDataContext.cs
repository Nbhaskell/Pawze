using Pawze.API.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pawze.API.Infrastructure
{
    public class PawzeDataContext : DbContext
    {
        public PawzeDataContext() : base("Pawze")
        {

        }
        public IDbSet<Box> Boxes { get; set; }
        public IDbSet<BoxItem> BoxItems { get; set; }
        public IDbSet<Inventory> Inventories { get; set; }
        public IDbSet<Subscription> Subscription { get; set; }
        public IDbSet<PawzeUser> PawzeUsers { get; set; }
        public IDbSet<Shipment> Shipment { get; set; }
        public IDbSet<Configuration> Configuration { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PawzeUser>()
            .HasMany(p => p.Subscriptions)
            .WithRequired(o => o.PawzeUser)
            .HasForeignKey(o => o.PawzeUserId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<PawzeUser>()
            .HasMany(u => u.Boxes)
            .WithRequired(b => b.PawzeUser)
            .HasForeignKey(b => b.PawzeUserId);



            modelBuilder.Entity<Subscription>()
            .HasMany(o => o.Boxes)
            .WithOptional(b => b.Subscription)
            .HasForeignKey(b => b.SubscriptionId);
            

            modelBuilder.Entity<Box>()
            .HasMany(b => b.BoxItems)
            .WithRequired(b => b.Box)
            .HasForeignKey(b => b.BoxId);

            modelBuilder.Entity<Inventory>()
            .HasMany(i => i.BoxItems)
            .WithRequired(b => b.Inventory)
            .HasForeignKey(b => b.InventoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}