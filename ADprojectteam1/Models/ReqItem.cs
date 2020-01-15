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
        public virtual SRequisition SReq { get; set; }
        public int Quant { get; set; }
        public int DeliveredQuant { get; set; }
    }
}