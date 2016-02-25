using Pawze.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.Core.Domain
{
    public class Box
    {
        public Box()
        {

        }

        public Box(BoxesModel box)
        {
            this.Update(box);
        }

        public void Update(BoxesModel box)
        {
            BoxId = box.BoxId;
            SubscriptionId = box.SubscriptionId;
            PawzeUserId = box.PawzeUserId;
        }

        public int BoxId { get; set; }
        public int? SubscriptionId { get; set; }
        public string PawzeUserId { get; set; }

        public virtual ICollection<BoxItem> BoxItems { get; set; }
        public virtual PawzeUser PawzeUser { get; set; }
        public virtual Subscription Subscription { get; set; }

    }
}