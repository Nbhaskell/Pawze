using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class PawzeUser
    {
        public string PawzeUserId { get; set; }
        public string StripeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public bool? International { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        

        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Box> Boxes { get; set; }
        //public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}