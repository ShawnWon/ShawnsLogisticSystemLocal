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
        //public int StockBalance { get; set; }
        public string WarehouseLocation { get; set; }


        public ItemSupplier(Item i, Supplier s, double up) {
            item = i;
            supplier = s;
            UnitPrice = up;
            //StockBalance = 20;
            WarehouseLocation ="Z1S1R1";
        }

        public ItemSupplier()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is ItemSupplier supplier &&
                   Id == supplier.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}