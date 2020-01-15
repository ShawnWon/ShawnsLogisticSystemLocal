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

        public string status { get; set; }//Can be "UnSubmitted, Submitted, Approved/Rejected, Partly Delivered, Fulfiled(when deliveredQuant equals to Quant in ListItem)."

        public SRequisition() {
            status = "Unsubmitted";
        }
    }
}