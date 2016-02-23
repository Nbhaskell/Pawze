using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Models
{
    public class SubscriptionsModel
    {
        public int SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public string PawzeUserId { get; set; }
        public string BoxId { get; set; }
        public bool ActiveSubscription { get; set; }
    }
}