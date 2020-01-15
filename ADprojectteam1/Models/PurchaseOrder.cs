using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string PONumber { get; set; }
        public string DelToAdd { get; set; }
        public virtual ICollection<ReOrderRecord> items { get; set; }
        public DateTime RequestDelivDate { get; set; }
        public double amount { get; set; }
        public string status { get; set; }//status can be "Pending","Sent","Confirmed","Delivered"
    }
}