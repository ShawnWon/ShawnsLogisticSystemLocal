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
        public string Status { get; set; }//status can be "Pending, Submitted, Approved, Rejected"

        public InventoryAdj(Item i,int q,string r) {

            item = i;
            Quant = q;
            Reason = r;
            Status = "Pending";
        }
    }
}