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
    }
}