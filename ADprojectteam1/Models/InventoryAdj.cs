using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class InventoryAdj
    {
        public int Id { get; set; }
        public virtual Item item { get; set; }
        public int Quant { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }//status can be "pending, approved, rejected"

        

        public InventoryAdj(Item i,int q,string r) {

            item = i;
            Quant = q;
            Reason = r;
            Status = "pending";
        }

        public InventoryAdj() { }

        public override bool Equals(object obj)
        {
            return obj is InventoryAdj adj &&
                   Id == adj.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}