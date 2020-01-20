using ADprojectteam1.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class DeliverOrder
    {
        public int Id { get; set; }

        
       

        public int depId { get; set; }

        public string status { get; set; }//Can be "collected, partlydelivered, fulfiled"

        public Dictionary<int, int> itemList { get; set; }

        public double GetOrderAmount()
        {
            double amount = 0;
            foreach (int itemId in itemList.Keys)
            {
                amount += itemList[itemId]* StockCardData.FindPriceByItemId(itemId);
            }
            return amount;
        }

        public DeliverOrder() {
            this.status = "collected";
        }

        

       



  
    }
}