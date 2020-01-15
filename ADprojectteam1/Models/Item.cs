using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQty { get; set; }
        public string UnitofMeasure { get; set; }
        public int stockBalance { get; set; }
        public virtual ItemSupplier Supplier1 { get; set; }
        public virtual ItemSupplier Supplier2 { get; set; }
        public virtual ItemSupplier Supplier3 { get; set; }

        public Item(string icode,string cat, string desc,int relev,int requan,string uom) {
            ItemCode = icode;
            Category = cat;
            Description = desc;
            ReorderLevel = relev;
            ReorderQty = requan;
            UnitofMeasure = uom;
            stockBalance = 20;

        
        }
    }
}