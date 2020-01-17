using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class ReqItem
    {
        public int Id { get; set; }
        public virtual Item item { get; set; }
        public virtual Employee emp { get; set; }
        public int Quant { get; set; }
        public int DeliveredQuant { get; set; }
        public string Status { get; set; }//value can be "pending","approved","partly delivered","delivered".

        public ReqItem(Item i, Employee e, int q) {
            item = i;
            emp = e;
            Quant = q;
            DeliveredQuant = 0;

            Status = "pending";
        }

        public ReqItem()
        {
        }
    }
}