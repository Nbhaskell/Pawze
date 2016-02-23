using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public string Tracking { get; set; }
        public int PawzeUserId { get; set; }
        public DateTime ShipmentDate { get; set; }

        public virtual Subscription Subscription { get; set;}
    }
}