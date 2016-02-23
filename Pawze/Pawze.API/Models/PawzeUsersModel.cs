﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Models
{
    public class PawzeUsersModel
    {
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
    }
}