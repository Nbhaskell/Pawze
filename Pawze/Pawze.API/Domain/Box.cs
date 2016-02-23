using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class Box
    {

        public int BoxId { get; set; }
        public int? SubscriptionId { get; set; }
        public string PawzeUserId { get; set; }

        public virtual ICollection<BoxItem> BoxItems { get; set; }
        public virtual PawzeUser PawzeUser { get; set; }
        public virtual Subscription Subscription { get; set; }

    }
}