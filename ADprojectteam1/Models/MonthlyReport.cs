using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class MonthlyReport
    {
       

        public int Id { get; set; }
        public int itemId { get; set; }
        public string date { get; set; }
        public int consumption { get; set; }
        public int stockbalance { get; set; }

        public MonthlyReport(int itemId, string date, int consumption, int stockbalance)
        {
            this.itemId = itemId;
            this.date = date;
            this.consumption = consumption;
            this.stockbalance = stockbalance;
        }

        public MonthlyReport() { }

        public override bool Equals(object obj)
        {
            return obj is MonthlyReport report &&
                   Id == report.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}