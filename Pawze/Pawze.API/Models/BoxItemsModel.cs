﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Models
{
    public class BoxItemsModel
    {
        public int BoxItemId { get; set; }
        public int BoxId { get; set; }
        public int InventoryId { get; set; }
        public decimal BoxItemPrice { get; set; }
    }
}