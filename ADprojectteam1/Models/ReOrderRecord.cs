using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class ReOrderRecord
    {
        public int Id { get; set; }
        public virtual ItemSupplier itemsupplier { get; set; }
        public virtual PurchaseOrder po { get; set; }
        public string status { get; set; }//status can be "Pending","PO sent","PO confirmed","Complete"

        public ReOrderRecord(ItemSupplier i) {
            itemsupplier =i;
            status = "Pending";

        
        }
    }
}