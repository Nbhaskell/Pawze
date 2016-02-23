using Pawze.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class PawzeUser
    {
        public PawzeUser()
        {

        }
        public PawzeUser(PawzeUsersModel pawzeUser)
        {
            this.Update(pawzeUser);
        }

        public void Update(PawzeUsersModel pawzeUser)
        {
            PawzeUserId = pawzeUser.PawzeUserId;
            UserName = pawzeUser.UserName;
            StripeId = pawzeUser.StripeId;
            FirstName = pawzeUser.FirstName;
            LastName = pawzeUser.LastName;
            Address1 = pawzeUser.Address1;
            Address2 = pawzeUser.Address2;
            Address3 = pawzeUser.Address3;
            Address4 = pawzeUser.Address4;
            Address5 = pawzeUser.Address5;
            City = pawzeUser.City;
            State = pawzeUser.State;
            PostCode = pawzeUser.PostCode;
            International = pawzeUser.International;
            Telephone = pawzeUser.Telephone;
            EmailAddress = pawzeUser.EmailAddress;
        }

        public string PawzeUserId { get; set; }
        public string UserName { get; set; }
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