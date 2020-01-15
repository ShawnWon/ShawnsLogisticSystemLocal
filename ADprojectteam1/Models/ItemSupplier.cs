using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class ItemSupplier
    {
        public int Id { get; set; }
        public virtual Item item { get; set; }
        public virtual Supplier supplier { get; set; }
        public double UnitPrice { get; set; }

        public ItemSupplier(Item i, Supplier s, double up) {
            item = i;
            supplier = s;
            UnitPrice = up;
        }
    }
}