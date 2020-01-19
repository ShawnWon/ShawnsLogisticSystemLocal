using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class StockCard
    {
       

        public int Id { get; set; }
        public Item item { get; set; }
        public DateTime date { get; set; }
        public Department department { get; set; }
        public double uprice { get; set; }
        public int quant { get; set; }
        public int balance { get; set; }
        public string comment { get; set; }

        public StockCard(Item it, DateTime dt,double price, int q,int b) {
            item = it;
            date = dt;
            uprice = price;
            quant = q;
            balance = b;
            comment = "Add in";

        }

        public StockCard(Item it, DateTime dt, Department d, int q, int b)
        {
            item = it;
            date = dt;
            department = d;
            quant = q;
            balance = b;
            comment = "Withdraw";

        }

        public StockCard(Item it, DateTime dt, int q, int b)
        {
            item = it;
            date = dt;            
            quant = q;
            balance = b;
            comment = "Stock Adjustment";
        }

        public StockCard()
        {
        }

        public bool equalsTo(StockCard other)
        {
            if (this.Id == other.Id) return true;
            return false;
        }
    }
}