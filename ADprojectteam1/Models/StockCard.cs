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
        public Supplier supplier { get; set; }
        public double uprice { get; set; }
        public int quant { get; set; }
        public int balance { get; set; }
        public string comment { get; set; }

        public StockCard(Item it, DateTime dt,Supplier s,double price, int q,int b) {
            item = it;
            date = dt;
            supplier = s;
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

        public override bool Equals(object obj)
        {
            return obj is StockCard card &&
                   Id == card.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}