using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public string PawzeUserId{ get; set; }
        public string BoxId { get; set; }
        public bool ActiveSubscription { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Box> Boxes { get; set; }
        public virtual PawzeUser PawzeUser { get; set; }
    }
}