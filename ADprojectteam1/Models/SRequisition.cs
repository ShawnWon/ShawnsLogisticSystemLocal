using ADprojectteam1.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class SRequisition
    {
        public int Id { get; set; }

        public string RFormNum { get; set; }

        public virtual ICollection<ReqItem> ListItem { get; set; }

        public string status { get; set; }//Can be "Pending, Approved/Rejected, Fulfiled(when all ReqItems' status is delivered)."

        public string remark { get; set; }

        public SRequisition() {
            status = "Pending";
        }

        public bool equalsTo(SRequisition other)
        {
            if (this.RFormNum.Equals( other.RFormNum)) return true;
            return false;
        }

        public double GetAmount() {
            double amount = 0;
            foreach (ReqItem ri in ListItem)
            {
                amount += ri.Quant * ItemData.FindAvePriceByItem(ri.item);
            
            }
            return amount;
        }
    }
}