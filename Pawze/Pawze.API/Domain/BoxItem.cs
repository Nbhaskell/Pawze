using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class BoxItem
    {

        public int BoxItemId { get; set; }
        public int BoxId { get; set; }
        public int InventoryId { get; set; }
        public decimal BoxItemPrice { get; set; }

        public virtual Box Box { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}