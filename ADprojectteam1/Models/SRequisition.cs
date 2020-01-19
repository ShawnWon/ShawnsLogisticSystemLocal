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

        

        public virtual ICollection<ReqItem> ListItem { get; set; }

        public string status { get; set; }//Can be "pending, approved/rejected, fulfiled(when all ReqItems' status is delivered)."

        public string remark { get; set; }

        public SRequisition() {
            status = "pending";
        }

        public bool equalsTo(SRequisition other)
        {
            if (this.Id==other.Id) return true;
            return false;
        }

        public Employee getEmployee()
        {
            return ListItem.FirstOrDefault().emp;
        
        }

        public double GetAmount() {
            double amount = 0;
            foreach (ReqItem ri in ListItem)
            {
                amount += ri.Quant * StockCardData.FindPriceByItemId(ri.item.Id);
            
            }
            return amount;
        }
    }
}