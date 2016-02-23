using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{
    public class Inventory
    {

        public int InventoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityOnHand { get; set; }

        public virtual ICollection<BoxItem> BoxItems { get; set; }
    }
}